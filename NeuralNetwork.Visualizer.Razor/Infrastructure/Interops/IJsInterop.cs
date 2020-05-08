using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Interops
{
   internal interface IJsInterop
   {
      Task ExcuteCode(string code);
      Task<TReturn> ExcuteCode<TReturn>(string code);
      Task ExcuteFunction(string functionName, params object[] args);
      Task<TReturn> ExcuteFunction<TReturn>(string functionName, params object[] args);
      Task ExecuteOnInstance(string functionPath, params object[] args);
      Task<TReturn> ExecuteOnInstance<TReturn>(string functionPath, params object[] args);
   }
}
