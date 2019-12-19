using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Calcs
{
   public abstract class NodeSizesPreCalc
   {
      public Rectangle OutputLabelRectangle { get; set; }
      public Rectangle EllipseRectangle { get; set; }
      public Size OutputSize { get; set; }
   }
}
