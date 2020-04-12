using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal static class ScriptRegistrarExt
   {
      internal static async ValueTask<IScriptRegistrar> Register(this ValueTask<IScriptRegistrar> task, IScriptRegistration script, string globalInstanceName)
      {
         return await (await task).Register(script, globalInstanceName).ConfigureAwait(false);
      }
   }
}
