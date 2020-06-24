using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Razor.Selection
{
   internal class RegionBuilder : IRegionBuilder
   {
      public IRegion Ellipse(Rectangle rectangle)
      {
         return new Region();
      }

      public IRegion Polygon(IEnumerable<Position> positions)
      {
         return new Region();
      }

      public IRegion Rectangle(Rectangle rectangle)
      {
         return new Region();
      }
   }
}
