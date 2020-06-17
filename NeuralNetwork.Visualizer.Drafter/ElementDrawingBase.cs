using NeuralNetwork.Model;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing
{
   public abstract class ElementDrawingBase<TElement> : IElementDrawing where TElement : Element
   {
      public ElementDrawingBase(TElement element)
      {
         this.Element = element;
      }

      public TElement Element { get; private set; }
      public abstract Task Draw(ICanvas canvas);
   }
}
