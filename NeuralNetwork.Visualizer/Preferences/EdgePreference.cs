using NeuralNetwork.Visualizer.Preferences.Formatting;
using NeuralNetwork.Visualizer.Preferences.Text;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class EdgePreference
   {
      private IFormatter<TextPreference> _weightFormatter;
      public IFormatter<TextPreference> WeightFormatter
      {
         get => _weightFormatter ?? (_weightFormatter = new BasicFormatter<TextPreference>(() => new TextPreference()));
         set => _weightFormatter = value;
      }

      private IFormatter<Pen> _connectorFormatter = new BasicFormatter<Pen>(() => new Pen(Color.Black));
      public IFormatter<Pen> ConnectorFormatter
      {
         get => _connectorFormatter ?? (_connectorFormatter = new BasicFormatter<Pen>(() => new Pen(Color.Transparent)));
         set => _connectorFormatter = value;
      }

      private IFormatter<Pen> _connectorSelectedFormatter = new BasicFormatter<Pen>(() => new Pen(Color.Orange));
      public IFormatter<Pen> ConnectorSelectedFormatter
      {
         get => _connectorSelectedFormatter ?? (_connectorSelectedFormatter = new BasicFormatter<Pen>(() => new Pen(Color.Transparent)));
         set => _connectorSelectedFormatter = value;
      }

      public byte RoundingDigits { get; set; } = 3;
   }
}
