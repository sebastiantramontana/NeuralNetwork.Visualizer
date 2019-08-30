using System.Drawing;

namespace NeuralNetwork.Visualizer.Drawing.Cache
{
   internal abstract class NodeSizesPreCalc
   {
      internal Rectangle? OutputLabelRectangle { get; set; }
      internal Rectangle? EllipseRectangle { get; set; }
      internal Size? OutputSize { get; set; }
   }
}
