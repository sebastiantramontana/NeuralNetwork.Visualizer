using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using System;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Canvas
{
   internal class GraphicsCanvas : ICanvas
   {
      private const int MINIMUM_FONT_SIZE = 8;

      private Gdi.Graphics _graph;
      internal GraphicsCanvas(Gdi.Graphics graph, Size size)
      {
         _graph = graph;
         this.Size = size;
      }

      public Size Size { get; }

      internal void SetGraphics(Gdi.Graphics graph)
      {
         _graph = graph;
      }

      public void DrawRectangle(Rectangle rect, Pen pen, IBrush brush)
      {
         DrawShape(rect, brush, pen, _graph.FillRectangle, _graph.DrawRectangle);
      }

      public void DrawEllipse(Rectangle rect, Pen pen, IBrush brush)
      {
         DrawShape(rect, brush, pen, _graph.FillEllipse, _graph.DrawEllipse);
      }

      public void DrawText(string text, FontLabel fontLabel, Rectangle rect)
      {
         if (!Validate(fontLabel.Brush))
            return;

         using var gdiBrush = fontLabel.Brush.ToGdi(rect);
         using var gdiFormat = fontLabel.TextFormat.ToGdi();

         DrawAdjustedFontString(text, fontLabel, rect.Size, (font) => _graph.DrawString(text, font, gdiBrush, rect.ToGdi(), gdiFormat));
      }

      public void DrawText(string text, FontLabel fontLabel, Rectangle rect, float angle)
      {
         if (!Validate(fontLabel.Brush))
            return;

         DrawAdjustedFontString(text, fontLabel, rect.Size, (font) =>
         {
            var transform = _graph.Transform;

            _graph.TranslateTransform(rect.Position.X, rect.Position.Y);
            _graph.RotateTransform(angle);

            using var gdiBrush = fontLabel.Brush.ToGdi(rect);
            using var gdiFormat = fontLabel.TextFormat.ToGdi();

            _graph.DrawString(text, font, gdiBrush, new Gdi.Rectangle(0, 0, rect.Size.Width, rect.Size.Height), gdiFormat);
            _graph.Transform = transform;
         });
      }

      public void DrawLine(Position pos1, Position pos2, Pen pen)
      {
         if (!Validate(pen))
            return;

         using var gdiPen = pen.ToGdi(pos1, pos2);
         _graph.DrawLine(gdiPen, pos1.ToGdi(), pos2.ToGdi());
      }

      public Position Translate(Position position, ICanvas destination)
      {
         if (object.ReferenceEquals(destination, this) || destination is GraphicsCanvas)
            return position;

         var posTranslated = destination.Translate(new Position(0, 0), this);
         position = new Position(position.X - posTranslated.X, position.Y - posTranslated.Y);
         return position;
      }

      private void DrawShape(Rectangle rect, IBrush brush, Pen pen, Action<Gdi.Brush, Gdi.Rectangle> fillShapeAction, Action<Gdi.Pen, Gdi.Rectangle> outlineShapeAction)
      {
         var minSize = Size.Min(this.Size, rect.Size);
         var rectangle = new Rectangle(rect.Position, minSize);

         if (Validate(brush))
         {
            using var gdiBrush = brush.ToGdi(rectangle);
            fillShapeAction(gdiBrush, rectangle.ToGdi());
         }

         if (Validate(pen))
         {
            using var gdiPen = pen.ToGdi(rectangle);
            outlineShapeAction(gdiPen, rectangle.ToGdi());
         }
      }

      private void DrawAdjustedFontString(string text, FontLabel fontLabel, Size containerSize, Action<Gdi.Font> drawAction)
      {
         var font = GetAdjustedFont(text, fontLabel, containerSize);

         if (font != null)
         {
            drawAction(font);
            font.Dispose();
         }
      }

      private Gdi.Font GetAdjustedFont(string text, FontLabel fontLabel, Size containerSize)
      {
         for (int adjustedWidth = containerSize.Width; adjustedWidth >= MINIMUM_FONT_SIZE; adjustedWidth--)
         {
            var testFont = fontLabel.ToGdi(adjustedWidth);

            var adjustedSizeNew = _graph.MeasureString(text, testFont);

            if (containerSize.Width > (int)adjustedSizeNew.Width
                && (containerSize.Height) > (int)adjustedSizeNew.Height)
            {
               return testFont;
            }

            testFont.Dispose();
         }

         return null;
      }

      private bool Validate(IBrush brush)
      {
         return !(brush is SolidBrush solidBrush && solidBrush.Color.IsTransparent);
      }

      private bool Validate(Pen pen)
      {
         return Validate(pen?.Brush);
      }
   }
}
