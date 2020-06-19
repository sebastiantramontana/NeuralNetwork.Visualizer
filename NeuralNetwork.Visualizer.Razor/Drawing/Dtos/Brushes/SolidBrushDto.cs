namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos.Brushes
{
   internal class SolidBrushDto : BrushBaseDto
   {
      internal SolidBrushDto(ColorDto color) : base(BrushType.Solid)
      {
         this.Color = color;
      }

      public ColorDto Color { get; }
   }
}
