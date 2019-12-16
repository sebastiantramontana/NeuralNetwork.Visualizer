using NeuralNetwork.Visualizer.Drawing;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Core;
using NeuralNetwork.Visualizer.Preferences.Pens;
using NeuralNetwork.Visualizer.Preferences.Text;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class Preference
   {
      private LayerPreference _layers = new LayerPreference
      {
         Background = SolidBrush.White,
         Title = new LayerTitlePreference() { Background = new GradientBrush(new Color(0xB0C4DE), new Color(0x87CEFA), 90), Font = new FontLabel(FontLabel.Default, FontStyle.Bold), Height = 20 },
         Border = Pen.BasicFromColor(Color.Black),
         BorderSelected = Pen.BasicFromColor(Color.Orange),
         BackgroundSelected = new SolidBrush(new Color(0xF5F5F5))
      };

      private NodePreference _inputs;
      private NeuronPreference _neurons;
      private NodePreference _biases;
      private EdgePreference _edges;

      private FontLabel _inputFontLabel = FontLabel.Null;
      public FontLabel InputFontLabel
      {
         get => _inputFontLabel ?? FontLabel.Null;
         set => _inputFontLabel = value;
      }

      private FontLabel _outputFontLabel = FontLabel.Null;
      public FontLabel OutputFontLabel
      {
         get => _outputFontLabel ?? FontLabel.Null;
         set => _outputFontLabel = value;
      }

      public LayerPreference Layers
      {
         get => _layers ?? (_layers = new LayerPreference());
         set => _layers = value;
      }

      public NodePreference Inputs
      {
         get => _inputs ?? (_inputs = new NodePreference
         {
            Background = new SolidBrush(new Color(240, 255, 240, 255)),
            BackgroundSelected = new SolidBrush(new Color(200, 215, 200, 255)),
            Border = new Pen(new SolidBrush(new Color(216, 230, 173, 255)), LineStyle.Solid, 3, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(166, 180, 123, 255)), LineStyle.Solid, 3, Cap.None, Cap.None)
         });
         set => _inputs = value;
      }

      public NeuronPreference Neurons
      {
         get => _neurons ?? (_neurons = new NeuronPreference
         {
            Background = new SolidBrush(new Color(0xF0FFFF)),
            BackgroundSelected = new SolidBrush(new Color(0xB0C4DE)),
            Border = new Pen(new SolidBrush(new Color(0xADD8E6)), LineStyle.Solid, 3, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(0x00BFFF)), LineStyle.Solid, 3, Cap.None, Cap.None)
         });
         set => _neurons = value;
      }

      public NodePreference Biases
      {
         get => _biases ?? (_biases = new NodePreference
         {
            Background = new SolidBrush(new Color(255, 240, 255, 255)),
            BackgroundSelected = new SolidBrush(new Color(215, 200, 215, 255)),
            Border = new Pen(new SolidBrush(new Color(0xFFB6C1)), LineStyle.Solid, 3, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(0xFFC0CB)), LineStyle.Solid, 3, Cap.None, Cap.None)
         });
         set => _biases = value;
      }

      public EdgePreference Edges
      {
         get => _edges ?? (_edges = new EdgePreference());
         set => _edges = value;
      }

      public byte NodeMargins { get; set; } = 5;
      public RenderQuality Quality { get; set; } = RenderQuality.Medium;
      public bool AsyncRedrawOnResize { get; set; } = false;
      public AutoRedrawMode AutoRedrawMode { get; set; } = AutoRedrawMode.NoAutoRedraw;
   }
}

