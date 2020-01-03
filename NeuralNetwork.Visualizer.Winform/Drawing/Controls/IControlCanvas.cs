using NeuralNetwork.Visualizer.Contracts.Drawing;
using System.Threading.Tasks;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   internal interface IControlCanvas : ICanvasBuilder
   {
      Gdi.Image GetImage();
      void Redraw();
   }
}
