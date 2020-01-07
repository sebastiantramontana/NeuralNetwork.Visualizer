using NeuralNetwork.Visualizer.Contracts.Drawing;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IDrafter
   {
      Task RedrawAsync(ICanvasBuilder canvasBuilder);
   }
}
