using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens
{
   internal class PenDto
   {
      public PenDto(int width, BrushBaseDto brush, LineStyle lineStyle, Cap cap)
      {
         this.Width = width;
         this.Brush = brush;
         this.LineStyle = lineStyle;
         this.Cap = cap;
      }

      public int Width { get; }
      public BrushBaseDto Brush { get; }
      public LineStyle LineStyle { get; }
      public Cap Cap { get; }
   }
}
