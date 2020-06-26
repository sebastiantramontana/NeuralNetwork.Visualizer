using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens;
using System;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas
{
   internal static class HtmlConversionExtensions
   {
      internal static ColorDto ToDto(this Color color)
      {
         string rgba = $"rgba({string.Join(',', color.R, color.G, color.B, color.A / 255f)})";
         return new ColorDto(rgba);
      }

      internal static BrushBaseDto ToDto(this IBrush brush, Rectangle rectangle)
      {
         BrushBaseDto brushDto = brush switch
         {
            SolidBrush solidBrush => new SolidBrushDto(solidBrush.Color.ToDto()),
            GradientBrush gradientBrush => new LinearGradientBrushDto(rectangle.Position.X, rectangle.Position.Y, rectangle.Position.X + rectangle.Size.Width, rectangle.Position.Y + rectangle.Size.Height, gradientBrush.Color1.ToDto(), gradientBrush.Color2.ToDto()),
            _ => throw new NotImplementedException($"Brush of type {brush.GetType().Name} not implemented"),
         };

         return brushDto;
      }

      internal static PenDto ToDto(this Pen pen, Rectangle rectangle)
      {
         return new PenDto(pen.Width, pen.Brush.ToDto(rectangle), pen.LineStyle.ToDto((byte)pen.Width), pen.StartCap == Cap.None ? pen.EndCap.ToDto() : pen.StartCap.ToDto());
      }

      internal static LineCap ToDto(this Cap cap)
      {
         LineCap lineCap = cap switch
         {
            Cap.None => LineCap.Butt,
            Cap.Circle => LineCap.Round,
            Cap.Square => LineCap.Square,
            Cap.Triangle => LineCap.Square,
            _ => throw new NotImplementedException("Unknown Cap type:" + cap)
         };

         return lineCap;
      }

      internal static byte[] ToDto(this LineStyle lineStyle, byte penWidth)
      {
         byte[] segment = lineStyle switch
         {
            LineStyle.Solid => new byte[] { },
            LineStyle.Dash => new[] { (byte)3, (byte)1 },
            LineStyle.Dot => new[] { penWidth, penWidth },
            LineStyle.DahsDot => new[] { (byte)3, (byte)1, penWidth, penWidth },
            _ => throw new NotImplementedException("Unknown Line style: " + lineStyle),
         };

         return segment;
      }

      internal static TextBaseline ToDto(this VerticalAlignment verticalAlignment)
      {
         TextBaseline textBaseline = verticalAlignment switch
         {
            VerticalAlignment.Middle => TextBaseline.Middle,
            VerticalAlignment.Top => TextBaseline.Top,
            VerticalAlignment.Bottom => TextBaseline.Bottom,
            _ => throw new NotImplementedException("Unknown Vertical Alignment: " + verticalAlignment)
         };

         return textBaseline;
      }

      internal static TextAligment ToDto(this HorizontalAlignment horizontalAlignment)
      {
         TextAligment textAlign = horizontalAlignment switch
         {
            HorizontalAlignment.Left => TextAligment.Start,
            HorizontalAlignment.Center => TextAligment.Center,
            HorizontalAlignment.Right => TextAligment.End,
            _ => throw new NotImplementedException("Unknown Vertical Alignment: " + horizontalAlignment)
         };

         return textAlign;
      }

      internal static FontDto ToDto(this FontLabel fontLabel, Rectangle rectangle)
      {
         var css = ConvertToCss(fontLabel);
         var brush = fontLabel.Brush?.ToDto(rectangle);
         var textAlignment = fontLabel.TextFormat.HorizontalAlignment.ToDto();
         var verticalAlignment = fontLabel.TextFormat.VerticalAligment.ToDto();

         return new FontDto(css, brush, textAlignment, verticalAlignment);
      }

      internal static PositionDto ToDto(this Position position)
      {
         return new PositionDto(position.X, position.Y);
      }

      internal static RectangleDto ToDto(this Rectangle rectangle)
      {
         return new RectangleDto(rectangle.Position.ToDto(), rectangle.Size.ToDto());
      }

      internal static SizeDto ToDto(this Size size)
      {
         return new SizeDto { Width = size.Width, Height = size.Height };
      }

      internal static Size ToVisualizer(this SizeDto size)
      {
         return new Size(size.Width, size.Height);
      }

      internal static Image ToVisualizer(this ImageDto image)
      {
         return new Image(image.Size.ToVisualizer(), image.Base64Bytes);
      }

      private static string ConvertToCss(FontLabel fontLabel)
      {
         var cssFontStyle = GetCssFontStyle(fontLabel.Style);
         var cssFontWeight = GetCssFontWeight(fontLabel.Style);
         var cssFontSize = fontLabel.Size + "px";
         var cssFontFamily = fontLabel.Family;

         return $"{cssFontStyle} {cssFontWeight} {cssFontSize} {cssFontFamily}";
      }

      private static string GetCssFontWeight(FontStyle fontStyle)
      {
         return (fontStyle == FontStyle.Bold || fontStyle == FontStyle.BoldItalic)
            ? "bold"
            : string.Empty;
      }

      private static string GetCssFontStyle(FontStyle fontStyle)
      {
         return (fontStyle == FontStyle.Italic || fontStyle == FontStyle.BoldItalic)
            ? "italic"
            : string.Empty;
      }
   }
}
