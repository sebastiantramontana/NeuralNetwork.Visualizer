using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   public interface IElementDrawing
   {
      Task Draw(ICanvas canvas);
   }
}
