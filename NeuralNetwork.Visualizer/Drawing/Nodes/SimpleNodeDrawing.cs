using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Preferences.Core;
using NeuralNetwork.Visualizer.Selection;
using System;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal abstract class SimpleNodeDrawing<TNode> : NodeBaseDrawing<TNode> where TNode : NodeBase
   {
      private readonly NodePreference _preferences;
      private readonly SimpleNodeSizesPreCalc _cache;

      internal SimpleNodeDrawing(TNode element, NodePreference preferences, SimpleNodeSizesPreCalc cache, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister) : base(element, preferences, cache, selectableElementRegister, selectionChecker)
      {
         _preferences = preferences;
         _cache = cache;
      }

      protected override void DrawContent(ICanvas canvas, Rectangle rect)
      {
         if (!this.Element.OutputValue.HasValue)
            return;

         var outputRectangle = GetOutputRectangle(rect);
         var font = _preferences.OutputValueFormatter.GetFormat(this.Element.OutputValue.Value);

         canvas.DrawText(Math.Round(this.Element.OutputValue.Value, _preferences.RoundingDigits).ToString(), font, outputRectangle);
      }

      private Rectangle GetOutputRectangle(Rectangle rect)
      {
         if (_cache.OutputSize is null)
         {
            var side = rect.Size.Width;
            var div3 = side / 3d;

            _cache.YCenteringOffeset = side / 2 - div3 / 2;
            _cache.OutputSize = new Size(side, (int)div3);
         }

         var outputRectangle = new Rectangle(new Position(rect.Position.X, rect.Position.Y + (int)_cache.YCenteringOffeset), _cache.OutputSize);

         return outputRectangle;
      }
   }
}
