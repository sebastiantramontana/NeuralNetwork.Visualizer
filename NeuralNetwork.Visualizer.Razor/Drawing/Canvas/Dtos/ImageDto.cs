using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using Base64 = System.String;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos
{
   internal class ImageDto
   {
      public SizeDto Size { get; }
      public Base64 Base64Bytes { get; }
   }
}
