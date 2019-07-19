using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Brushes
{
    public interface IBrush
    {
        Brush CreateBrush(Rectangle rectangle);
    }
}
