using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Calcs
{
   public class SimpleNodeSizesPreCalc : NodeSizesPreCalc
   {
      public Rectangle InputLabelRectangle { get; set; }
      public double YCenteringOffeset { get; set; }
   }
}
