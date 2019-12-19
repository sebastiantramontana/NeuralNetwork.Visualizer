using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   public interface INodeDrawing : IDrawing
   {
      ICanvas Canvas { get; }
      Position EdgeStartPosition { get; }
      NodeBase Node { get; }
   }
}
