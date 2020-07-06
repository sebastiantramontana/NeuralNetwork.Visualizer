using NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal class DrawingRunner : IDrawingRunner
   {
      private readonly IJsInterop _jsInterop;
      private readonly ISynchronize _synchronize;

      public DrawingRunner(IJsInterop jsInterop, ISynchronize synchronize)
      {
         _jsInterop = jsInterop;
         _synchronize = synchronize;
      }

      public Task Run(Func<Task> drawFunc)
      {
         return _synchronize.ForEachAsync(new[]
         {
           new Func<Task>( ()=>_jsInterop.ExecuteOnInstanceAsync("Canvas.beginDraw")),
           drawFunc,
           new Func<Task>( ()=>_jsInterop.ExecuteOnInstanceAsync("Canvas.endDraw"))
         });
      }
   }
}
