using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Winform.Selection
{
   public interface ISelectionEventFiring
   {
      void FireSelectionEvent(Position position);
   }
}
