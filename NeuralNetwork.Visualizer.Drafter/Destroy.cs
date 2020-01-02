using System;

namespace NeuralNetwork.Visualizer.Drawing
{
   internal static class Destroy
   {
      internal static void Disposable<TDisposable>(ref TDisposable disposable) where TDisposable : class, IDisposable
      {
         disposable?.Dispose();
         disposable = null;
      }
   }
}
