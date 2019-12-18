using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface INodePreference
   {
      IBrush Background { get; set; }
      IBrush BackgroundSelected { get; set; }
      Pen Border { get; set; }
      Pen BorderSelected { get; set; }
      IFormatter<FontLabel> OutputValueFormatter { get; set; }
      byte RoundingDigits { get; set; }
   }
}