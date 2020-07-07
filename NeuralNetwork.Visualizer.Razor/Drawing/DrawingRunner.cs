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
         _jsInterop.ExecuteOnInstance("Canvas.beginDraw");

         return drawFunc.Invoke().ContinueWith((t) =>
         {
            _jsInterop.ExecuteOnInstance("Canvas.endDraw");
         }, TaskContinuationOptions.OnlyOnRanToCompletion);
      }
   }
}
