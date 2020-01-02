using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface INodePreference : ISelectable2DPreference
   {
      IFormatter<FontLabel> OutputValueFormatter { get; set; }
      byte RoundingDigits { get; set; }
   }
}