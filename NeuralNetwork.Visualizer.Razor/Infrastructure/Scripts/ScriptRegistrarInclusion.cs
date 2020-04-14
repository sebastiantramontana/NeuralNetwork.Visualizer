using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal class ScriptRegistrarInclusion : IScriptRegistrarInclusion
   {
      private readonly IJsInterop _jsInterop;
      private readonly string _scriptBaseUrl;
      private readonly ICollection<ScriptFileRegistration> _fileRegistrations;

      internal ScriptRegistrarInclusion(IJsInterop jsInterop, string scriptBaseUrl)
      {
         _fileRegistrations = new List<ScriptFileRegistration>();
         _jsInterop = jsInterop;
         _scriptBaseUrl = NormalizeBaseUrl(scriptBaseUrl);
      }

      public IScriptRegistrar Include(string fileName)
      {
         _fileRegistrations.Add(new ScriptFileRegistration(fileName));
         return this;
      }

      public IScriptRegistrarInclusion Register(IScriptRegistration script, string globalInstaceName)
      {
         _fileRegistrations.Last().InstanceRegistrations.Add(new ScriptInstanceRegistration(script.FunctionName, globalInstaceName));
         return this;
      }

      public async ValueTask Execute()
      {
         foreach (var fileRegistraion in _fileRegistrations)
         {
            string src = BuildSrcAttribute(_scriptBaseUrl, fileRegistraion.FileName);
            string id = BuildIdAttributte(src);
            string code = BuildCode(id, src, fileRegistraion.InstanceRegistrations);

            Console.WriteLine(code);

            await _jsInterop.ExcuteCode(code);
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

      private string BuildCode(string id, string src, IEnumerable<ScriptInstanceRegistration> instanceRegistrations)
      {
         var functionCallings = StringifyFunctionCalling(instanceRegistrations);

         return @$"
                  if(document.getElementById('{id}') === null)
                  {{
                     var script = document.createElement('script');
                     script.type = 'text/javascript';
                     script.defer = 'defer';
                     script.src = '{src}';
                     script.id = '{id}';
                     script.onload = function()
                     {{
                        {functionCallings}
                     }};

                     var head = document.getElementsByTagName('head')[0];
                     head.appendChild(script);
                  }}
                  else
                  {{
                     {functionCallings}
                  }}";
      }

      private string StringifyFunctionCalling(IEnumerable<ScriptInstanceRegistration> instanceRegistrations)
      {
         StringBuilder stringBuilder = new StringBuilder();

         foreach (var registration in instanceRegistrations)
         {
            stringBuilder.AppendLine($"{registration.FunctionName}('{registration.GlobalInstanceName}');");
         }

         return stringBuilder.ToString();
      }
   }
}
