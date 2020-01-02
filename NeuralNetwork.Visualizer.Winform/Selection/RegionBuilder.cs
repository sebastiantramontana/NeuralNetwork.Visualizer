using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Selection
{
   internal class RegionBuilder : IRegionBuilder
   {
      public IRegion Rectangle(Rectangle rectangle)
      {
         return new Region(new Gdi.Region(rectangle.ToGdi()));
      }

      public IRegion Ellipse(Rectangle rectangle)
      {
         var gp = new GraphicsPath();
         gp.AddEllipse(rectangle.ToGdi());

         return new Region(new Gdi.Region(gp));
      }

      public IRegion Polygon(IEnumerable<Position> positions)
      {
         var gp = new GraphicsPath();
         gp.AddPolygon(positions.Select(p => p.ToGdi()).ToArray());
         gp.CloseFigure();

         return new Region(new Gdi.Region(gp));
      }
   }
}
