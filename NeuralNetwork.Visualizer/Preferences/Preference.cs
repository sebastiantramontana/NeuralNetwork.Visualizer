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
         Title = new LayerTitlePreference() { Background = new GradientBrush(new Color(0xB0, 0xC4, 0xDE, 0xFF), new Color(0x87, 0xCE, 0xFA, 0xFF), 90), Font = new FontLabel(FontLabel.Default, FontStyle.Bold), Height = 20 },
         Border = Pen.BasicFromColor(Color.Black),
         BorderSelected = Pen.BasicFromColor(Color.Orange),
         BackgroundSelected = new SolidBrush(new Color(0xF5, 0xF5, 0xF5, 0xFF))
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
            Background = new SolidBrush(new Color(0xF0, 0xFF, 0xFF, 0xFF)),
            BackgroundSelected = new SolidBrush(new Color(0xB0, 0xC4, 0xDE, 0xFF)),
            Border = new Pen(new SolidBrush(new Color(0xAD, 0xD8, 0xE6, 0xFF)), LineStyle.Solid, 3, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(0x00, 0xBF, 0xFF, 0xFF)), LineStyle.Solid, 3, Cap.None, Cap.None)
         });
         set => _neurons = value;
      }

      public NodePreference Biases
      {
         get => _biases ?? (_biases = new NodePreference
         {
            Background = new SolidBrush(new Color(255, 240, 255, 255)),
            BackgroundSelected = new SolidBrush(new Color(215, 200, 215, 255)),
            Border = new Pen(new SolidBrush(new Color(0xFF, 0xB6, 0xC1, 0xFF)), LineStyle.Solid, 3, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(0xFF, 0xC0, 0xCB, 0xFF)), LineStyle.Solid, 3, Cap.None, Cap.None)
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

