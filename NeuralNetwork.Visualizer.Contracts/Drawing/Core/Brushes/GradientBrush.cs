using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes
{
   public class GradientBrush : BrushBase<GradientBrush>
   {
      public GradientBrush(Color color1, Color color2, int angle)
      {
         this.Color1 = color1;
         this.Color2 = color2;
         this.Angle = angle;
      }

      public Color Color1 { get; }
      public Color Color2 { get; }
      public int Angle { get; }

      public override bool Equals(GradientBrush other)
      {
         return !(other is null)
            && other.Color1 == this.Color1
            && other.Color2 == this.Color2
            && other.Angle == this.Angle;
      }

      public override int GetHashCode()
      {
         return (this.Color1, this.Color2, this.Angle).GetHashCode();
      }
   }
}
