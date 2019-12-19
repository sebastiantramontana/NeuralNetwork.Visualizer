using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Selection
{
   public interface ISelectionEventFiring
   {
      void FireSelectionEvent(Position position);
   }
}
