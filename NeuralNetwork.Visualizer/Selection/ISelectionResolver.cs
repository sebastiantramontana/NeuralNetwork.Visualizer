using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Model;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Selection
{
    internal interface ISelectionResolver
    {
        Element GetElementFromLocation(Point location);
        void SetCurrentRootCanvas(ICanvas currentRootCanvas);
    }
}
