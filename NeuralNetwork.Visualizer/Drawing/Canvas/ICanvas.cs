using NeuralNetwork.Visualizer.Preferences.Brushes;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Drawing.Canvas
{
    internal interface ICanvas
    {
        int MaxWidth { get; }
        int MaxHeight { get; }

        Point Translate(Point point, ICanvas destination);
        void DrawEllipse(Rectangle rect, Pen pen, IBrush brush);
        void DrawLine(Point p1, Point p2, Pen pen);
        void DrawRectangle(Rectangle rect, Pen pen, IBrush brush);
        void DrawText(string text, Font font, Point position, IBrush brush, StringFormat format);
        void DrawText(string text, FontInfo font, Rectangle rect, IBrush brush, StringFormat format);
        void DrawText(string text, FontInfo font, Rectangle rect, IBrush brush, StringFormat format, float angle);
        void DrawImage(Image image, Point position, Size size);
        Size MeasureText(string text, Font font, Point position, StringFormat format);
    }
}
