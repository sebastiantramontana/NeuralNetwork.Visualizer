using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos.Brushes
{
   internal class ColorDto
   {
      private readonly string _rgba;

      public ColorDto(Color color)
      {
         _rgba = ConvertToRGBA(color);
      }

      public override string ToString()
      {
         return _rgba;
      }

      private string ConvertToRGBA(Color color)
      {
         return $"rgba({string.Join(',', color.R, color.G, color.B, color.A)})";
      }
   }
}
