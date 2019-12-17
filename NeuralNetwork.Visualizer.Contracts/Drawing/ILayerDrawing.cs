using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   internal interface ILayerDrawing : IDrawing
   {
      IEnumerable<INodeDrawing> NodesDrawing { get; }
   }
}
