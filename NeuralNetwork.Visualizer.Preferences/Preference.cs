using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class Preference : IPreference
   {
      private ILayerPreference _layers = new LayerPreference
      {
         Background = SolidBrush.White,
         Title = new LayerTitlePreference() { Background = new GradientBrush(new Color(0xB0, 0xC4, 0xDE, 0xFF), new Color(0x87, 0xCE, 0xFA, 0xFF), 90), Font = new FontLabel(FontLabel.Default, FontStyle.Bold), Height = 20 },
         Border = Pen.BasicFromColor(Color.Black),
         BorderSelected = Pen.BasicFromColor(Color.Orange),
         BackgroundSelected = new SolidBrush(new Color(0xF5, 0xF5, 0xF5, 0xFF))
      };

      private INodePreference _inputs;
      private INeuronPreference _neurons;
      private INodePreference _biases;
      private IEdgePreference _edges;

      private FontLabel _inputFontLabel = FontLabel.Default;
      public FontLabel InputFontLabel
      {
         get => _inputFontLabel ?? FontLabel.Null;
         set => _inputFontLabel = value;
      }

      private FontLabel _outputFontLabel = FontLabel.Default;
      public FontLabel OutputFontLabel
      {
         get => _outputFontLabel ?? FontLabel.Null;
         set => _outputFontLabel = value;
      }

      public ILayerPreference Layers
      {
         get => _layers ?? (_layers = new LayerPreference());
         set => _layers = value;
      }

      public INodePreference Inputs
      {
         get => _inputs ?? (_inputs = new NodePreference
         {
            Background = new SolidBrush(new Color(240, 255, 240, 255)),
            BackgroundSelected = new SolidBrush(new Color(200, 215, 200, 255)),
            Border = new Pen(new SolidBrush(new Color(216, 230, 173, 255)), LineStyle.Solid, 1, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(166, 180, 123, 255)), LineStyle.Solid, 1, Cap.None, Cap.None)
         });
         set => _inputs = value;
      }

      public INeuronPreference Neurons
      {
         get => _neurons ?? (_neurons = new NeuronPreference
         {
            Background = new SolidBrush(new Color(0xF0, 0xFF, 0xFF, 0xFF)),
            BackgroundSelected = new SolidBrush(new Color(0xB0, 0xC4, 0xDE, 0xFF)),
            Border = new Pen(new SolidBrush(new Color(0xAD, 0xD8, 0xE6, 0xFF)), LineStyle.Solid, 1, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(0x00, 0xBF, 0xFF, 0xFF)), LineStyle.Solid, 1, Cap.None, Cap.None)
         });
         set => _neurons = value;
      }

      public INodePreference Biases
      {
         get => _biases ?? (_biases = new NodePreference
         {
            Background = new SolidBrush(new Color(255, 240, 255, 255)),
            BackgroundSelected = new SolidBrush(new Color(215, 200, 215, 255)),
            Border = new Pen(new SolidBrush(new Color(0xFF, 0xB6, 0xC1, 0xFF)), LineStyle.Solid, 1, Cap.None, Cap.None),
            BorderSelected = new Pen(new SolidBrush(new Color(0xFF, 0xC0, 0xCB, 0xFF)), LineStyle.Solid, 1, Cap.None, Cap.None)
         });
         set => _biases = value;
      }

      public IEdgePreference Edges
      {
         get => _edges ?? (_edges = new EdgePreference());
         set => _edges = value;
      }

      public byte NodeMargins { get; set; } = 5;
      public RenderQuality Quality { get; set; } = RenderQuality.Medium;
      public bool AsyncRedrawOnResize { get; set; } = false;
      public AutoRedrawMode AutoRedrawMode { get; set; } = AutoRedrawMode.NoAutoRedraw;
      public bool Selectable { get; set; }
   }
}

