namespace NeuralNetwork.Visualizer.Preferences.Formatting
{
   public class ByValueSignFormatter<T> : IFormatter<T>
   {
      public ByValueSignFormatter(T whenNegative, T whenZero, T whenPositive, T whenNull)
      {
         this.WhenNegative = whenNegative;
         this.WhenZero = whenZero;
         this.WhenPositive = whenPositive;
         this.WhenNull = whenNull;
      }

      public T WhenNegative { get; }
      public T WhenZero { get; }
      public T WhenPositive { get; }
      public T WhenNull { get; }

      public T GetFormat(double? value)
      {
         return !value.HasValue ? this.WhenNull :
            (value.Value < 0.0 ? this.WhenNegative :
               (value.Value == 0.0 ? this.WhenZero : this.WhenPositive));
      }
   }
}
