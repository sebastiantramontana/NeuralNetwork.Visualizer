using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface INeuronPreference : INodePreference
   {
      IFormatter<FontLabel> SumValueFormatter { get; set; }
   }
}