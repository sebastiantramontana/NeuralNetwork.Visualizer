using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Calcs
{
   public class SimpleNodeSizesPreCalc : NodeSizesPreCalc
   {
      internal Rectangle InputLabelRectangle { get; set; }
      internal double YCenteringOffeset { get; set; }
   }
}
