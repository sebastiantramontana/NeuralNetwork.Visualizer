using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using System;
using System.IO;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping
{
   public static class GdiExtensions
   {
      public static Gdi.Brush ToGdi(this IBrush brush, Rectangle rectangle)
      {
         Gdi.Brush gdiBrush = brush switch
         {
            SolidBrush solidBrush => new Gdi.SolidBrush(solidBrush.Color.ToGdi()),
            GradientBrush gradienBrush => new Gdi.Drawing2D.LinearGradientBrush(rectangle.ToGdi(), gradienBrush.Color1.ToGdi(), gradienBrush.Color2.ToGdi(), gradienBrush.Angle),
            _ => throw new NotImplementedException($"Brush of type {brush.GetType().Name} not implemented"),
         };

         return gdiBrush;
      }

      public static Gdi.Brush ToGdi(this IBrush brush, Position pos1, Position pos2)
      {
         Gdi.Brush gdiBrush = brush switch
         {
            SolidBrush solidBrush => new Gdi.SolidBrush(solidBrush.Color.ToGdi()),
            GradientBrush gradienBrush => new Gdi.Drawing2D.LinearGradientBrush(pos1.ToGdi(), pos2.ToGdi(), gradienBrush.Color1.ToGdi(), gradienBrush.Color2.ToGdi()),
            _ => throw new NotImplementedException($"Brush of type {brush.GetType().Name} not implemented"),
         };

         return gdiBrush;
      }

      public static Gdi.Pen ToGdi(this Pen pen, Rectangle rectangle)
      {
         return new Gdi.Pen(pen.Brush.ToGdi(rectangle), pen.Width);
      }

      public static Gdi.Pen ToGdi(this Pen pen, Position pos1, Position pos2)
      {
         return new Gdi.Pen(pen.Brush.ToGdi(pos1, pos2), pen.Width);
      }

      public static Gdi.Color ToGdi(this Color color)
      {
         return Gdi.Color.FromArgb(color.A, color.R, color.G, color.B);
      }

      public static Gdi.Rectangle ToGdi(this Rectangle rectangle)
      {
         return new Gdi.Rectangle(rectangle.Position.X, rectangle.Position.Y, rectangle.Size.Width, rectangle.Size.Height);
      }

      public static Gdi.StringFormat ToGdi(this TextFormat format)
      {
         return new Gdi.StringFormat
         {
            Alignment = format.HorizontalAlignment.ToGdi(),
            LineAlignment = format.VerticalAligment.ToGdi(),
            Trimming = format.TextTrimming.ToGdi()
         };
      }

      public static Gdi.PointF ToGdi(this Position position)
      {
         return new Gdi.PointF(position.X, position.Y);
      }

      public static Position ToVisualizer(this Gdi.Point position)
      {
         return new Position(position.X, position.Y);
      }

      public static Gdi.Font ToGdi(this FontLabel fontLabel)
      {
         return new Gdi.Font(fontLabel.Family, fontLabel.Size, fontLabel.Style.ToGdi(), Gdi.GraphicsUnit.Pixel);
      }

      public static Size ToVisualizer(this Gdi.SizeF size)
      {
         return new Size((int)size.Width, (int)size.Height);
      }

      public static Size ToVisualizer(this Gdi.Size size)
      {
         return new Size(size.Width, size.Height);
      }

      public static Gdi.Size ToGdi(this Size size)
      {
         return new Gdi.Size(size.Width, size.Height);
      }

      public static Gdi.StringAlignment ToGdi(this HorizontalAlignment alignment)
      {
         switch (alignment)
         {
            case HorizontalAlignment.Left:
               return Gdi.StringAlignment.Near;
            case HorizontalAlignment.Center:
               return Gdi.StringAlignment.Center;
            case HorizontalAlignment.Right:
               return Gdi.StringAlignment.Far;
            default:
               break;
         }

         throw new NotImplementedException($"Text aligment {alignment} is not implemented");
      }

      public static Gdi.StringAlignment ToGdi(this VerticalAlignment alignment)
      {
         switch (alignment)
         {
            case VerticalAlignment.Top:
               return Gdi.StringAlignment.Near;
            case VerticalAlignment.Middle:
               return Gdi.StringAlignment.Center;
            case VerticalAlignment.Bottom:
               return Gdi.StringAlignment.Far;
            default:
               break;
         }

         throw new NotImplementedException($"Text aligment {alignment} is not implemented");
      }

      public static Gdi.StringTrimming ToGdi(this TextTrimming trimming)
      {
         switch (trimming)
         {
            case TextTrimming.None:
               return Gdi.StringTrimming.None;
            case TextTrimming.Character:
               return Gdi.StringTrimming.EllipsisCharacter;
            case TextTrimming.Word:
               return Gdi.StringTrimming.EllipsisWord;
            default:
               break;
         }

         throw new NotImplementedException($"Text trimming {trimming} is not implemented");
      }

      public static Gdi.FontStyle ToGdi(this FontStyle style)
      {
         switch (style)
         {
            case FontStyle.Regular:
               return Gdi.FontStyle.Regular;
            case FontStyle.Bold:
               return Gdi.FontStyle.Bold;
            case FontStyle.Italic:
               return Gdi.FontStyle.Italic;
            case FontStyle.BoldItalic:
               return Gdi.FontStyle.Bold | Gdi.FontStyle.Italic;
            default:
               break;
         }

         throw new NotImplementedException($"Font style {style} is not implemented");
      }

      public static Gdi.Image ToGdi(this Image image)
      {
         var bytes = Convert.FromBase64String(image.Base64Bytes);
         using var stream = new MemoryStream(bytes);

         return new Gdi.Bitmap(stream);
      }

      public static Image ToVisualizer(this Gdi.Image image)
      {
         using var stream = new MemoryStream();
         image.Save(stream, Gdi.Imaging.ImageFormat.Png);

         var base64Bytes = Convert.ToBase64String(stream.ToArray());

         return new Image(new Size(image.Width, image.Height), base64Bytes);
      }
   }
}
