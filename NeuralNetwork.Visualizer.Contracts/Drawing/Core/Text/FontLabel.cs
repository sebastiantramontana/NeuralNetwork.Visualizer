﻿using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text
{
   public class FontLabel : IEquatable<FontLabel>
   {
      public static FontLabel Null = new FontLabel(string.Empty, 0, SolidBrush.Null, new TextFormat(HorizontalAlignment.Left, VerticalAlignment.Top, TextTrimming.None));
      public static FontLabel Default = new FontLabel("Tahoma", FontStyle.Regular, SolidBrush.Black, new TextFormat(HorizontalAlignment.Center, VerticalAlignment.Middle, TextTrimming.None));

      public FontLabel(FontLabel copyFrom, FontStyle style) : this(copyFrom.Family, style, copyFrom.Brush, copyFrom.TextFormat)
      {
      }

      public FontLabel(FontLabel copyFrom, IBrush brush) : this(copyFrom.Family, copyFrom.Style, brush, copyFrom.TextFormat)
      {
      }

      public FontLabel(FontLabel copyFrom, TextFormat textFormat) : this(copyFrom.Family, copyFrom.Style, copyFrom.Brush, textFormat)
      {
      }

      public FontLabel(string family, FontStyle style, IBrush brush, TextFormat textFormat)
      {
         this.Family = family;
         this.Style = style;
         this.Brush = brush;
         this.TextFormat = textFormat;
      }

      public string Family { get; }
      public FontStyle Style { get; }
      public IBrush Brush { get; }
      public TextFormat TextFormat { get; }

      public bool Equals(FontLabel other)
      {
         return !(other is null)
            && other.Family == this.Family
            && other.Style == this.Style
            && other.Brush == this.Brush
            && other.TextFormat == this.TextFormat;
      }

      public override bool Equals(object obj)
      {
         return Equals(obj as FontLabel);
      }

      public override int GetHashCode()
      {
         return (this.Family, this.Style, this.Brush, this.TextFormat).GetHashCode();
      }

      public static bool operator ==(FontLabel label1, FontLabel label2)
      {
         return (label1?.Equals(label2)) ?? false;
      }

      public static bool operator !=(FontLabel label1, FontLabel label2)
      {
         return !(label1 == label2);
      }
   }
}
