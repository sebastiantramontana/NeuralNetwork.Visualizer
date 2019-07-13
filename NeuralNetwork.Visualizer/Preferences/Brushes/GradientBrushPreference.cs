using System.Drawing;
using System.Drawing.Drawing2D;

namespace NeuralNetwork.Visualizer.Preferences.Brushes
{
   public class GradientBrushPreference : IBrushPreference
   {
      public GradientBrushPreference(Color color1, Color color2, int angle, Rectangle rectangle)
      {
         this.Color1 = color1;
         this.Color2 = color2;
         this.Angle = angle;
         this.Rectangle = rectangle;
      }

      public Color Color1 { get; }
      public Color Color2 { get; }
      public int Angle { get; }
      public Rectangle Rectangle { get; set; }

      public Brush CreateBrush()
      {
         return new LinearGradientBrush(this.Rectangle, this.Color1, this.Color2, this.Angle);
      }
   }
}
