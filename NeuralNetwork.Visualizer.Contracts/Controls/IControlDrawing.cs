using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   internal interface IControlDrawing
   {
      Task RedrawAsync();
      void Redraw();
      Image GetImage();
   }
}
