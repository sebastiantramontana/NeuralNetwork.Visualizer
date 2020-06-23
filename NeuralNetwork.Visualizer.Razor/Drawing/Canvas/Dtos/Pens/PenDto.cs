using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens
{
   internal class PenDto
   {
      public PenDto(int width, BrushBaseDto brush, byte[] lineDash, LineCap cap)
      {
         this.Width = width;
         this.Brush = brush;
         this.LineDash = lineDash;
         this.Cap = cap;
      }

      public int Width { get; }
      public BrushBaseDto Brush { get; }
      public byte[] LineDash { get; }
      public LineCap Cap { get; }
   }
}
