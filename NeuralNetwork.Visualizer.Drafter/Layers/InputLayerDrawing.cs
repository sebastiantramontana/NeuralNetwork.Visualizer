using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Drawing.Nodes;

namespace NeuralNetwork.Visualizer.Drawing.Layer
{
   internal class InputLayerDrawing : LayerBaseDrawing<InputLayer, Input>
   {
      private readonly IPreference _preference;
      private readonly SimpleNodeSizesPreCalc _simpleNodeSizesCache;
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly IRegionBuilder _regionBuilder;

      internal InputLayerDrawing(InputLayer layer, IPreference preferences, LayerSizesPreCalc cache, SimpleNodeSizesPreCalc simpleNodeSizesCache, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister, IRegionBuilder regionBuilder) : base(layer, preferences, cache, simpleNodeSizesCache, selectionChecker, selectableElementRegister, regionBuilder)
      {
         _preference = preferences;
         _simpleNodeSizesCache = simpleNodeSizesCache;
         _selectionChecker = selectionChecker;
         _selectableElementRegister = selectableElementRegister;
         _regionBuilder = regionBuilder;
      }

      protected override INodeDrawing CreateDrawingNode(Input node)
      {
         return new InputDrawing(node, _preference, _simpleNodeSizesCache, _selectableElementRegister, _selectionChecker, _regionBuilder);
      }
   }
}
