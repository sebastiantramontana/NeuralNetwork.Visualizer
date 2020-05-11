using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs
{
   internal interface ISynchronize
   {
      Task ForEachhAsync<T>(IEnumerable<T> objects, Func<T, Task> action);
   }
}
