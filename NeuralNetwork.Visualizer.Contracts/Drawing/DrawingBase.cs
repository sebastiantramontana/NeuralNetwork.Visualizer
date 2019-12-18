using NeuralNetwork.Model;

namespace NeuralNetwork.Visualizer.Contracts.Drawing
{
   internal abstract class DrawingBase<TElement> : IDrawing where TElement : Element
   {
      internal DrawingBase(TElement element)
      {
         this.Element = element;
      }

      internal TElement Element { get; private set; }
      public abstract void Draw(ICanvas canvas);
   }
}
