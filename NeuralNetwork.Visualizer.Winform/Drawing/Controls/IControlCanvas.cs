using System;
using System.Threading.Tasks;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   internal interface IControlCanvas : IDisposable
   {
      Gdi.Image GetImage();
      Task RedrawAsync();
   }
}
