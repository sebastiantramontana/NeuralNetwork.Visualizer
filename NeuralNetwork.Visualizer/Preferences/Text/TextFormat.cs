using NeuralNetwork.Visualizer.Preferences.Core;
using System;

namespace NeuralNetwork.Visualizer.Preferences.Text
{
   public class TextFormat : IEquatable<TextFormat>
   {
      public TextFormat(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAligment, TextTrimming textTrimming)
      {
         this.HorizontalAlignment = horizontalAlignment;
         this.VerticalAligment = verticalAligment;
         this.TextTrimming = textTrimming;
      }

      public HorizontalAlignment HorizontalAlignment { get; }
      public VerticalAlignment VerticalAligment { get; }
      public TextTrimming TextTrimming { get; }

      public override bool Equals(object obj)
      {
         return base.Equals(obj as TextFormat);
      }

      public bool Equals(TextFormat other)
      {
         return !(other is null)
            && other.HorizontalAlignment == this.HorizontalAlignment
            && other.VerticalAligment == this.VerticalAligment
            && other.TextTrimming == this.TextTrimming;
      }

      public override int GetHashCode()
      {
         var hashCode = (this.HorizontalAlignment, this.VerticalAligment, this.TextTrimming).GetHashCode();
         return hashCode;
      }

      public static bool operator ==(TextFormat left, TextFormat right)
      {
         return (left?.Equals(right)) ?? false;
      }

      public static bool operator !=(TextFormat left, TextFormat right)
      {
         return !(left == right);
      }
   }
}
