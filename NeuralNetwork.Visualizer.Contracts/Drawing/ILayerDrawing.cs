using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   public interface ILayerDrawing : IDrawing
   {
      IEnumerable<INodeDrawing> NodesDrawing { get; }
   }
}
