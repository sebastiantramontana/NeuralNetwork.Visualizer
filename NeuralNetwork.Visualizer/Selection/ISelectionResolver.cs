using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using NeuralNetwork.Visualizer.Preferences.Core;

namespace NeuralNetwork.Visualizer.Selection
{
   internal interface ISelectionResolver
   {
      Element GetElementFromLocation(Position location);
      void SetCurrentRootCanvas(ICanvas currentRootCanvas);
      void MarkToBeRefreshed(InputLayer inputLayer);
   }
}
