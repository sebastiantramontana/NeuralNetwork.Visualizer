using NeuralNetwork.Model;
using NeuralNetwork.Visualizer.Drawing.Canvas;

namespace NeuralNetwork.Visualizer.Drawing
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
