using NeuralNetwork.Visualizer.Drawing.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Drawing.Layers
{
    internal interface ILayerDrawing : IDrawing
    {
        IEnumerable<INodeDrawing> NodesDrawing { get; }
    }
}
