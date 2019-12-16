using NeuralNetwork.Visualizer.Preferences.Core;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
    internal interface IToolTipFiring
    {
        void Show(Position position);
        void Hide();
    }
}
