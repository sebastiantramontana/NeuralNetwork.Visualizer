using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal class DrawingRunner : IDrawingRunner
   {
      private readonly IJsInterop _jsInterop;

      public DrawingRunner(IJsInterop jsInterop)
      {
         _jsInterop = jsInterop;
      }

      public Task Run(Func<Task> drawFunc)
      {
         var beginTask = _jsInterop.ExecuteOnInstance("Canvas.beginDraw");
         var drawTask = drawFunc.Invoke();
         var endTask = _jsInterop.ExecuteOnInstance("Canvas.endDraw");

         return Task.WhenAll(beginTask, drawTask, endTask);
      }
   }
}
