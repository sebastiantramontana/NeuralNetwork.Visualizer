using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Nodes;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Selection;

namespace NeuralNetwork.Visualizer.Drawing.Layers
{
    internal class InputLayerDrawing : LayerBaseDrawing<InputLayer, Input>
    {
        private readonly Preference _preference;
        private readonly SimpleNodeSizesPreCalc _simpleNodeCache;
        private readonly IElementSelectionChecker _selectionChecker;
        private readonly ISelectableElementRegister _selectableElementRegister;

        internal InputLayerDrawing(InputLayer layer, Preference preferences, LayerSizesPreCalc cache, SimpleNodeSizesPreCalc simpleNodeCache, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister) : base(layer, preferences, cache, simpleNodeCache, selectionChecker, selectableElementRegister)
        {
            _preference = preferences;
            _simpleNodeCache = simpleNodeCache;
            _selectionChecker = selectionChecker;
            _selectableElementRegister = selectableElementRegister;
        }

        protected override INodeDrawing CreateDrawingNode(Input node)
        {
            return new InputDrawing(node, _preference, _simpleNodeCache, _selectableElementRegister, _selectionChecker);
        }
    }
}
