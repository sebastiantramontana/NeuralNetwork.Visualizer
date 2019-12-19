using NeuralNetwork.Model;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   public abstract class DrawingBase<TElement> : IDrawing where TElement : Element
   {
      public DrawingBase(TElement element)
      {
         this.Element = element;
      }

      public TElement Element { get; private set; }
      public abstract void Draw(ICanvas canvas);
   }
}
