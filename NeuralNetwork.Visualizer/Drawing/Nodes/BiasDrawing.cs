using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Selection;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
    internal class BiasDrawing : SimpleNodeDrawing<Bias>
    {
        internal BiasDrawing(Bias element, Preference preferences, SimpleNodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker) : base(element, preferences.Biases, cache, selectionChecker, selectableElementRegister)
        {
        }
    }
}
