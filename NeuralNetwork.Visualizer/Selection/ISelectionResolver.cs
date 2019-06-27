using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Selection
{
   internal interface ISelectionResolver
   {
      Element GetElementFromLocation(Point location);
      void SetCurrentRootCanvas(ICanvas currentRootCanvas);
      void MarkToBeRefreshed(InputLayer inputLayer);
   }
}
