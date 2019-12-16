using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Core;
using NeuralNetwork.Visualizer.Preferences.Pens;
using NeuralNetwork.Visualizer.Preferences.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing.Canvas
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
