using System;

namespace NeuralNetwork.Visualizer.Preferences.Core
{
   public class Color : IEquatable<Color>
   {
      public static readonly Color Null = new Color(0, 0, 0, 0);
      public Color(byte r, byte g, byte b, byte a)
      {
         R = r;
         G = g;
         B = b;
         A = a;
      }

      public byte R { get; }
      public byte G { get; }
      public byte B { get; }
      public byte A { get; }
      public bool IsTransparent => this.A == 0;
      public override bool Equals(object obj)
      {
         return base.Equals(obj as Color);
      }
      public bool Equals(Color other)
      {
         return !(other is null)
            && other.R == this.R
            && other.G == this.G
            && other.B == this.B
            && other.A == this.A;
      }

      public override int GetHashCode()
      {
         return (this.R, this.G, this.B, this.A).GetHashCode();
      }

      public static bool operator ==(Color left, Color right)
      {
         return left?.Equals(right) ?? false;
      }

      public static bool operator !=(Color left, Color right)
      {
         return !(left == right);
      }
   }
}
