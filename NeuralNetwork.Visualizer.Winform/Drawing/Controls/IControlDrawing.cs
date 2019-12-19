using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   public interface IControlDrawing
   {
      Task RedrawAsync();
      Task<Image> GetImage();
   }
}
