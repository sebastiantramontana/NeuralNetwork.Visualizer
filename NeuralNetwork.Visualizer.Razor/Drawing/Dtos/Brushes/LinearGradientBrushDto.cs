namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos.Brushes
{
   internal class LinearGradientBrushDto : BrushBaseDto
   {
      public LinearGradientBrushDto(int x1, int y1, int x2, int y2, ColorDto color1, ColorDto color2) : base(BrushType.LinearGradient)
      {
         this.X1 = x1;
         this.Y1 = y1;
         this.X2 = x2;
         this.Y2 = y2;
         this.Color1 = color1;
         this.Color2 = color2;
      }

      public int X1 { get; }
      public int Y1 { get; }
      public int X2 { get; }
      public int Y2 { get; }
      public ColorDto Color1 { get; }
      public ColorDto Color2 { get; }
   }
}
