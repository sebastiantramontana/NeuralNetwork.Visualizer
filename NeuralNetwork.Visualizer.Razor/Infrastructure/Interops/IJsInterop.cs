using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Interops
{
   internal interface IJsInterop
   {
      void ExcuteCode(string code);
      TReturn ExcuteCode<TReturn>(string code);
      void ExcuteFunction(string functionName, params object[] args);
      Task ExcuteFunctionAsync(string functionName, params object[] args);
      TReturn ExcuteFunction<TReturn>(string functionName, params object[] args);
      void ExecuteOnInstance(string functionPath, params object[] args);
      TReturn ExecuteOnInstance<TReturn>(string functionPath, params object[] args);
   }
}
