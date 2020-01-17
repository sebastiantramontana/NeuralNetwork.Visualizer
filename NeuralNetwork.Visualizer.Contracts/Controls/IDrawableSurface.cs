using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IDrawableSurface
   {
      Size Size { get; }
      Size DrawingSize { get; }
      Image GetImage();
      Task RedrawAsync();
      IDrafter Drafter { get; }
   }
}
