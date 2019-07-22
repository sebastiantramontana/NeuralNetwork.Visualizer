using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Brushes
{
   public class SolidBrushPreference : IBrush
   {
      public SolidBrushPreference(Color color)
      {
         this.Color = color;
      }

      public Color Color { get; }

      public Brush CreateBrush(Rectangle rectangle)
      {
         return this.Color.A > 0 ? new SolidBrush(this.Color) : System.Drawing.Brushes.Transparent.Clone() as Brush;
      }
   }
}
