using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Drawing.Layer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing
{
   public class Drafter : IDrafter
   {
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly ISelectionResolver _selectionResolver;
      private readonly INeuralNetworkVisualizerControl _control;

      public Drafter(INeuralNetworkVisualizerControl control, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister, ISelectionResolver selectionResolver, IRegionBuilder regionBuilder)
      {
         _selectionChecker = selectionChecker;
         _selectableElementRegister = selectableElementRegister;
         _selectionResolver = selectionResolver;
         _control = control;
         this.RegionBuilder = regionBuilder;
      }

      public IRegionBuilder RegionBuilder { get; }

      public async Task RedrawAsync(ICanvasBuilder canvasBuilder)
      {
         if (!ValidateInputLayer())
            return;

         var zoomedControlSize = await GetZoomedControlSize().ConfigureAwait(false);
         var layerSizes = GetLayerSizes(zoomedControlSize);
         var canvas = canvasBuilder.Build(new Size(zoomedControlSize.Width, layerSizes.Height));

         await DrawLayersAsync(canvas, layerSizes).ConfigureAwait(false);
      }

      private bool ValidateInputLayer()
      {
         var inputLayer = _control.InputLayer;

         if (inputLayer is null)
            return false;

         inputLayer.Validate();

         return true;
      }

      private LayerSizesPreCalc GetLayerSizes(Size zoomedControlSize)
      {
         var layersCount = _control.InputLayer.CountLayers();
         var maxNodes = _control.InputLayer.GetMaxNodeCountInLayer();
         var preferences = _control.Preferences;

         var layerSize = new LayerSizesPreCalc(zoomedControlSize, layersCount, maxNodes, preferences);
         return layerSize;
      }

      private async Task<Size> GetZoomedControlSize()
      {
         var controlSize = await _control.GetSize().ConfigureAwait(false);

         var size = new Size((int)(_control.Zoom * controlSize.Width), (int)(_control.Zoom * controlSize.Height));
         return size;
      }

      private async Task DrawLayersAsync(ICanvas canvas, LayerSizesPreCalc layerSizesPreCalc)
      {
         int x = 0;

         _selectionResolver.SetCurrentRootCanvas(canvas);

         IDictionary<NodeBase, INodeDrawing> previousNodesDic = new Dictionary<NodeBase, INodeDrawing>();

         var inputLayer = _control.InputLayer;
         var preferences = _control.Preferences;

         SimpleNodeSizesPreCalc simpleNodeSizesCache = new SimpleNodeSizesPreCalc();
         NeuronSizesPreCalc neuronCache = new NeuronSizesPreCalc(preferences);
         EdgeSizesPreCalc edgesCache = new EdgeSizesPreCalc();

         for (LayerBase layer = inputLayer; layer != null; layer = layer.Next)
         {
            ILayerDrawing layerDrawing = (layer == inputLayer)
               ? new InputLayerDrawing(layer as InputLayer, preferences, layerSizesPreCalc, simpleNodeSizesCache, _selectionChecker, _selectableElementRegister, this.RegionBuilder) as ILayerDrawing
               : new NeuronLayerDrawing(layer as NeuronLayer, previousNodesDic, canvas, preferences, layerSizesPreCalc, neuronCache, simpleNodeSizesCache, edgesCache, _selectionChecker, _selectableElementRegister, this.RegionBuilder);

            var canvasRect = new Rectangle(new Position(x, 0), new Size(layerSizesPreCalc.Width, layerSizesPreCalc.Height));
            var layerCanvas = new NestedCanvas(canvasRect, canvas);

            await Task.Run(() => layerDrawing.Draw(layerCanvas));

            previousNodesDic = layerDrawing.NodesDrawing.ToDictionary(n => n.Node, n => n);
            x += layerSizesPreCalc.Width;
         }
      }
   }
}
