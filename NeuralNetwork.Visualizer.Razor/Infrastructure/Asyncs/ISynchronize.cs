using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs
{
   internal interface ISynchronize
   {
      Task ForEachAsync<T>(IEnumerable<T> objects, Func<T, Task> action);
      Task ForEachAsync(IEnumerable<Func<Task>> funcs);
   }
}
