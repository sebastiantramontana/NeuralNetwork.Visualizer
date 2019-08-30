using NeuralNetwork.Visualizer.Drawing;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Pens;
using NeuralNetwork.Visualizer.Preferences.Text;
using System;
using System.Drawing;
using Draw = System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class Preference : IDisposable
   {
      private LayerPreference _layers = new LayerPreference
      {
         Background = new SolidBrushPreference(Draw.Color.White),
         Title = new LayerTitlePreference() { Background = new GradientBrushPreference(Draw.Color.LightSteelBlue, Draw.Color.LightSkyBlue, 90), Font = new TextPreference { FontStyle = Draw.FontStyle.Bold }, Height = 20 },
         Border = new SimplePen(Draw.Pens.Black),
         BorderSelected = new SimplePen(Draw.Pens.Orange),
         BackgroundSelected = new SolidBrushPreference(Draw.Color.WhiteSmoke)
      };

      private NodePreference _inputs;
      private NeuronPreference _neurons;
      private NodePreference _biases;
      private EdgePreference _edges;

      private InputFontLabel _inputFontLabel = InputFontLabel.Null;
      public InputFontLabel InputFontLabel
      {
         get => _inputFontLabel ?? InputFontLabel.Null;
         set => _inputFontLabel = value;
      }

      private OutputFontLabel _outputFontLabel = OutputFontLabel.Null;
      public OutputFontLabel OutputFontLabel
      {
         get => _outputFontLabel ?? OutputFontLabel.Null;
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
            Background = new SolidBrushPreference(Draw.Color.FromArgb(240, 255, 240)),
            BackgroundSelected = new SolidBrushPreference(Draw.Color.FromArgb(200, 215, 200)),
            Border = new SimplePen(new Draw.Pen(Draw.Color.FromArgb(216, 230, 173), 3f)),
            BorderSelected = new SimplePen(new Draw.Pen(Draw.Color.FromArgb(166, 180, 123), 3f))
         });
         set => _inputs = value;
      }

      public NeuronPreference Neurons
      {
         get => _neurons ?? (_neurons = new NeuronPreference
         {
            Background = new SolidBrushPreference(Draw.Color.Azure),
            BackgroundSelected = new SolidBrushPreference(Draw.Color.LightSteelBlue),
            Border = new SimplePen(new Draw.Pen(Draw.Color.LightBlue, 3f)),
            BorderSelected = new SimplePen(new Draw.Pen(Draw.Color.DeepSkyBlue, 3f))
         });
         set => _neurons = value;
      }

      public NodePreference Biases
      {
         get => _biases ?? (_biases = new NodePreference
         {
            Background = new SolidBrushPreference(Draw.Color.FromArgb(255, 240, 255)),
            BackgroundSelected = new SolidBrushPreference(Draw.Color.FromArgb(215, 200, 215)),
            Border = new SimplePen(new Draw.Pen(Draw.Color.LightPink, 3f)),
            BorderSelected = new SimplePen(new Draw.Pen(Draw.Color.Pink, 3f))
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

      public void Dispose()
      {
         Destroy.Disposable(ref _inputFontLabel);
         Destroy.Disposable(ref _outputFontLabel);
         Destroy.Disposable(ref _layers);
         Destroy.Disposable(ref _inputs);
         Destroy.Disposable(ref _neurons);
         Destroy.Disposable(ref _biases);
      }
   }
}
