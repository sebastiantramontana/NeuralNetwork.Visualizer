using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class LayerTitlePreference : ILayerTitlePreference
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
