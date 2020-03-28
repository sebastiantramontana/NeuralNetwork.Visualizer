using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure.Scripts
{
   internal class ScriptRegistrar : IScriptRegistrar
   {
      private readonly IJsInterop _jsInterop;

      public ScriptRegistrar(IJsInterop jsInterop)
      {
         _jsInterop = jsInterop;
      }

      public async ValueTask Register(IScriptRegistration script)
      {
         await _jsInterop.Excute(script.Code);
      }
   }
}
