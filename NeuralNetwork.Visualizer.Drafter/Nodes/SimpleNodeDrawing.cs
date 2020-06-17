using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal abstract class SimpleNodeDrawing<TNode> : NodeBaseDrawing<TNode> where TNode : NodeBase
   {
      private readonly INodePreference _preferences;
      private readonly SimpleNodeSizesPreCalc _cache;

      internal SimpleNodeDrawing(TNode element, INodePreference preferences, SimpleNodeSizesPreCalc cache, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister, IRegionBuilder regionBuilder) : base(element, preferences, cache, selectableElementRegister, selectionChecker, regionBuilder)
      {
         _preferences = preferences;
         _cache = cache;
      }

      protected override Task DrawContent(ICanvas canvas, Rectangle rect)
      {
         if (!this.Element.OutputValue.HasValue)
            return Task.CompletedTask;

         var outputRectangle = GetOutputRectangle(rect);
         var font = _preferences.OutputValueFormatter.GetFormat(this.Element.OutputValue.Value);

         return canvas.DrawText(Math.Round(this.Element.OutputValue.Value, _preferences.RoundingDigits).ToString(), font, outputRectangle);
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
