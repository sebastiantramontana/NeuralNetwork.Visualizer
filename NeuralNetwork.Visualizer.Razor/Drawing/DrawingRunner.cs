using NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal class DrawingRunner : IDrawingRunner
   {
      private readonly IJsInterop _jsInterop;
      private readonly IJsDrawingCallAccumulator _jsDrawingCallAccumulator;

      public DrawingRunner(IJsInterop jsInterop, IJsDrawingCallAccumulator jsDrawingCallAccumulator)
      {
         _jsInterop = jsInterop;
         _jsDrawingCallAccumulator = jsDrawingCallAccumulator;
      }

      public Task Run(Func<Task> drawFunc)
      {
         _jsInterop.ExecuteOnInstance("Canvas.beginDraw");

         return drawFunc.Invoke().ContinueWith((t) =>
         {
            var calls = _jsDrawingCallAccumulator.GetCalls();
            _jsInterop.ExecuteOnInstance("Canvas.endDraw", new { Calls = calls });

         }, TaskContinuationOptions.OnlyOnRanToCompletion);
      }
   }
}
