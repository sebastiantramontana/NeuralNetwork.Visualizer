using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;
using System;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos
{
   internal class FontDto
   {
      public FontDto(FontLabel fontLabel)
      {
         this.Css = ConvertToCss(fontLabel);
         this.Brush = GetBrush(fontLabel);
         this.HorizontalAlignment = fontLabel.TextFormat.HorizontalAlignment;
         this.VerticalAlignment = fontLabel.TextFormat.VerticalAligment;
      }

      public string Css { get; }
      public BrushBaseDto Brush { get; }
      public HorizontalAlignment HorizontalAlignment { get; }
      public VerticalAlignment VerticalAlignment { get; }

      private string ConvertToCss(FontLabel fontLabel)
      {
         var cssFontStyle = GetCssFontStyle(fontLabel.Style);
         var cssFontWeight = GetCssFontWeight(fontLabel.Style);
         var cssFontSize = fontLabel.Size + "px";
         var cssFontFamily = fontLabel.Family;

         return $"{cssFontStyle} {cssFontWeight} {cssFontSize} {cssFontFamily}";
      }

      private string GetCssFontWeight(FontStyle fontStyle)
      {
         return (fontStyle == FontStyle.Bold || fontStyle == FontStyle.BoldItalic)
            ? "bold"
            : string.Empty;
      }

      private string GetCssFontStyle(FontStyle fontStyle)
      {
         return (fontStyle == FontStyle.Italic || fontStyle == FontStyle.BoldItalic)
            ? "italic"
            : string.Empty;
      }

      private BrushBaseDto GetBrush(FontLabel fontLabel)
      {
         BrushBaseDto brushDto = fontLabel.Brush switch
         {
            SolidBrush solidBrush => new SolidBrushDto(new ColorDto(solidBrush.Color)),
            GradientBrush gradientBrush => new LinearGradientBrushDto(0, 0, fontLabel.Size, fontLabel.Size, new ColorDto(gradientBrush.Color1), new ColorDto(gradientBrush.Color2)), //TODO: REVIEW THIS!!!!
            _ => throw new NotImplementedException($"Brush of type {fontLabel.Brush.GetType().Name} not implemented"),
         };

         return brushDto;
      }
   }
}
