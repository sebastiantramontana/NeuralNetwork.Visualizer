using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Selection
{
   internal class Region : IRegion
   {
      public Region(Gdi.Region gdiRegion)
      {
         this.GdiRegion = gdiRegion;
      }

      public Gdi.Region GdiRegion { get; }

      public bool IsVisible(Position position)
      {
         return this.GdiRegion.IsVisible(position.ToGdi());
      }
   }
}
