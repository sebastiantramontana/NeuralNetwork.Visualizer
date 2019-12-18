using NeuralNetwork.Model;
using NeuralNetwork.Visualizer.Contracts.Drawing;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
   internal class RegistrationInfo
   {
      public RegistrationInfo(Element element, ICanvas canvas, IRegion region, int zIndex)
      {
         this.Element = element;
         this.Canvas = canvas;
         this.Region = region;
         this.ZIndex = zIndex;
      }

      public Element Element { get; }
      public ICanvas Canvas { get; }
      public IRegion Region { get; }
      public int ZIndex { get; }
   }
}
