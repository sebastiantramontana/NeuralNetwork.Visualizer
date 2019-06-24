using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Drawing.Nodes;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Selection;
using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Drawing.Layers
{
    internal class NeuronLayerDrawing : LayerBaseDrawing<NeuronLayer, Neuron>
    {
        private readonly IDictionary<NodeBase, INodeDrawing> _previousNodes;
        private readonly ICanvas _edgesCanvas;
        private readonly Preference _preferences;
        private readonly NeuronSizesPreCalc _neuronCache;
        private readonly EdgeSizesPreCalc _edgesCache;
        private readonly IElementSelectionChecker _selectionChecker;
        private readonly ISelectableElementRegister _selectableElementRegister;

        internal NeuronLayerDrawing(NeuronLayer layer, IDictionary<NodeBase, INodeDrawing> previousNodes, ICanvas edgesCanvas, Preference preferences, LayerSizesPreCalc cache, NeuronSizesPreCalc neuronCache, SimpleNodeSizesPreCalc biasCache, EdgeSizesPreCalc edgesCache, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister) : base(layer, preferences, cache, biasCache, selectionChecker, selectableElementRegister)
        {
            _previousNodes = previousNodes;
            _edgesCanvas = edgesCanvas;
            _preferences = preferences;
            _neuronCache = neuronCache;
            _edgesCache = edgesCache;
            this._selectionChecker = selectionChecker;
            this._selectableElementRegister = selectableElementRegister;
        }

        protected override INodeDrawing CreateDrawingNode(Neuron node)
        {
            return new NeuronDrawing(node, _previousNodes, _edgesCanvas, _preferences, _neuronCache, _edgesCache, _selectionChecker, _selectableElementRegister);
        }
    }
}
