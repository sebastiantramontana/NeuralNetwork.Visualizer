using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure
{
   internal interface IScript<T>
   {
      ValueTask<T> CreateDomAccess();
   }
}
