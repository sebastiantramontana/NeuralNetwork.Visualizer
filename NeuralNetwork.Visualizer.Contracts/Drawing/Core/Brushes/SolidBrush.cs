using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes
{
   public class SolidBrush : BrushBase<SolidBrush>
   {
      public static readonly SolidBrush Null = new SolidBrush(Color.Null);
      public static readonly SolidBrush Black = new SolidBrush(Color.Black);
      public static readonly SolidBrush White = new SolidBrush(Color.White);
      public static readonly SolidBrush Orange = new SolidBrush(Color.Orange);

      public SolidBrush(Color color)
      {
         this.Color = color;
      }

      public Color Color { get; }

      public override bool Equals(SolidBrush other)
      {
         return !(other is null)
            && other.Color == this.Color;
      }

      public override int GetHashCode()
      {
         return this.Color.GetHashCode();
      }
   }
}
