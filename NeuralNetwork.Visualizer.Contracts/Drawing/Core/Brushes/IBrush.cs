using System;

namespace NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes
{
   public interface IBrush
   {
      bool Equals(IBrush other);
   }

   public interface IBrush<TBrush> : IBrush, IEquatable<TBrush> where TBrush : class, IBrush
   {

   }
}
