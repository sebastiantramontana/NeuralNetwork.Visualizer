using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface ILayerTitlePreference
   {
      IBrush Background { get; set; }
      FontLabel Font { get; set; }
      int Height { get; set; }
   }
}