using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System;
using System.Collections.Generic;
using System.Linq;

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

      public void Execute()
      {
         ExecuteInsertScriptTagCode();

         foreach (var fileRegistraion in _fileRegistrations)
         {
            ExecuteScriptFileRegistration(fileRegistraion);
         }
      }

      private void ExecuteScriptFileRegistration(ScriptFileRegistration scriptFileRegistration)
      {
         string src = BuildSrcAttribute(_scriptBaseUrl, scriptFileRegistration.FileName);
         string id = BuildIdAttributte(src);

         ExecuteInsertScript(id, src, scriptFileRegistration.InstanceRegistrations, _globalInstanceName);
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
         return @$"var {INSERT_SCRIPT_FUNCTION_NAME} = {INSERT_SCRIPT_FUNCTION_NAME} || ((id, src, registrationFunctions, globalInstanceName) =>
               {{
                  const executeRegistrations = () =>
                  {{
                     registrationFunctions
                           .forEach(rf => window[rf](globalInstanceName));
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

      private void ExecuteInsertScript(string id, string src, IEnumerable<ScriptInstanceRegistration> instanceRegistrations, string globalInstanceName)
      {
         var functions = StringifyFunctionsArray(instanceRegistrations);
         _jsInterop.ExcuteFunction(INSERT_SCRIPT_FUNCTION_NAME, id, src, functions, globalInstanceName);
      }

      private string[] StringifyFunctionsArray(IEnumerable<ScriptInstanceRegistration> instanceRegistrations)
      {
         return instanceRegistrations
            .Select(ir => ir.FunctionName)
            .ToArray();
      }
   }
}
