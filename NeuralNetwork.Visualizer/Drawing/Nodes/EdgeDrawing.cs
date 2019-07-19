using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Selection;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal class EdgeDrawing : DrawingBase<Edge>
   {
      private readonly EdgePreference _preferences;
      private readonly Point _fromPosition;
      private readonly Point _toPosition;
      private readonly int _textHeight;
      private readonly EdgeSizesPreCalc _cache;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly IElementSelectionChecker _selectionChecker;

      internal EdgeDrawing(Edge element, EdgePreference preferences, Point fromPosition, Point toPosition, int textHeight, EdgeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker) : base(element)
      {
         _preferences = preferences;
         _fromPosition = fromPosition;
         _toPosition = toPosition;
         _textHeight = textHeight;
         _cache = cache;
         _selectableElementRegister = selectableElementRegister;
         _selectionChecker = selectionChecker;
      }

      public override void Draw(ICanvas canvas)
      {
         RegisterSelectableConnectorLine(canvas);

         using (var pen = GetPen(_selectionChecker.IsSelected(this.Element)))
         {
            canvas.DrawLine(_fromPosition, _toPosition, pen);
         }

         DrawWeight(canvas);
      }

      private void RegisterSelectableConnectorLine(ICanvas canvas)
      {
         var gp = new GraphicsPath();
         gp.AddPolygon(new[]
         {
                new Point(_fromPosition.X - 4 , _fromPosition.Y - 4),
                new Point(_fromPosition.X + 4 , _fromPosition.Y + 4),
                new Point(_toPosition.X + 4 , _toPosition.Y + 4),
                new Point(_toPosition.X - 4 , _toPosition.Y - 4),
            });

         gp.CloseFigure();

         _selectableElementRegister.Register(new RegistrationInfo(this.Element, canvas, new Region(gp), 3));
      }

      private Pen GetPen(bool isSelected)
      {
         return (isSelected)
             ? _preferences.WhenSelected.CreatePen()
             : _preferences.Connector.GetFormat(this.Element.Weight);
      }

      private void DrawWeight(ICanvas canvas)
      {
         if (!this.Element.Weight.HasValue)
            return;

         var weightValue = Math.Round(this.Element.Weight.Value, _preferences.RoundingDigits).ToString();
         var sizesPositions = GetSizesPositions();

         using (var valueFormat = _preferences.ValueFormatter.GetFormat(this.Element.Weight.Value))
         {
            canvas.DrawText(weightValue, valueFormat.CreateFontInfo(), sizesPositions.TextRectangle, valueFormat.Brush, valueFormat.Format, sizesPositions.Angle);
         }
      }

      private (Rectangle TextRectangle, float Angle) GetSizesPositions()
      {
         int totalHeight = _toPosition.Y - _fromPosition.Y;
         var sizePosValues = _cache.GetValues(_fromPosition.X, _toPosition.X);

         int y;
         int x;
         float angle = (float)(Math.Atan2(totalHeight, sizePosValues.TotalWidth) * (180d / Math.PI));

         if (angle >= 0) //it depends on connector angle
         {
            y = (int)(_fromPosition.Y + sizePosValues.WidthPortionNear * totalHeight);
            x = sizePosValues.NearX;
         }
         else
         {
            var totalHeightPositive = totalHeight * -1; //turn positive
            y = (int)(_toPosition.Y + Math.Round(totalHeightPositive - (sizePosValues.WidthPortionFar * totalHeightPositive)));
            x = sizePosValues.FarX;
         }

         return (new Rectangle(x, y, sizePosValues.TextWidth, _textHeight), angle);
      }
   }
}
