using NeuralNetwork.Model;

namespace NeuralNetwork.Visualizer.Selection
{
    internal interface IElementSelectionChecker
    {
        bool IsSelected(Element element);
    }
}
