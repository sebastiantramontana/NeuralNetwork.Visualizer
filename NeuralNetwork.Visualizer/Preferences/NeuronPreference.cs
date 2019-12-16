using NeuralNetwork.Visualizer.Preferences.Formatting;
using NeuralNetwork.Visualizer.Preferences.Text;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class NeuronPreference : NodePreference
   {
      private IFormatter<FontLabel> _sumValueFormatter;
      public IFormatter<FontLabel> SumValueFormatter
      {
         get => _sumValueFormatter ?? (_sumValueFormatter = new NullFormatter<FontLabel>(FontLabel.Null));
         set => _sumValueFormatter = value;
      }
   }
}
