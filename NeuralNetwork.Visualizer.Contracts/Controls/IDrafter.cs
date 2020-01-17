using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IDrafter
   {
      Task RedrawAsync(ICanvasBuilder canvasBuilder);
      IRegionBuilder RegionBuilder { get; }
   }
}
