using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Preferences.Formatting;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class EdgePreference : IEdgePreference
   {
      private IFormatter<FontLabel> _weightFormatter;
      public IFormatter<FontLabel> WeightFormatter
      {
         get => _weightFormatter ?? (_weightFormatter = new NullFormatter<FontLabel>(FontLabel.Default));
         set => _weightFormatter = value;
      }

      private IFormatter<Pen> _connectorFormatter = new NullFormatter<Pen>(Pen.BasicFromColor(Color.Black));
      public IFormatter<Pen> ConnectorFormatter
      {
         get => _connectorFormatter ?? (_connectorFormatter = new NullFormatter<Pen>(Pen.Null));
         set => _connectorFormatter = value;
      }

      private IFormatter<Pen> _connectorSelectedFormatter = new NullFormatter<Pen>(Pen.BasicFromColor(Color.Orange));
      public IFormatter<Pen> ConnectorSelectedFormatter
      {
         get => _connectorSelectedFormatter ?? (_connectorSelectedFormatter = new NullFormatter<Pen>(Pen.Null));
         set => _connectorSelectedFormatter = value;
      }

      public byte RoundingDigits { get; set; } = 3;
   }
}
