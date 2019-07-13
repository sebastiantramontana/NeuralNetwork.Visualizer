using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Selection;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal interface INodeDrawing : IDrawing
   {
      ICanvas Canvas { get; }
      Point EdgeStartPosition { get; }
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

      public Point EdgeStartPosition { get; private set; }
      public ICanvas Canvas { get; private set; }

      public override void Draw(ICanvas canvas)
      {
         if (!_cache.EllipseRectangle.HasValue)
         {
            var side = Math.Min(canvas.MaxWidth, canvas.MaxHeight);

            var x_centered = (canvas.MaxWidth - side) / 2;
            var y_centered = (canvas.MaxHeight - side) / 2;

            _cache.EllipseRectangle = new Rectangle(x_centered, y_centered, side, side);
         }

         RegisterSelectableNodeEllipse(canvas);

         this.Canvas = canvas;
         this.EdgeStartPosition = new Point(_cache.EllipseRectangle.Value.X + _cache.EllipseRectangle.Value.Width, _cache.EllipseRectangle.Value.Y + (_cache.EllipseRectangle.Value.Height / 2));

         bool isSelected = _selectionChecker.IsSelected(this.Element);

         using (var pen = GetPen(isSelected))
         using (var backBrush = GetBrush(isSelected, GetGradientRectangle((int)pen.Width)))
         {
            canvas.DrawEllipse(_cache.EllipseRectangle.Value, pen, backBrush);
         }

         DrawContent(canvas, _cache.EllipseRectangle.Value);
      }

      private Rectangle GetGradientRectangle(int borderWidth)
      {
         var borders2 = borderWidth * 2;
         return Rectangle.Inflate(_cache.EllipseRectangle.Value, borders2, borders2);
      }

      private void RegisterSelectableNodeEllipse(ICanvas canvas)
      {
         var gp = new GraphicsPath();
         gp.AddEllipse(_cache.EllipseRectangle.Value);

         _selectableElementRegister.Register(new RegistrationInfo(this.Element, canvas, new Region(gp), 2));
      }

      private Pen GetPen(bool isSelected)
      {
         return (isSelected)
             ? _preferences.BorderSelected.CreatePen()
             : _preferences.Border.CreatePen();
      }

      private Brush GetBrush(bool isSelected, Rectangle gradientRectangle)
      {
         var brush = (isSelected)
                ? _preferences.BackgroundSelected
                : _preferences.Background;

         if (brush is GradientBrushPreference gradientBrush)
         {
            gradientBrush.Rectangle = gradientRectangle;
         }

         return brush.CreateBrush();
      }

      public NodeBase Node => this.Element;
      protected abstract void DrawContent(ICanvas canvas, Rectangle rect);
   }
}
