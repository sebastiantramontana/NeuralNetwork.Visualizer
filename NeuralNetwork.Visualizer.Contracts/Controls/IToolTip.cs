using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IToolTip
   {
      Task Show(string title, string text);
      Task Close();
   }
}
