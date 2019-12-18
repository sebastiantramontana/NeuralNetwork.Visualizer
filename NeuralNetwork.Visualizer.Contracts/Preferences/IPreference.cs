using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface IPreference
   {
      bool AsyncRedrawOnResize { get; set; }
      AutoRedrawMode AutoRedrawMode { get; set; }
      INodePreference Biases { get; set; }
      IEdgePreference Edges { get; set; }
      FontLabel InputFontLabel { get; set; }
      INodePreference Inputs { get; set; }
      ILayerPreference Layers { get; set; }
      INeuronPreference Neurons { get; set; }
      byte NodeMargins { get; set; }
      FontLabel OutputFontLabel { get; set; }
      RenderQuality Quality { get; set; }
      bool Selectable { get; set; }
   }
}