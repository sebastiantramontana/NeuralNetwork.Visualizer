using System.Drawing;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
    internal interface IToolTipFiring
    {
        void Show(Point location);
        void Hide();
    }
}
