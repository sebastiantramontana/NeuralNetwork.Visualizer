namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos
{
   internal class ColorDto
   {
      public ColorDto(string cssColor)
      {
         this.Css = cssColor;
      }

      public string Css { get; }
   }
}
