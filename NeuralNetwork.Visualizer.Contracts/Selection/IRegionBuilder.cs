using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
   public interface IRegionBuilder
   {
      IRegion Rectangle(Rectangle rectangle);
      IRegion Ellipse(Rectangle rectangle);
      IRegion Polygon(IEnumerable<Position> positions);
   }
}
