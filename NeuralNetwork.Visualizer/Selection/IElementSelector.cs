using NeuralNetwork.Model;
using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Selection
{
    internal interface IElementSelector : IElementSelectionChecker
    {
        IEnumerable<Element> SelectedElements { get; }

        Element SelectOnly(Point location);
        Element AddToSelection(Point location);
        Element Unselect(Point location);
        void UnselectAll();
    }
}
