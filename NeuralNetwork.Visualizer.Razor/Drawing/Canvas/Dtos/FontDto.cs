using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos
{
   internal class FontDto
   {
      public FontDto(string css, BrushBaseDto brush, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
      {
         this.Css = css;
         this.Brush = brush;
         this.HorizontalAlignment = horizontalAlignment;
         this.VerticalAlignment = verticalAlignment;
      }

      public string Css { get; }
      public BrushBaseDto Brush { get; }
      public HorizontalAlignment HorizontalAlignment { get; }
      public VerticalAlignment VerticalAlignment { get; }
   }
}
