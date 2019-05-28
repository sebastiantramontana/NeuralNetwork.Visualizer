using System;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Pens
{
    public interface IPen : IDisposable
    {
        Pen CreatePen();
    }
}
