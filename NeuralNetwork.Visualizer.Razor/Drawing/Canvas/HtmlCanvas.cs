using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas
{
   internal class HtmlCanvas : ICanvas
   {
      private readonly IJsInterop _jsInterop;

      internal HtmlCanvas(Size size, IJsInterop jsInterop)
      {
         this.Size = size;
         _jsInterop = jsInterop;
      }
      public Size Size { get; }

      public async Task DrawEllipse(Rectangle rect, Pen pen, IBrush brush)
      {
         var x = rect.Position.X;
         var y = rect.Position.Y;
         var radiusX = rect.Size.Width / 2;
         var radiusY = rect.Size.Height / 2;
         var penDto = pen.ToDto(rect);
         var brushDto = brush.ToDto(rect);

         await _jsInterop.ExecuteOnInstance($"Canvas.Drawing.drawEllipse", x, y, radiusX, radiusY, penDto, brushDto).ConfigureAwait(false);
      }

      public async Task DrawLine(Position p1, Position p2, Pen pen)
      {
         var p1Dto = p1.ToDto();
         var p2Dto = p2.ToDto();
         var penDto = pen.ToDto(new Rectangle(p1, new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y))));

         await _jsInterop.ExecuteOnInstance($"Canvas.Drawing.drawLine", p1Dto, p2Dto, penDto).ConfigureAwait(false);
      }

      public async Task DrawRectangle(Rectangle rect, Pen pen, IBrush brush)
      {
         var rectangleDto = rect.ToDto();
         var penDto = pen.ToDto(rect);
         var brushDto = brush.ToDto(rect);

         await _jsInterop.ExecuteOnInstance($"Canvas.Drawing.drawRectangle", rectangleDto, penDto, brushDto).ConfigureAwait(false);
      }

      public Task DrawText(string text, FontLabel font, Rectangle rect)
      {
         return DrawText(text, font, rect, 0);
      }

      public async Task DrawText(string text, FontLabel font, Rectangle rect, float angle)
      {
         var fontDto = font.ToDto(rect);
         var rectangleDto = rect.ToDto();

         await _jsInterop.ExecuteOnInstance($"Canvas.Drawing.drawText", text, fontDto, rectangleDto, angle).ConfigureAwait(false);
      }

      public Position Translate(Position position, ICanvas destination)
      {
         if (object.ReferenceEquals(destination, this) || destination is HtmlCanvas)
            return position;

         var posTranslated = destination.Translate(new Position(0, 0), this);
         position = new Position(position.X - posTranslated.X, position.Y - posTranslated.Y);
         return position;
      }
   }
}
