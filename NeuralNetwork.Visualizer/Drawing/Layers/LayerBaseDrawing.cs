using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Drawing.Nodes;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Core;
using NeuralNetwork.Visualizer.Preferences.Pens;
using NeuralNetwork.Visualizer.Selection;
using System;
using System.Collections.Generic;
using Gdi = System.Drawing;
using System.Linq;
using NeuralNetwork.Visualizer.Drawing.Canvas.GdiMapping;

namespace NeuralNetwork.Visualizer.Drawing.Layers
{
   internal abstract class LayerBaseDrawing<TLayer, TNode> : DrawingBase<TLayer>, ILayerDrawing
        where TLayer : LayerBase<TNode>
        where TNode : NodeBase
   {
      private readonly Preference _preferences;
      private readonly LayerSizesPreCalc _cache;
      private readonly SimpleNodeSizesPreCalc _biasCache;
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly IList<INodeDrawing> _nodesDrawing;

      internal LayerBaseDrawing(TLayer layer, Preference preferences, LayerSizesPreCalc cache, SimpleNodeSizesPreCalc biasCache, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister) : base(layer)
      {
         _preferences = preferences;
         _cache = cache;
         _biasCache = biasCache;
         _selectionChecker = selectionChecker;
         _selectableElementRegister = selectableElementRegister;
         _nodesDrawing = new List<INodeDrawing>(layer.GetAllNodes().Count());
      }

      public IEnumerable<INodeDrawing> NodesDrawing { get { return _nodesDrawing; } }

      public override void Draw(ICanvas canvas)
      {
         var rect = new Rectangle(new Position(0, 0), canvas.Size);
         _selectableElementRegister.Register(new RegistrationInfo(this.Element, canvas, new Gdi.Region(rect.ToGdi()), 1));

         var isSelected = _selectionChecker.IsSelected(this.Element);
         var brush = GetBrush(isSelected);

         var pen = GetPen(isSelected);
         canvas.DrawRectangle(rect, pen, brush);

         DrawTitle(canvas);
         DrawNodes(canvas);
      }

      private Pen GetPen(bool isSelected)
      {
         return (isSelected)
             ? _preferences.Layers.BorderSelected
             : _preferences.Layers.Border;
      }

      private IBrush GetBrush(bool isSelected)
      {
         var brush = (isSelected)
             ? _preferences.Layers.BackgroundSelected
             : _preferences.Layers.Background;

         return brush;
      }

      private void DrawNodes(ICanvas canvas)
      {
         int centeredY = _cache.StartingY + (_cache.TotalNodesHeight - _cache.NodeHeight * this.Element.GetAllNodes().Count()) / 2;
         centeredY = Math.Max(centeredY, _cache.StartingY);

         if (this.Element.Bias != null)
         {
            var biasDrawing = new BiasDrawing(this.Element.Bias, _preferences, _biasCache, _selectableElementRegister, _selectionChecker);
            InternalDrawNode(biasDrawing);
         }

         foreach (var node in this.Element.Nodes)
         {
            var nodeDrawing = CreateDrawingNode(node);
            InternalDrawNode(nodeDrawing);
         }

         void InternalDrawNode(INodeDrawing nodeDrawing)
         {
            _nodesDrawing.Add(nodeDrawing);

            DrawNode(nodeDrawing, canvas, centeredY);
            centeredY += _cache.NodeHeight;
         }
      }

      private void DrawNode(INodeDrawing nodeDrawing, ICanvas parentCanvas, int y)
      {
         var newCanvas = new NestedCanvas(new Rectangle(new Position(_preferences.NodeMargins, y), new Size(_cache.NodeWidth, _cache.NodeEllipseHeight)), parentCanvas);
         nodeDrawing.Draw(newCanvas);
      }

      private void DrawTitle(ICanvas canvas)
      {
         if (_preferences.Layers.Title.Height <= 0)
         {
            return;
         }

         var rectTitle = new Rectangle(new Position(0, 0), new Size(canvas.Size.Width, _preferences.Layers.Title.Height));

         canvas.DrawRectangle(rectTitle, null, _preferences.Layers.Title.Background);
         canvas.DrawText(this.Element.Id, _preferences.Layers.Title.Font, rectTitle);
      }

      protected abstract INodeDrawing CreateDrawingNode(TNode node);
   }
}
