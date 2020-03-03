using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Html.Selection
{
   internal class RegionBuilder : IRegionBuilder
   {
      public IRegion Ellipse(Rectangle rectangle)
      {
         throw new System.NotImplementedException();
      }

      public IRegion Polygon(IEnumerable<Position> positions)
      {
         throw new System.NotImplementedException();
      }

      public IRegion Rectangle(Rectangle rectangle)
      {
         throw new System.NotImplementedException();
      }
   }
}
