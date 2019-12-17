using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   internal interface ICanvas
   {
      Size Size { get; }

      Position Translate(Position point, ICanvas destination);
      void DrawEllipse(Rectangle rect, Pen pen, IBrush brush);
      void DrawLine(Position p1, Position p2, Pen pen);
      void DrawRectangle(Rectangle rect, Pen pen, IBrush brush);
      void DrawText(string text, FontLabel font, Position position);
      void DrawText(string text, FontLabel font, Rectangle rect);
      void DrawText(string text, FontLabel font, Rectangle rect, float angle);
      Size MeasureText(string text, FontLabel font, Position position);
   }
}
