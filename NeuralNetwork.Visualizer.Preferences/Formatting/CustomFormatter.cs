using NeuralNetwork.Visualizer.Contracts.Preferences;
using System;

namespace NeuralNetwork.Visualizer.Preferences.Formatting
{
   public class CustomFormatter<T> : IFormatter<T>
   {
      public CustomFormatter(Func<double?, T> formaterFunc)
      {
         this.FormaterFunc = formaterFunc;
      }

      public Func<double?, T> FormaterFunc { get; }

      public T GetFormat(double? value)
      {
         return FormaterFunc(value);
      }
   }
}
