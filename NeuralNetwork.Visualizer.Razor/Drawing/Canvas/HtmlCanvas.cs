using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas
{
   internal class HtmlCanvas : ICanvas
   {
      private readonly IJsInterop _jsInterop;
      private readonly string _globalInstanceName;

      internal HtmlCanvas(Size size, IJsInterop jsInterop, string globalInstanceName)
      {
         this.Size = size;
         _jsInterop = jsInterop;
         _globalInstanceName = globalInstanceName;
      }
      public Size Size { get; }

      public async Task DrawEllipse(Rectangle rect, Pen pen, IBrush brush)
      {
         //x, y, radiusX, radiusY, pen, brush

         //await _jsInterop.ExecuteOnInstance($"Canvas.drawEllipse",rect.Position.X,).ConfigureAwait(false);
      }

      public async Task DrawLine(Position p1, Position p2, Pen pen)
      {
         throw new System.NotImplementedException();
      }

      public async Task DrawRectangle(Rectangle rect, Pen pen, IBrush brush)
      {
         throw new System.NotImplementedException();
      }

      public async Task DrawText(string text, FontLabel font, Rectangle rect)
      {
         throw new System.NotImplementedException();
      }

      public async Task DrawText(string text, FontLabel font, Rectangle rect, float angle)
      {
         throw new System.NotImplementedException();
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
