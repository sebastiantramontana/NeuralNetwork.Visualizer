using System;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
    internal interface IControlCanvas
    {
        Size Size { get; set; }
        Image Image { get; set; }
        NeuralNetworkVisualizerControl Control { get; }
        bool IsReady { get; }

        void SetBlank();
        (Graphics Graph, Image Image) GetGraphics();
        Size GetLayersDrawingSize();

        void SafeInvoke(Action action);
        T SafeInvoke<T>(Func<T> action);
    }
}
