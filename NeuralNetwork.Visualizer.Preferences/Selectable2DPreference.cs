using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Preferences;

namespace NeuralNetwork.Visualizer.Preferences
{
   public abstract class Selectable2DPreferenceBase : ISelectable2DPreference
   {
      public abstract IBrush Background { get; set; }
      public abstract IBrush BackgroundSelected { get; set; }
      public abstract Pen Border { get; set; }
      public abstract Pen BorderSelected { get; set; }

      public (Pen Border, IBrush Background) GetInfoBySelection(bool isSelected)
      {
         (Pen, IBrush) info = (isSelected)
            ? (this.BorderSelected, this.BackgroundSelected)
            : (this.Border, this.Background);

         return info;
      }
   }
}
