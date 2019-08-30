using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Drawing.Cache;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Text;
using NeuralNetwork.Visualizer.Selection;
using System.Drawing;

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
         if (_preferences.InputFontLabel == InputFontLabel.Null || !_cache.EllipseRectangle.HasValue)
            return;

         var fontLabel = _preferences.InputFontLabel;

         if (!_cache.InputLabelRectangle.HasValue)
         {
            var rect = _cache.EllipseRectangle.Value;
            var labelWidth = rect.X - _preferences.NodeMargins;
            var labelHeight = fontLabel.Size;
            _cache.InputLabelRectangle = new Rectangle(0, (rect.Height - labelHeight) / 2, labelWidth, labelHeight);
         }

         var label = this.Element.Id;

         var fontInfo = new FontInfo(fontLabel.Family, fontLabel.Style);
         canvas.DrawText(label, fontInfo, _cache.InputLabelRectangle.Value, new SolidBrushPreference(fontLabel.Color), fontLabel.Format);
      }


   }
}

