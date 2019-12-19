using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface ISelectionEventFiring
   {
      void FireSelectionEvent(Position position);
   }
}
