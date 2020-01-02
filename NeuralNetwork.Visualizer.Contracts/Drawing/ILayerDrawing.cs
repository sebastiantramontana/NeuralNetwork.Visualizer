using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   public interface ILayerDrawing : IElementDrawing
   {
      IEnumerable<INodeDrawing> NodesDrawing { get; }
   }
}
