using Microsoft.JSInterop;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal class ScriptRegistrarInclusion : IScriptRegistrarInclusion
   {
      private const string INSERT_SCRIPT_FUNCTION_NAME = "neuralNetworkVisualizerInsertScript";

      private readonly IJsInterop _jsInterop;
      private readonly string _globalInstanceName;
      private readonly string _scriptBaseUrl;
      private readonly ICollection<ScriptFileRegistration> _fileRegistrations;

      internal ScriptRegistrarInclusion(IJsInterop jsInterop, string scriptBaseUrl, string globalInstanceName)
      {
         _fileRegistrations = new List<ScriptFileRegistration>();
         _jsInterop = jsInterop;
         _globalInstanceName = globalInstanceName;
         _scriptBaseUrl = NormalizeBaseUrl(scriptBaseUrl);
      }

      public IScriptRegistrar Include(string fileName)
      {
         _fileRegistrations.Add(new ScriptFileRegistration(fileName));
         return this;
      }

      public IScriptRegistrarInclusion Register(IScriptRegistration script)
      {
         _fileRegistrations
            .Last()
            .InstanceRegistrations
            .Add(new ScriptInstanceRegistration(script.FunctionName));

         return this;
      }

      private TaskCompletionSource<bool> _executeTaskCompletion;

      public async Task Execute()
      {
         _executeTaskCompletion = new TaskCompletionSource<bool>();
         Task executeTask = _executeTaskCompletion.Task;

         string insertCode = BuildInsertScriptCode();
         await _jsInterop.ExcuteCode(insertCode);

         Task task = Task.CompletedTask;
         using var dotNetReference = DotNetObjectReference.Create(this);

         foreach (var fileRegistraion in _fileRegistrations)
         {
            string src = BuildSrcAttribute(_scriptBaseUrl, fileRegistraion.FileName);
            string id = BuildIdAttributte(src);
            
            await task.ContinueWith((t) => task = ExecuteInsertScript(id, src, fileRegistraion.InstanceRegistrations, _globalInstanceName, dotNetReference));
         }

         await executeTask;
      }

      private int _onScriptRegisteredCount = 0;

      [JSInvokable]
      public void OnScriptRegistered()
      {
         _onScriptRegisteredCount++;

         if (_fileRegistrations.Count == _onScriptRegisteredCount)
         {
            _executeTaskCompletion.SetResult(true);
         }
      }

      private string NormalizeBaseUrl(string scriptBaseUrl)
      {
         return scriptBaseUrl?.Trim()?.EndsWith('/') ?? true ? scriptBaseUrl : scriptBaseUrl + '/';
      }

      private string BuildSrcAttribute(string scriptBaseUrl, string fileName)
      {
         if (string.IsNullOrWhiteSpace(fileName))
         {
            throw new ArgumentException($"Script file name to include cannot be null or blank", nameof(fileName));
         }

         return $"{scriptBaseUrl}{fileName.Trim()}";
      }

      private string BuildIdAttributte(string srcAttribute)
      {
         return $"neuralnetwork-visualizer-script-{srcAttribute.Replace(' ', '-').Replace('/', '-')}";
      }

      private string BuildInsertScriptCode()
      {
         return @$"var {INSERT_SCRIPT_FUNCTION_NAME} = {INSERT_SCRIPT_FUNCTION_NAME} || ((id, src, registrationFunctions, globalInstanceName, dotNetRef) =>
               {{
                  let executeRegistrations = () =>
                  {{
                     registrationFunctions
                           .forEach(rf => window[rf](globalInstanceName));

                     dotNetRef.invokeMethod('{nameof(OnScriptRegistered)}');
                  }};
                  
                  let createScriptTag = () =>
                  {{
                     let script = document.createElement('script');

                     script.setAttribute('type', 'text/javascript');
                     script.setAttribute('async', 'async');
                     script.setAttribute('src', src);
                     script.setAttribute('id', id);

                     script.onload = () =>
                     {{
                        executeRegistrations();
                     }};

                     return script;
                  }};

                  let appendToHead = (script) =>
                  {{
                     let head = document.getElementsByTagName('head')[0];
                     head.appendChild(script);
                  }};

                  if(document.getElementById(id) === null)
                  {{
                     let script = createScriptTag();
                     appendToHead(script);
                  }}
                  else
                  {{
                     executeRegistrations();
                  }}
               }})";
      }

      private async Task ExecuteInsertScript<T>(string id, string src, IEnumerable<ScriptInstanceRegistration> instanceRegistrations, string globalInstanceName, DotNetObjectReference<T> dotNetReference)
         where T : class
      {
         var functions = StringifyFunctionsArray(instanceRegistrations);
         await _jsInterop.ExcuteFunction(INSERT_SCRIPT_FUNCTION_NAME, id, src, functions, globalInstanceName, dotNetReference);
      }

      private string[] StringifyFunctionsArray(IEnumerable<ScriptInstanceRegistration> instanceRegistrations)
      {
         return instanceRegistrations
            .Select(ir => ir.FunctionName)
            .ToArray();
      }
   }
}
