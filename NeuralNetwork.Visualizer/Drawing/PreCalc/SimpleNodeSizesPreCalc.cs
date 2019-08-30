using System.Drawing;

namespace NeuralNetwork.Visualizer.Drawing.Cache
{
   internal class SimpleNodeSizesPreCalc : NodeSizesPreCalc
   {
      internal Rectangle? InputLabelRectangle { get; set; }
      internal double YCenteringOffeset { get; set; }
   }
}
