using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Interops
{
   internal interface IJsInterop
   {
      Task ExcuteCodeAsync(string code);
      Task<TReturn> ExcuteCodeAsync<TReturn>(string code);
      Task ExcuteFunctionAsync(string functionName, params object[] args);
      Task<TReturn> ExcuteFunctionAsync<TReturn>(string functionName, params object[] args);
      void ExecuteOnInstance(string functionPath, params object[] args);
      Task ExecuteOnInstanceAsync(string functionPath, params object[] args);
      Task<TReturn> ExecuteOnInstanceAsync<TReturn>(string functionPath, params object[] args);
   }
}
