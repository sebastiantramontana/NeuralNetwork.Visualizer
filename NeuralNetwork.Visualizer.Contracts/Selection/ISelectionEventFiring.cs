using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
   public interface ISelectionEventFiring
   {
      void FireSelectionEvent(Position position, SelectionEvent selectionEvent);
   }
}
