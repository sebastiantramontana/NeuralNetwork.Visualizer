using NeuralNetwork.Model.Nodes;
using NeuralNetworkVisualizer.Drawing.Cache;
using NeuralNetworkVisualizer.Preferences;
using NeuralNetworkVisualizer.Selection;

namespace NeuralNetworkVisualizer.Drawing.Nodes
{
    internal class InputDrawing : SimpleNodeDrawing<Input>
    {
        internal InputDrawing(Input element, Preference preferences, SimpleNodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker) : base(element, preferences.Inputs, cache, selectionChecker, selectableElementRegister)
        {
        }
    }
}
