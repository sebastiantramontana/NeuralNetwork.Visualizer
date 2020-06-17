using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal class InputDrawing : SimpleNodeDrawing<Input>
   {
      private readonly IPreference _preferences;
      private readonly SimpleNodeSizesPreCalc _cache;

      internal InputDrawing(Input element, IPreference preferences, SimpleNodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker, IRegionBuilder regionBuilder) : base(element, preferences.Inputs, cache, selectionChecker, selectableElementRegister, regionBuilder)
      {
         _preferences = preferences;
         _cache = cache;
      }

      public override async Task Draw(ICanvas canvas)
      {
         await base.Draw(canvas);
         await DrawLabel(canvas);
      }

      private Task DrawLabel(ICanvas canvas)
      {
         if (_preferences.InputFontLabel == FontLabel.Null || _cache.EllipseRectangle is null)
            return Task.CompletedTask;

         var fontLabel = _preferences.InputFontLabel;

         if (_cache.InputLabelRectangle is null)
         {
            var rect = _cache.EllipseRectangle;
            var labelWidth = rect.Position.X - _preferences.NodeMargins;
            var labelHeight = fontLabel.Size;
            _cache.InputLabelRectangle = new Rectangle(new Position(0, (rect.Size.Height - labelHeight) / 2), new Size(labelWidth, labelHeight));
         }

         var label = this.Element.Id;

         return canvas.DrawText(label, fontLabel, _cache.InputLabelRectangle);
      }
   }
}

