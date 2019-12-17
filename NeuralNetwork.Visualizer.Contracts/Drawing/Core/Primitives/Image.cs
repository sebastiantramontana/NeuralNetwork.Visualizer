using Base64 = System.String;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives
{

   public class Image
   {
      public Size Size { get; }
      public Base64 Bytes { get; }
   }
}
