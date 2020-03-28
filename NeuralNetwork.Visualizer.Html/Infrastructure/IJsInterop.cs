using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure
{
   internal interface IJsInterop
   {
      ValueTask Excute(string code);
      ValueTask<TReturn> Excute<TReturn>(string code);
      ValueTask ExecuteInstance(string functionPath, params object[] args);
      ValueTask<TReturn> ExecuteInstance<TReturn>(string functionPath, params object[] args);
   }
}
