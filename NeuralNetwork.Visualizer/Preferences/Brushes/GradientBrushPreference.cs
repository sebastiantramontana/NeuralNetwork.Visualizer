using System.Drawing;
using System.Drawing.Drawing2D;

namespace NeuralNetwork.Visualizer.Preferences.Brushes
{
   public class GradientBrushPreference : IBrush
   {
      public GradientBrushPreference(Color color1, Color color2, int angle)
      {
         this.Color1 = color1;
         this.Color2 = color2;
         this.Angle = angle;
      }

      public Color Color1 { get; }
      public Color Color2 { get; }
      public int Angle { get; }

      public Brush CreateBrush(Rectangle rectangle)
      {
         return new LinearGradientBrush(rectangle, this.Color1, this.Color2, this.Angle);
      }
   }
}
