using Base64 = System.String;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives
{

   public class Image
   {
      public Image(Size size, Base64 base64Bytes)
      {
         this.Size = size;
         this.Base64Bytes = base64Bytes;
      }

      public Size Size { get; }
      public Base64 Base64Bytes { get; }
   }
}
