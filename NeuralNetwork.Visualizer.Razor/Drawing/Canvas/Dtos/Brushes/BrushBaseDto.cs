namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes
{
   internal abstract class BrushBaseDto
   {
      protected BrushBaseDto(BrushType type)
      {
         this.Type = type;
      }

      public BrushType Type { get; }
   }
}
