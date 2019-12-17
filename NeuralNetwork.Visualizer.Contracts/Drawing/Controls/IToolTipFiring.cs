using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Controls
{
   internal interface IToolTipFiring
   {
      void Show(Position position);
      void Hide();
   }
}
