using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using System.ComponentModel;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface IPreference : INotifyPropertyChanged
   {
      bool Selectable { get; set; }
      bool AutoRedrawOnChanges { get; set; }
      byte NodeMargins { get; set; }
      RenderQuality Quality { get; set; }

      INodePreference Biases { get; set; }
      IEdgePreference Edges { get; set; }
      INodePreference Inputs { get; set; }
      ILayerPreference Layers { get; set; }
      INeuronPreference Neurons { get; set; }

      FontLabel InputFontLabel { get; set; }
      FontLabel OutputFontLabel { get; set; }
   }
}