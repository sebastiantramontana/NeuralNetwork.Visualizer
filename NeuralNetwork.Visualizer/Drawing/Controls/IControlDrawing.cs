using System.Drawing;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
    internal interface IControlDrawing
    {
        Task RedrawAsync();
        void Redraw();
        Image GetImage();
    }
}
