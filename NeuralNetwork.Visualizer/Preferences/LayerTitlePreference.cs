using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Text;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class LayerTitlePreference
   {
      private FontLabel _font;
      public FontLabel Font
      {
         get => _font ?? (_font = FontLabel.Null);
         set => _font = value;
      }

      private IBrush _background;
      public IBrush Background
      {
         get => _background ?? (_background = SolidBrush.Null);
         set => _background = value;
      }

      public int Height { get; set; }
   }
}
