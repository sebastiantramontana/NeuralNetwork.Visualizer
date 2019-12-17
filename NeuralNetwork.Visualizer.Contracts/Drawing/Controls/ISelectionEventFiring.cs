using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Controls
{
   internal interface ISelectionEventFiring
   {
      void FireSelectionEvent(Position position);
   }
}
