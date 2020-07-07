using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas;
using NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal class CanvasBuilder : ICanvasBuilder
   {
      private readonly IJsDrawingCallAccumulator _jsDrawingCallAccumulator;

      internal CanvasBuilder(IJsDrawingCallAccumulator jsDrawingCallAccumulator)
      {
         _jsDrawingCallAccumulator = jsDrawingCallAccumulator;
      }

      public ICanvas Build(Size size)
      {
         return new HtmlCanvas(size, _jsDrawingCallAccumulator);
      }
   }
}
