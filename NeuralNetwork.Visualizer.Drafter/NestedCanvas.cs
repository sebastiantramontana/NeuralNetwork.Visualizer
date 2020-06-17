using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing
{
   internal class NestedCanvas : ICanvas
   {
      private readonly Rectangle _rect;
      private readonly ICanvas _decorated;

      public NestedCanvas(Rectangle rect, ICanvas decorated)
      {
         _rect = rect;
         _decorated = decorated;
      }

      public Size Size => _rect.Size;

      public Task DrawEllipse(Rectangle rect, Pen pen, IBrush brush)
      {
         var position = rect.Position + _rect.Position;
         var size = Size.Min(this.Size, rect.Size);
         var rectangle = new Rectangle(position, size);

         return _decorated.DrawEllipse(rectangle, pen, brush);
      }

      public Task DrawLine(Position p1, Position p2, Pen pen)
      {
         var newP1 = p1 + _rect.Position;
         var newP2 = p2 + _rect.Position;

         return _decorated.DrawLine(newP1, newP2, pen);
      }

      public Task DrawRectangle(Rectangle rect, Pen pen, IBrush brush)
      {
         var position = rect.Position + _rect.Position;
         var size = Size.Min(this.Size, rect.Size);
         var rectangle = new Rectangle(position, size);

         return _decorated.DrawRectangle(rectangle, pen, brush);
      }

      public Task DrawText(string text, FontLabel font, Rectangle rect)
      {
         var rectangle = rect + _rect.Position;
         return _decorated.DrawText(text, font, rectangle);
      }

      public Task DrawText(string text, FontLabel font, Rectangle rect, float angle)
      {
         var rectangle = rect + _rect.Position;
         return _decorated.DrawText(text, font, rectangle, angle);
      }

      public Position Translate(Position position, ICanvas destination)
      {
         if (destination == this)
            return position;

         var newPosition = position + _rect.Position;
         return _decorated.Translate(newPosition, destination);
      }
   }
}
