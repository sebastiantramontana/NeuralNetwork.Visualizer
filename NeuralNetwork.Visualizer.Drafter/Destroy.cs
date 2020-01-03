using System;

namespace NeuralNetwork.Visualizer.Drawing
{
   public static class Destroy
   {
      public static void Disposable<TDisposable>(ref TDisposable disposable) where TDisposable : class, IDisposable
      {
         disposable?.Dispose();
         disposable = null;
      }
   }
}
