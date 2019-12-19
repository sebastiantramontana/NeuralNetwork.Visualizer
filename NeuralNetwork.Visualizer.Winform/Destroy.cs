using System;

namespace NeuralNetwork.Visualizer.Winform
{
    internal static class Destroy
    {
        internal static void Disposable<TDisposable>(ref TDisposable disposable) where TDisposable : class, IDisposable
        {
            if (disposable != null)
            {
                disposable.Dispose();
                disposable = null;
            }
        }
    }
}
