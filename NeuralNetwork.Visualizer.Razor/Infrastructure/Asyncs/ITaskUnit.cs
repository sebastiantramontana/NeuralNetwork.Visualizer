using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs
{
   internal interface ITaskUnit
   {
      Task StartAsync(Func<Task> func);
      void Finish();
   }
}
