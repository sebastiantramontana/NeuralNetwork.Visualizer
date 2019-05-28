using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Selection;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
    internal class InputDrawing : SimpleNodeDrawing<Input>
    {
        internal InputDrawing(Input element, Preference preferences, SimpleNodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker) : base(element, preferences.Inputs, cache, selectionChecker, selectableElementRegister)
        {
        }
    }
}
