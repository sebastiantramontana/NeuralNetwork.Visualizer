using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IDrawableSurface
   {
      Task<Size> GetSize();
      Task<Size> GetDrawingSize();
      Task<Image> GetImage();
      Task RedrawAsync();
      IDrafter Drafter { get; }
   }
}
