using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Preferences.Formatting;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class NodePreference : Selectable2DPreferenceBase
   {
      private IBrush _background;
      public override IBrush Background
      {
         get => _background ?? (_background = SolidBrush.Null);
         set => _background = value;
      }

      private IBrush _backgroundSelected;
      public override IBrush BackgroundSelected
      {
         get => _backgroundSelected ?? (_backgroundSelected = SolidBrush.Null);
         set => _backgroundSelected = value;
      }

      private IFormatter<FontLabel> _outputValueFormatter;
      public IFormatter<FontLabel> OutputValueFormatter
      {
         get => _outputValueFormatter ?? (_outputValueFormatter = new NullFormatter<FontLabel>(FontLabel.Default));
         set => _outputValueFormatter = value;
      }

      private Pen _border = Pen.BasicFromColor(Color.Black);
      public override Pen Border
      {
         get => _border ?? (_border = Pen.Null);
         set => _border = value;
      }

      private Pen _borderSelected = Pen.BasicFromColor(Color.Orange);
      public override Pen BorderSelected
      {
         get => _borderSelected ?? (_borderSelected = Pen.Null);
         set => _borderSelected = value;
      }

      public byte RoundingDigits { get; set; } = 3;
   }
}
