using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs
{
   internal class TaskUnit : ITaskUnit
   {
      public static TaskUnit Create()
      {
         return new TaskUnit();
      }

      private TaskUnit()
      {

      }

      private TaskCompletionSource<bool> _executeTaskCompletion = null;

      public Task StartAsync(Func<Task> func)
      {
         if (_executeTaskCompletion != null)
            throw new InvalidOperationException("TaskUnit.StartAsync cannot be nested. StartAsync only can be recalled after Finish call");

         _executeTaskCompletion = new TaskCompletionSource<bool>();

         var taskFunc = func.Invoke();
         var taskCompletion = _executeTaskCompletion.Task;

         return Task.WhenAll(taskFunc, taskCompletion);
      }

      public void Finish()
      {
         _executeTaskCompletion.SetResult(true);
         _executeTaskCompletion = null;
      }
   }
}
