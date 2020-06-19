using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal interface IDrawingRunner
   {
      Task Run(Func<Task> drawFunc);
   }
}
