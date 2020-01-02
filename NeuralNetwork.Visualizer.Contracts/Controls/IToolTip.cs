using System;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IToolTip
   {
      void Show(string title, string text);
      void Close();
   }
}
