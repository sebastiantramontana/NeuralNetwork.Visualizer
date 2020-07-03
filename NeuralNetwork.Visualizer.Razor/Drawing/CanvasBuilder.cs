using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal class CanvasBuilder : ICanvasBuilder
   {
      private readonly IJsInterop _jsInterop;
      private readonly ITaskUnit _taskUnit;

      internal CanvasBuilder(IJsInterop jsInterop, ITaskUnit taskUnit)
      {
         _jsInterop = jsInterop;
         _taskUnit = taskUnit;
      }
      public ICanvas Build(Size size)
      {
         return new HtmlCanvas(size, _jsInterop, _taskUnit);
      }
   }
}
