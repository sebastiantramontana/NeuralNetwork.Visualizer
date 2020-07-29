using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using System;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.NetCore.Winform.Drawing
{
   internal interface IGdiImageCanvasBuilder : IDisposable
   {
      Gdi.Image CurrentImage { get; }
      ICanvas Build(Size size, RenderQuality quality);
   }
}
