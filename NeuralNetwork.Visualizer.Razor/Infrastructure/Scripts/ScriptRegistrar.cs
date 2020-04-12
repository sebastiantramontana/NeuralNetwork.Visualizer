using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal class ScriptRegistrar : IScriptRegistrar
   {
      private readonly IJsInterop _jsInterop;

      public ScriptRegistrar(IJsInterop jsInterop)
      {
         _jsInterop = jsInterop;
      }

      public async ValueTask<IScriptRegistrar> Register(IScriptRegistration script, string globalInstanceName)
      {
         await Task.Delay(3000);
         await _jsInterop.ExcuteFunction(script.FunctionName, globalInstanceName);

         return this;
      }
   }
}
