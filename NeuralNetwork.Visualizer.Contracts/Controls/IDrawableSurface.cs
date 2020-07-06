using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IDrawableSurface
   {
      Size Size { get; }
      Size DrawingSize { get; }
      Image Image { get; }
      IDrafter Drafter { get; }

      Task RedrawAsync();
   }
}
