using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Core;
using NeuralNetwork.Visualizer.Preferences.Text;
using NeuralNetwork.Visualizer.Selection;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal class InputDrawing : SimpleNodeDrawing<Input>
   {
      private readonly Preference _preferences;
      private readonly SimpleNodeSizesPreCalc _cache;

      internal InputDrawing(Input element, Preference preferences, SimpleNodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker) : base(element, preferences.Inputs, cache, selectionChecker, selectableElementRegister)
      {
         _preferences = preferences;
         _cache = cache;
      }

      public override void Draw(ICanvas canvas)
      {
         base.Draw(canvas);
         DrawLabel(canvas);
      }

      private void DrawLabel(ICanvas canvas)
      {
         if (_preferences.InputFontLabel == FontLabel.Null || _cache.EllipseRectangle is null)
            return;

         var fontLabel = _preferences.InputFontLabel;

         if (_cache.InputLabelRectangle is null)
         {
            var rect = _cache.EllipseRectangle;
            var labelWidth = rect.Position.X - _preferences.NodeMargins;
            var labelHeight = fontLabel.Size;
            _cache.InputLabelRectangle = new Rectangle(new Position(0, (rect.Size.Height - labelHeight) / 2), new Size(labelWidth, labelHeight));
         }

         var label = this.Element.Id;

         canvas.DrawText(label, fontLabel, _cache.InputLabelRectangle);
      }
   }
}

