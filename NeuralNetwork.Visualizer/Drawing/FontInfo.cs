using System.Drawing;

namespace NeuralNetwork.Visualizer.Drawing
{
   internal class FontInfo
   {
      internal FontInfo(string family, FontStyle style)
      {
         this.Family = family;
         this.Style = style;
      }

      internal string Family { get; }
      internal FontStyle Style { get; }
   }
}
