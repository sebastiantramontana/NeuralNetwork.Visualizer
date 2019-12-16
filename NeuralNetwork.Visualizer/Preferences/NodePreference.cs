using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Core;
using NeuralNetwork.Visualizer.Preferences.Formatting;
using NeuralNetwork.Visualizer.Preferences.Pens;
using NeuralNetwork.Visualizer.Preferences.Text;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class NodePreference
   {
      private IBrush _background;
      public IBrush Background
      {
         get => _background ?? (_background = SolidBrush.Null);
         set => _background = value;
      }

      private IBrush _backgroundSelected;
      public IBrush BackgroundSelected
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
      public Pen Border
      {
         get => _border ?? (_border = Pen.Null);
         set => _border = value;
      }

      private Pen _borderSelected = Pen.BasicFromColor(Color.Orange);
      public Pen BorderSelected
      {
         get => _borderSelected ?? (_borderSelected = Pen.Null);
         set => _borderSelected = value;
      }

      public byte RoundingDigits { get; set; } = 3;
   }
}
