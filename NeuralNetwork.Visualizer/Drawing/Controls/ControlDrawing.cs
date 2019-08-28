using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Drawing.Layers;
using NeuralNetwork.Visualizer.Drawing.Nodes;
using NeuralNetwork.Visualizer.Selection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
   internal class ControlDrawing : IControlDrawing
   {
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly ISelectionResolver _selectionResolver;

      public ControlDrawing(IControlCanvas controlCanvas, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister, ISelectionResolver selectionResolver)
      {
         this.ControlCanvas = controlCanvas;
         _selectionChecker = selectionChecker;
         _selectableElementRegister = selectableElementRegister;
         _selectionResolver = selectionResolver;
      }

      public IControlCanvas ControlCanvas { get; }

      public Image GetImage()
      {
         return this.ControlCanvas.Image;
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
         await this.ControlCanvas.SafeInvoke(async () =>
         {
            await RedrawInternalAsync(async (graph, layersSize) => await DrawLayersAsync(graph, layersSize));
         });

         _isDrawing = false;
      }

      private async Task RedrawInternalAsync(Func<Graphics, LayerSizesPreCalc, Task> drawLayersAction)
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

      private void DrawLayers(Graphics graph, LayerSizesPreCalc layerSizesPreCalc)
      {
         DrawLayersGeneral(graph, layerSizesPreCalc, (layerDrawing, layerCanvas) =>
          {
             layerDrawing.Draw(layerCanvas);
             return Task.CompletedTask;
          }).Wait();
      }

      private async Task DrawLayersAsync(Graphics graph, LayerSizesPreCalc layerSizesPreCalc)
      {
         await DrawLayersGeneral(graph, layerSizesPreCalc, async (layerDrawing, layerCanvas) =>
         {
            await Task.Factory.StartNew((obj) =>
               {
                  var layerAndCanvas = obj as Tuple<ILayerDrawing, ICanvas>;
                  layerAndCanvas.Item1.Draw(layerAndCanvas.Item2);
               }, new Tuple<ILayerDrawing, ICanvas>(layerDrawing, layerCanvas));
         });
      }

      private async Task DrawLayersGeneral(Graphics graph, LayerSizesPreCalc layersDrawingSize, Func<ILayerDrawing, ICanvas, Task> drawLayerAction)
      {
         var graphCanvas = new GraphicsCanvas(graph, this.ControlCanvas.Size.Width, this.ControlCanvas.Size.Height);
         int x = 0;

         _selectionResolver.SetCurrentRootCanvas(graphCanvas);

         IDictionary<NodeBase, INodeDrawing> previousNodesDic = new Dictionary<NodeBase, INodeDrawing>();

         var inputLayer = this.ControlCanvas.Control.InputLayer;
         var preferences = this.ControlCanvas.Control.Preferences;

         SimpleNodeSizesPreCalc simpleNodesCache = new SimpleNodeSizesPreCalc();
         NeuronSizesPreCalc neuronCache = new NeuronSizesPreCalc(preferences);
         EdgeSizesPreCalc edgesCache = new EdgeSizesPreCalc();

         for (LayerBase layer = inputLayer; layer != null; layer = layer.Next)
         {
            ILayerDrawing layerDrawing = null;

            if (layer == inputLayer)
            {
               layerDrawing = new InputLayerDrawing(layer as InputLayer, preferences, layersDrawingSize, simpleNodesCache, _selectionChecker, _selectableElementRegister);
            }
            else
            {
               layerDrawing = new NeuronLayerDrawing(layer as NeuronLayer, previousNodesDic, graphCanvas, preferences, layersDrawingSize, neuronCache, simpleNodesCache, edgesCache, _selectionChecker, _selectableElementRegister);
            }

            var canvasRect = new Rectangle(x, 0, layersDrawingSize.Width, layersDrawingSize.Height);
            var layerCanvas = new NestedCanvas(canvasRect, graphCanvas);

            await drawLayerAction(layerDrawing, layerCanvas);

            previousNodesDic = layerDrawing.NodesDrawing.ToDictionary(n => n.Node, n => n);

            x += layersDrawingSize.Width;
         }
      }
   }
}
