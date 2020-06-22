namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos
{
   internal class PositionDto
   {
      public PositionDto(int x, int y)
      {
         this.X = x;
         this.Y = y;
      }

      public int X { get; }
      public int Y { get; }
   }
}
