using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Preferences.Formatting;

namespace NeuralNetwork.Visualizer.Preferences
{
   public class NeuronPreference : NodePreference, INeuronPreference
   {
      private IFormatter<FontLabel> _sumValueFormatter;
      public IFormatter<FontLabel> SumValueFormatter
      {
         get => _sumValueFormatter ?? (_sumValueFormatter = new NullFormatter<FontLabel>(FontLabel.Default));
         set => _sumValueFormatter = value;
      }
   }
}
