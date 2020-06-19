using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos.Brushes
{
   internal class ColorDto
   {
      private readonly Color _color;

      public ColorDto(Color color)
      {
         _color = color;
      }

      public override string ToString()
      {
         return $"#"
      }
   }
}
