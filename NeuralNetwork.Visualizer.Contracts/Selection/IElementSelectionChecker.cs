using NeuralNetwork.Model;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
    internal interface IElementSelectionChecker
    {
        bool IsSelected(Element element);
    }
}
