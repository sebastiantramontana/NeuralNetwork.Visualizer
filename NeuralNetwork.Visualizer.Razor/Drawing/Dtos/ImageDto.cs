using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using Base64 = System.String;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos
{
   internal class ImageDto
   {
      public SizeDto Size { get; }
      public Base64 Base64Bytes { get; }

      internal Image ToImage()
      {
         return new Image(this.Size.ToSize(), this.Base64Bytes);
      }
   }
}
