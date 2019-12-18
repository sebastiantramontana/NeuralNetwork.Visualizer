using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface IEdgePreference
   {
      IFormatter<Pen> ConnectorFormatter { get; set; }
      IFormatter<Pen> ConnectorSelectedFormatter { get; set; }
      byte RoundingDigits { get; set; }
      IFormatter<FontLabel> WeightFormatter { get; set; }
   }
}