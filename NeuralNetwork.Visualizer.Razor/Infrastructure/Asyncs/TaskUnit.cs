using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Asyncs
{
   internal class TaskUnit : ITaskUnit
   {
      private TaskCompletionSource<bool> _executeTaskCompletion = null;
      public async Task StartAsync(Func<Task> func)
      {
         if (_executeTaskCompletion != null)
            throw new InvalidOperationException("TaskUnit.Start cannot be nested. Start can be recalled after Finish call");

         _executeTaskCompletion = new TaskCompletionSource<bool>();

         await func.Invoke().ConfigureAwait(false);

         await _executeTaskCompletion.Task.ConfigureAwait(false);
      }
      public void Finish()
      {
         _executeTaskCompletion.SetResult(true);
         _executeTaskCompletion = null;
      }
   }
}
