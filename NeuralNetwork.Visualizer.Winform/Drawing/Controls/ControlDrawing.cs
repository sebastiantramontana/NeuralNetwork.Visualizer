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
using System;
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
      private readonly IInvoker _invoker;

      public ControlDrawing(IControlCanvas controlCanvas, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister, ISelectionResolver selectionResolver, IInvoker invoker)
      {
         this.ControlCanvas = controlCanvas;
         _selectionChecker = selectionChecker;
         _selectableElementRegister = selectableElementRegister;
         _selectionResolver = selectionResolver;
         _invoker = invoker;
      }

      public IControlCanvas ControlCanvas { get; }

      public async Task<Image> GetImage()
      {
         return await Task.Run(() => this.ControlCanvas.Image.ToVisualizer());
      }

      public void Redraw()
      {
         if (!this.ControlCanvas.IsReady)
         {
            return;
         }

         RedrawInternalAsync((graph, layersSize) => { DrawLayers(graph, layersSize); return Task.CompletedTask; }).Wait();
      }

      private bool _isDrawing = false; //flag to avoid multiple parallel drawing
      public async Task RedrawAsync()
      {
         if (!this.ControlCanvas.IsReady || _isDrawing)
         {
            return;
         }

         _isDrawing = true;
         await _invoker.SafeInvoke(async () =>
         {
            await RedrawInternalAsync(async (graph, layersSize) => await DrawLayersAsync(graph, layersSize));
         });

         _isDrawing = false;
      }

      private async Task RedrawInternalAsync(Func<Gdi.Graphics, LayerSizesPreCalc, Task> drawLayersAction)
      {
         if (!ValidateInputLayer())
         {
            this.ControlCanvas.SetBlank();
         }
         else
         {
            var graphAndImage = this.ControlCanvas.GetGraphics();

            await drawLayersAction(graphAndImage.Graph, graphAndImage.LayerSizes);
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

      private void DrawLayers(Gdi.Graphics graph, LayerSizesPreCalc layerSizesPreCalc)
      {
         DrawLayersGeneral(graph, layerSizesPreCalc, (layerDrawing, layerCanvas) =>
          {
             layerDrawing.Draw(layerCanvas);
             return Task.CompletedTask;
          }).Wait();
      }

      private async Task DrawLayersAsync(Gdi.Graphics graph, LayerSizesPreCalc layerSizesPreCalc)
      {
         await DrawLayersGeneral(graph, layerSizesPreCalc, async (layerDrawing, layerCanvas) => await Task.Run(() => { layerDrawing.Draw(layerCanvas); }));
      }

      private async Task DrawLayersGeneral(Gdi.Graphics graph, LayerSizesPreCalc layersDrawingSize, Func<ILayerDrawing, ICanvas, Task> drawLayerAction)
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
            ILayerDrawing layerDrawing = null;

            if (layer == inputLayer)
            {
               layerDrawing = new InputLayerDrawing(layer as InputLayer, preferences, layersDrawingSize, simpleNodeSizesCache, _selectionChecker, _selectableElementRegister);
            }
            else
            {
               layerDrawing = new NeuronLayerDrawing(layer as NeuronLayer, previousNodesDic, graphCanvas, preferences, layersDrawingSize, neuronCache, simpleNodeSizesCache, edgesCache, _selectionChecker, _selectableElementRegister);
            }

            var canvasRect = new Rectangle(new Position(x, 0), new Size(layersDrawingSize.Width, layersDrawingSize.Height));
            var layerCanvas = new NestedCanvas(canvasRect, graphCanvas);

            await drawLayerAction(layerDrawing, layerCanvas);

            previousNodesDic = layerDrawing.NodesDrawing.ToDictionary(n => n.Node, n => n);

            x += layersDrawingSize.Width;
         }
      }
   }
}
