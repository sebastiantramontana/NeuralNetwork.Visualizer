using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts
{
   internal class FontDto
   {
      public FontDto(FontCssDto css, BrushBaseDto brush, TextAligment textAligment, TextBaseline textBaseline)
      {
         this.Css = css;
         this.Brush = brush;
         this.TextAligment = textAligment;
         this.TextBaseline = textBaseline;
      }

      public FontCssDto Css { get; }
      public BrushBaseDto Brush { get; }
      public TextAligment TextAligment { get; }
      public TextBaseline TextBaseline { get; }
   }
}
