using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Model;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Selection
{
    internal class RegistrationInfo
    {
        public RegistrationInfo(Element element, ICanvas canvas, Region region, int zIndex)
        {
            this.Element = element;
            this.Canvas = canvas;
            this.Region = region;
            this.ZIndex = zIndex;
        }

        public Element Element { get; }
        public ICanvas Canvas { get; }
        public Region Region { get; }
        public int ZIndex { get; }
    }
}
