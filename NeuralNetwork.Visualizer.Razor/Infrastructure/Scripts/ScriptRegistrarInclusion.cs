using Microsoft.JSInterop;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs;
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
      private readonly ITaskUnit _taskUnit;
      private readonly string _globalInstanceName;
      private readonly string _scriptBaseUrl;
      private readonly ICollection<ScriptFileRegistration> _fileRegistrations;

      internal ScriptRegistrarInclusion(IJsInterop jsInterop, ITaskUnit taskUnit, string scriptBaseUrl, string globalInstanceName)
      {
         _fileRegistrations = new List<ScriptFileRegistration>();
         _jsInterop = jsInterop;
         _taskUnit = taskUnit;
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

      public async Task Execute()
      {
         ExecuteInsertScriptTagCode();

         using var dotNetObjectReference = DotNetObjectReference.Create(this);

         await _taskUnit.StartAsync(() =>
         {
            var tasks = new List<Task>();

            foreach (var fileRegistraion in _fileRegistrations)
            {
               tasks.Add(ExecuteScriptFileRegistration(fileRegistraion, dotNetObjectReference));
            }

            return Task.WhenAll(tasks);
         });
      }

      private int _onScriptRegisteredCount = 0;

      [JSInvokable]
      public void OnScriptRegistered()
      {
         _onScriptRegisteredCount++;

         if (_fileRegistrations.Count == _onScriptRegisteredCount)
         {
            _taskUnit.Finish();
         }
      }

      private Task ExecuteScriptFileRegistration(ScriptFileRegistration scriptFileRegistration, DotNetObjectReference<ScriptRegistrarInclusion> dotNetReference)
      {
         string src = BuildSrcAttribute(_scriptBaseUrl, scriptFileRegistration.FileName);
         string id = BuildIdAttributte(src);

         return ExecuteInsertScript(id, src, scriptFileRegistration.InstanceRegistrations, _globalInstanceName, dotNetReference);
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

      private void ExecuteInsertScriptTagCode()
      {
         string insertCode = BuildInsertScriptCode();
         _jsInterop.ExcuteCode(insertCode);
      }

      private string BuildInsertScriptCode()
      {
         return @$"var {INSERT_SCRIPT_FUNCTION_NAME} = {INSERT_SCRIPT_FUNCTION_NAME} || ((id, src, registrationFunctions, globalInstanceName, dotNetRef) =>
               {{
                  const executeRegistrations = () =>
                  {{
                     registrationFunctions
                           .forEach(rf => window[rf](globalInstanceName));

                     dotNetRef.invokeMethod('{nameof(OnScriptRegistered)}');
                  }};
                  
                  const createScriptTag = () =>
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

                  const appendToHead = (script) =>
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

      private Task ExecuteInsertScript(string id, string src, IEnumerable<ScriptInstanceRegistration> instanceRegistrations, string globalInstanceName, DotNetObjectReference<ScriptRegistrarInclusion> dotNetReference)
      {
         var functions = StringifyFunctionsArray(instanceRegistrations);
         return _jsInterop.ExcuteFunctionAsync(INSERT_SCRIPT_FUNCTION_NAME, id, src, functions, globalInstanceName, dotNetReference);
      }

      private string[] StringifyFunctionsArray(IEnumerable<ScriptInstanceRegistration> instanceRegistrations)
      {
         return instanceRegistrations
            .Select(ir => ir.FunctionName)
            .ToArray();
      }
   }
}
