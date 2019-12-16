using System;

namespace NeuralNetwork.Visualizer.Preferences.Core
{
   public class Color : IEquatable<Color>
   {
      public static readonly Color Null = new Color(0, 0, 0, 0);
      public static readonly Color Black = new Color(0, 0, 0, 255);
      public static readonly Color White = new Color(255, 255, 255, 255);
      public static readonly Color Gray = new Color(128, 128, 128, 255);
      public static readonly Color LightGray = new Color(211, 211, 211, 255);
      public static readonly Color Orange = new Color(255, 128, 0, 255);
      public static readonly Color Red = new Color(255, 0, 0, 255);
      public static readonly Color LightGreen = new Color(0x90, 0xEE, 0x90, 255);
      public static readonly Color LightPink = new Color(0xFF, 0xB6, 0xC1, 255);

      public Color(byte r, byte g, byte b, byte a)
      {
         this.R = r;
         this.G = g;
         this.B = b;
         this.A = a;
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
         return (left?.Equals(right)) ?? false;
      }

      public static bool operator !=(Color left, Color right)
      {
         return !(left == right);
      }
   }
}
