using NeuralNetwork.Visualizer.Preferences.Formatting;
using NeuralNetwork.Visualizer.Preferences.Text;

namespace NeuralNetwork.Visualizer.Preferences
{
    public class NeuronPreference : NodePreference
    {
        private IFormatter<TextPreference> _sumValueFormatter;
        public IFormatter<TextPreference> SumValueFormatter
        {
            get => _sumValueFormatter ?? (_sumValueFormatter = new BasicFormatter<TextPreference>(() => new TextPreference()));
            set => _sumValueFormatter = value;
        }
    }
}
