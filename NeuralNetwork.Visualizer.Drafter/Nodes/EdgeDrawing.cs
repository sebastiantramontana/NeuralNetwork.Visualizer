using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal class EdgeDrawing : ElementDrawingBase<Edge>
   {
      private readonly IEdgePreference _preferences;
      private readonly Position _fromPosition;
      private readonly Position _toPosition;
      private readonly int _textHeight;
      private readonly EdgeSizesPreCalc _cache;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly IRegionBuilder _regionBuilder;

      internal EdgeDrawing(Edge element, IEdgePreference preferences, Position fromPosition, Position toPosition, int textHeight, EdgeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker, IRegionBuilder regionBuilder) : base(element)
      {
         _preferences = preferences;
         _fromPosition = fromPosition;
         _toPosition = toPosition;
         _textHeight = textHeight;
         _cache = cache;
         _selectableElementRegister = selectableElementRegister;
         _selectionChecker = selectionChecker;
         _regionBuilder = regionBuilder;
      }

      public override void Draw(ICanvas canvas)
      {
         RegisterSelectableConnectorLine(canvas);

         var pen = GetPen();

         canvas.DrawLine(_fromPosition, _toPosition, pen);
         DrawWeight(canvas);
      }

      private void RegisterSelectableConnectorLine(ICanvas canvas)
      {
         var positions = new[]
         {
                new Position(_fromPosition.X - 4 , _fromPosition.Y - 4),
                new Position(_fromPosition.X + 4 , _fromPosition.Y + 4),
                new Position(_toPosition.X + 4 , _toPosition.Y + 4),
                new Position(_toPosition.X - 4 , _toPosition.Y - 4),
         };

         _selectableElementRegister.Register(new RegistrationInfo(this.Element, canvas, _regionBuilder.Polygon(positions), 3));
      }

      private Pen GetPen()
      {
         var isSelected = _selectionChecker.IsSelected(this.Element);
         var formatter = _preferences.GetInfoBySelection(isSelected);
         var pen = formatter.GetFormat(this.Element.Weight);

         return pen;
      }

      private void DrawWeight(ICanvas canvas)
      {
         if (!this.Element.Weight.HasValue)
            return;

         var weightValue = Math.Round(this.Element.Weight.Value, _preferences.RoundingDigits).ToString();
         var sizesPositions = GetSizesPositions();
         var fontLabel = _preferences.WeightFormatter.GetFormat(this.Element.Weight.Value);

         canvas.DrawText(weightValue, fontLabel, sizesPositions.TextRectangle, sizesPositions.Angle);
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

         return (new Rectangle(new Position(x, y), new Size(sizePosValues.TextWidth, _textHeight)), angle);
      }
   }
}
