using System;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Html.Drawing.Canvas;
using NeuralNetwork.Visualizer.Html.Infrastructure;

namespace NeuralNetwork.Visualizer.Html.Drawing
{
   internal class CanvasBuilder : ICanvasBuilder
   {
      private readonly IJsInterop _jsInterop;
      private readonly string _globalInstanceName;

      internal CanvasBuilder(IJsInterop jsInterop, string globalInstanceName)
      {
         _jsInterop = jsInterop;
         _globalInstanceName = globalInstanceName;
      }
      public ICanvas Build(Size size)
      {
         return new HtmlCanvas(size, _jsInterop, _globalInstanceName);
      }
   }
}
