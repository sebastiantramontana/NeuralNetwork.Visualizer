using NeuralNetwork.Infrastructure.Winform;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using NeuralNetwork.Visualizer.Winform.Drawing.Layers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   internal class ControlDrawing : IControlDrawing
   {
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly ISelectionResolver _selectionResolver;
      private readonly IInvoker _winformInvoker;

      public ControlDrawing(IControlCanvas controlCanvas, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister, ISelectionResolver selectionResolver, IInvoker winformInvoker)
      {
         this.ControlCanvas = controlCanvas;
         _selectionChecker = selectionChecker;
         _selectableElementRegister = selectableElementRegister;
         _selectionResolver = selectionResolver;
         _winformInvoker = winformInvoker;
      }

      public IControlCanvas ControlCanvas { get; }

      public async Task<Image> GetImage()
      {
         return await Task.Run(() => this.ControlCanvas.Image.ToVisualizer());
      }

      private bool _isDrawing = false; //flag to avoid multiple parallel drawing
      public async Task RedrawAsync()
      {
         if (!this.ControlCanvas.IsReady || _isDrawing)
         {
            return;
         }

         _isDrawing = true;
         await _winformInvoker.SafeInvoke(async () =>
         {
            await RedrawInternalAsync();
         });

         _isDrawing = false;
      }

      private async Task RedrawInternalAsync()
      {
         if (!ValidateInputLayer())
         {
            this.ControlCanvas.SetBlank();
         }
         else
         {
            var graphAndImage = this.ControlCanvas.GetGraphics();

            await DrawLayersAsync(graphAndImage.Graph, graphAndImage.LayerSizes);
            this.ControlCanvas.Image = graphAndImage.Image;

            Destroy.Disposable(ref graphAndImage.Graph);
         }
      }

      private bool ValidateInputLayer()
      {
         var inputLayer = this.ControlCanvas.Control.InputLayer;

         if (inputLayer != null)
         {
            inputLayer.Validate();
            return true;
         }

         return false;
      }

      private async Task DrawLayersAsync(Gdi.Graphics graph, LayerSizesPreCalc layerSizesPreCalc)
      {
         var graphCanvas = new GraphicsCanvas(graph, this.ControlCanvas.Size);
         int x = 0;

         _selectionResolver.SetCurrentRootCanvas(graphCanvas);

         IDictionary<NodeBase, INodeDrawing> previousNodesDic = new Dictionary<NodeBase, INodeDrawing>();

         var inputLayer = this.ControlCanvas.Control.InputLayer;
         var preferences = this.ControlCanvas.Control.Preferences;

         SimpleNodeSizesPreCalc simpleNodeSizesCache = new SimpleNodeSizesPreCalc();
         NeuronSizesPreCalc neuronCache = new NeuronSizesPreCalc(preferences);
         EdgeSizesPreCalc edgesCache = new EdgeSizesPreCalc();

         for (LayerBase layer = inputLayer; layer != null; layer = layer.Next)
         {
            ILayerDrawing layerDrawing = (layer == inputLayer)
               ? new InputLayerDrawing(layer as InputLayer, preferences, layerSizesPreCalc, simpleNodeSizesCache, _selectionChecker, _selectableElementRegister) as ILayerDrawing
               : new NeuronLayerDrawing(layer as NeuronLayer, previousNodesDic, graphCanvas, preferences, layerSizesPreCalc, neuronCache, simpleNodeSizesCache, edgesCache, _selectionChecker, _selectableElementRegister);

            var canvasRect = new Rectangle(new Position(x, 0), new Size(layerSizesPreCalc.Width, layerSizesPreCalc.Height));
            var layerCanvas = new NestedCanvas(canvasRect, graphCanvas);

            await Task.Run(() => layerDrawing.Draw(layerCanvas));

            previousNodesDic = layerDrawing.NodesDrawing.ToDictionary(n => n.Node, n => n);
            x += layerSizesPreCalc.Width;
         }
      }
   }
}
