using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos
{
   internal class SizeDto
   {
      public int Width { get; set; }
      public int Height { get; set; }

      internal Size ToSize()
      {
         return new Size(this.Width, this.Height);
      }
   }
}
