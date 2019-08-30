using System;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Text
{
   public abstract class FontLabel : IEquatable<FontLabel>, IDisposable
   {
      protected FontLabel(string family, FontStyle style, int size, Color color, StringFormat format)
      {
         this.Family = family;
         this.Style = style;
         this.Size = size;
         this.Color = color;
         _format = format?.Clone() as StringFormat;
      }

      public string Family { get; }
      public FontStyle Style { get; }
      public int Size { get; }
      public Color Color { get; }

      private StringFormat _format;
      public StringFormat Format => _format;

      public bool Equals(FontLabel other)
      {
         return !(other is null)
            && other.Family == this.Family
            && other.Style == this.Style
            && other.Size == this.Size
            && other.Color == this.Color
            && this.Format == other.Format;
      }

      public override bool Equals(object obj)
      {
         return Equals(obj as FontLabel);
      }

      public override int GetHashCode()
      {
         return (this.Family, this.Style, this.Size, this.Color, this.Format).GetHashCode();
      }

      public void Dispose()
      {
         Destroy.Disposable(ref _format);
      }

      public static bool operator ==(FontLabel label1, FontLabel label2)
      {
         return !(label1 is null) && label1.Equals(label2);
      }

      public static bool operator !=(FontLabel label1, FontLabel label2)
      {
         return !(label1 == label2);
      }
   }
}
