using System;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives
{
   public class Rectangle : IEquatable<Rectangle>
   {
      public Rectangle(Position position, Size size)
      {
         this.Position = position;
         this.Size = size;
      }

      public Position Position { get; }
      public Size Size { get; }

      public override bool Equals(object obj)
      {
         return Equals(obj as Rectangle);
      }

      public bool Equals(Rectangle other)
      {
         return !(other is null)
            && other.Position == this.Position
            && other.Size == this.Size;
      }

      public override int GetHashCode()
      {
         return (this.Position, this.Size).GetHashCode();
      }

      public static bool operator ==(Rectangle left, Rectangle right)
      {
         return (left?.Equals(right)) ?? false;
      }

      public static bool operator !=(Rectangle left, Rectangle right)
      {
         return !(left == right);
      }

      public static Rectangle operator +(Rectangle rectangle, Position position)
      {
         var newPosition = rectangle?.Position + position;

         return new Rectangle(newPosition, rectangle.Size);
      }

      public static Rectangle operator -(Rectangle rectangle, Position position)
      {
         var newPosition = rectangle?.Position - position;

         return new Rectangle(newPosition, rectangle.Size);
      }
   }
}
