using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Brushes
{
   public class SolidBrushPreference : IBrushPreference
   {
      public SolidBrushPreference(Color color)
      {
         this.Color = color;
      }

      public Color Color { get; }

      public Brush CreateBrush()
      {
         return (this.Color.ToArgb() != Color.Transparent.ToArgb() ? new SolidBrush(this.Color) : System.Drawing.Brushes.Transparent.Clone() as Brush);
      }
   }
}
