using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Drawing.Canvas.GdiMapping;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Core;
using NeuralNetwork.Visualizer.Preferences.Pens;
using NeuralNetwork.Visualizer.Selection;
using System;
using Gdi = System.Drawing;
using System.Drawing.Drawing2D;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal interface INodeDrawing : IDrawing
   {
      ICanvas Canvas { get; }
      Position EdgeStartPosition { get; }
      NodeBase Node { get; }
   }

   internal abstract class NodeBaseDrawing<TNode> : DrawingBase<TNode>, INodeDrawing where TNode : NodeBase
   {
      private readonly NodePreference _preferences;
      private readonly NodeSizesPreCalc _cache;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly IElementSelectionChecker _selectionChecker;

      internal NodeBaseDrawing(TNode element, NodePreference preferences, NodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker) : base(element)
      {
         _preferences = preferences;
         _cache = cache;
         _selectableElementRegister = selectableElementRegister;
         _selectionChecker = selectionChecker;
      }

      public Position EdgeStartPosition { get; private set; }
      public ICanvas Canvas { get; private set; }

      public override void Draw(ICanvas canvas)
      {
         if (_cache.EllipseRectangle is null)
         {
            var side = Math.Min(canvas.Size.Width, canvas.Size.Height);

            var x_centered = (canvas.Size.Width - side) / 2;
            var y_centered = (canvas.Size.Height - side) / 2;

            _cache.EllipseRectangle = new Rectangle(new Position(x_centered, y_centered), new Size(side, side));
         }

         RegisterSelectableNodeEllipse(canvas);

         this.Canvas = canvas;
         this.EdgeStartPosition = new Position(_cache.EllipseRectangle.Position.X + _cache.EllipseRectangle.Size.Width, _cache.EllipseRectangle.Position.Y + (_cache.EllipseRectangle.Size.Height / 2));

         bool isSelected = _selectionChecker.IsSelected(this.Element);

         var pen = GetPen(isSelected);
         var backBrush = GetBrush(isSelected);
         canvas.DrawEllipse(_cache.EllipseRectangle, pen, backBrush);

         DrawContent(canvas, _cache.EllipseRectangle);
      }

      private void RegisterSelectableNodeEllipse(ICanvas canvas)
      {
         var gp = new GraphicsPath();
         gp.AddEllipse(_cache.EllipseRectangle.ToGdi());

         _selectableElementRegister.Register(new RegistrationInfo(this.Element, canvas, new Gdi.Region(gp), 2));
      }

      private Pen GetPen(bool isSelected)
      {
         return (isSelected)
             ? _preferences.BorderSelected
             : _preferences.Border;
      }

      //TODO: Repeated. Apply DRY
      private IBrush GetBrush(bool isSelected)
      {
         var brush = (isSelected)
                ? _preferences.BackgroundSelected
                : _preferences.Background;

         return brush;
      }

      public NodeBase Node => this.Element;
      protected abstract void DrawContent(ICanvas canvas, Rectangle rect);
   }
}
