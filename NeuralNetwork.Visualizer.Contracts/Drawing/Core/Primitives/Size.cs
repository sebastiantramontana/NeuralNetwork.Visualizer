using System;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives
{
   public class Size : IEquatable<Size>
   {
      public static readonly Size Null = new Size(0, 0);

      public Size(int width, int height)
      {
         this.Width = width;
         this.Height = height;
      }

      public int Width { get; }
      public int Height { get; }
      public bool IsNull => this == Size.Null;

      public override bool Equals(object obj)
      {
         return Equals(obj as Size);
      }

      public bool Equals(Size other)
      {
         return !(other is null)
            && other.Width == this.Width
            && other.Height == this.Height;
      }

      public override int GetHashCode()
      {
         return (this.Width, this.Height).GetHashCode();
      }

      public static Size Min(Size size1, Size size2)
      {
         var width = Math.Min(size1.Width, size2.Width);
         var height = Math.Min(size1.Height, size2.Height);

         return new Size(width, height);
      }

      public static bool operator ==(Size left, Size right)
      {
         return (left?.Equals(right)) ?? false;
      }

      public static bool operator !=(Size left, Size right)
      {
         return !(left == right);
      }

      public static Size operator +(Size size1, Size size2)
      {
         var width = (size1?.Width ?? 0) + (size2?.Width ?? 0);
         var height = (size1?.Height ?? 0) + (size2?.Height ?? 0);

         return new Size(width, height);
      }

      public static Size operator +(Size size, int amount)
      {
         var width = (size?.Width ?? 0) + amount;
         var height = (size?.Height ?? 0) + amount;

         return new Size(width, height);
      }

      public static Size operator -(Size size1, Size size2)
      {
         var width = (size1?.Width ?? 0) - (size2?.Width ?? 0);
         var height = (size1?.Height ?? 0) - (size2?.Height ?? 0);

         return new Size(width, height);
      }

      public static Size operator -(Size size, int amount)
      {
         return size + -amount;
      }
   }
}
