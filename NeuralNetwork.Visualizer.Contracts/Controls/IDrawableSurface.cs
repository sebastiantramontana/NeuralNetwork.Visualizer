using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IDrawableSurface
   {
      Size Size { get; }
      Size DrawingSize { get; }
      Task<Image> GetImage();
      Task RedrawAsync();
   }
}
