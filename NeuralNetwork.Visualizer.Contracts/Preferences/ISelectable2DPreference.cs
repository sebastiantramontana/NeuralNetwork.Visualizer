using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;

namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface ISelectable2DPreference : ISelectablePreference<(Pen Border, IBrush Background)>
   {
      IBrush Background { get; set; }
      IBrush BackgroundSelected { get; set; }
      Pen Border { get; set; }
      Pen BorderSelected { get; set; }
   }
}
