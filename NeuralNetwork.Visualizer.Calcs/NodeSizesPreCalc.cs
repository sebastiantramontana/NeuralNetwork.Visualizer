using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Calcs
{
   public abstract class NodeSizesPreCalc
   {
      internal Rectangle OutputLabelRectangle { get; set; }
      internal Rectangle EllipseRectangle { get; set; }
      internal Size OutputSize { get; set; }
   }
}
