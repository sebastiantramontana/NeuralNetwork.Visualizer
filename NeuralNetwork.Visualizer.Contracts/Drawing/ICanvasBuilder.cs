using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   public interface ICanvasBuilder
   {
      ICanvas Build(Size size);
   }
}
