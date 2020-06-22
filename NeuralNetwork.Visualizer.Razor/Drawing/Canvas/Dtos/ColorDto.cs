namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos
{
   internal class ColorDto
   {
      private readonly string _cssColor;

      public ColorDto(string cssColor)
      {
         _cssColor = cssColor;
      }

      public override string ToString()
      {
         return _cssColor;
      }
   }
}
