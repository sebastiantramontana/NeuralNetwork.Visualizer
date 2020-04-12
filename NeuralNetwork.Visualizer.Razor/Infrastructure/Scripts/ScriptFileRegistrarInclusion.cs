using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal class ScriptFileRegistrarInclusion : IScriptFileRegistrarInclusion
   {
      private readonly IScriptRegistrar _scriptRegistrar;
      private readonly IJsInterop _jsInterop;
      private readonly string _scriptBaseUrl;

      public ScriptFileRegistrarInclusion(IScriptRegistrar scriptRegistrar, IJsInterop jsInterop, string scriptBaseUrl)
      {
         _scriptRegistrar = scriptRegistrar;
         _jsInterop = jsInterop;
         _scriptBaseUrl = NormalizeBaseUrl(scriptBaseUrl);
      }

      public async ValueTask<IScriptRegistrar> Include(string fileName)
      {
         string src = BuildSrcAttribute(fileName);
         string id = BuildIdAttributte(src);
         string code = BuildCode(id, src);

         Console.WriteLine($"src = {src}");
         Console.WriteLine($"id = {id}");
         Console.WriteLine($"code = {code}");

         await _jsInterop.ExcuteCode(code);

         return _scriptRegistrar;
      }

      private string NormalizeBaseUrl(string scriptBaseUrl)
      {
         return scriptBaseUrl?.Trim()?.EndsWith('/') ?? true ? scriptBaseUrl : scriptBaseUrl + '/';
      }

      private string BuildSrcAttribute(string fileName)
      {
         if (string.IsNullOrWhiteSpace(fileName))
         {
            throw new ArgumentException($"Script file name to include cannot be null or blank", nameof(fileName));
         }

         return $"{_scriptBaseUrl}{fileName.Trim()}";
      }

      private string BuildIdAttributte(string srcAttribute)
      {
         return $"neuralnetwork-visualizer-script-{srcAttribute.Replace(' ', '-').Replace('/', '-')}";
      }

      private string BuildCode(string id, string src)
      {
         return @$"
                  if(document.getElementById('{id}') === null)
                  {{
                     var script = document.createElement('script');
                     script.type = 'text/javascript';
                     script.defer = 'defer';
                     script.src = '{src}';
                     script.id = '{id}';

                     var head = document.getElementsByTagName('head')[0];
                     head.appendChild(script);
                  }}";
      }
   }
}
