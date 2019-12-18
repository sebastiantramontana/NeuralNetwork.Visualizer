using NeuralNetwork.Visualizer.Contracts.Preferences;

namespace NeuralNetwork.Visualizer.Preferences.Formatting
{
   public class NullFormatter<T> : IFormatter<T>
   {
      public NullFormatter(T value)
      {
         this.Value = value;
      }

      public T Value { get; }

      public T GetFormat(double? value)
      {
         return this.Value;
      }
   }
}
