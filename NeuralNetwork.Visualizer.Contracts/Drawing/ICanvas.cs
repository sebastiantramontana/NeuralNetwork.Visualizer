using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   public interface ICanvas
   {
      Size Size { get; }

      Position Translate(Position position, ICanvas destination);
      Task DrawEllipse(Rectangle rect, Pen pen, IBrush brush);
      Task DrawLine(Position p1, Position p2, Pen pen);
      Task DrawRectangle(Rectangle rect, Pen pen, IBrush brush);
      Task DrawText(string text, FontLabel font, Rectangle rect);
      Task DrawText(string text, FontLabel font, Rectangle rect, float angle);
   }
}
