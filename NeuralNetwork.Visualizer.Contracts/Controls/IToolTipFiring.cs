using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IToolTipFiring
   {
      void Show(Position position);
      void Hide();
   }
}
