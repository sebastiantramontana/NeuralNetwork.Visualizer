using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure
{
   internal interface IJsInterop
   {
      ValueTask ExcuteCode(string code);
      ValueTask<TReturn> ExcuteCode<TReturn>(string code);
      ValueTask ExcuteFunction(string functionName, params object[] args);
      ValueTask<TReturn> ExcuteFunction<TReturn>(string functionName, params object[] args);
      ValueTask ExecuteOnInstance(string functionPath, params object[] args);
      ValueTask<TReturn> ExecuteOnInstance<TReturn>(string functionPath, params object[] args);
   }
}
