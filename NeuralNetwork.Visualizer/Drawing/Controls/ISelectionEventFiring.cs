using System.Drawing;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
    internal interface ISelectionEventFiring
    {
        void FireSelectionEvent(Point location);
    }
}
