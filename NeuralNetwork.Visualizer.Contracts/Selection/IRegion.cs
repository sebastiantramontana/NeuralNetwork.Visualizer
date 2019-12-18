using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
   public interface IRegion
   {
      bool IsVisible(Position position);
   }
}
