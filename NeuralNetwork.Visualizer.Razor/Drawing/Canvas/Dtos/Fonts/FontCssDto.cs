namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts
{
   internal class FontCssDto
   {
      public FontCssDto(string cssFontFamily, string cssFontStyle, string cssFontWeight)
      {
         this.CssFontFamily = cssFontFamily;
         this.CssFontStyle = cssFontStyle;
         this.CssFontWeight = cssFontWeight;
      }

      public string CssFontFamily { get; }
      public string CssFontStyle { get; }
      public string CssFontWeight { get; }
   }
}
