using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IControlDrawing
   {
      Task RedrawAsync();
      void Redraw();
      Task<Image> GetImage();
   }
}
