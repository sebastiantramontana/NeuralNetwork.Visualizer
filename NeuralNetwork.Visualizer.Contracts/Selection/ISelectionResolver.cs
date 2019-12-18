using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
   internal interface ISelectionResolver
   {
      Element GetElementFromLocation(Position location);
      void SetCurrentRootCanvas(ICanvas currentRootCanvas);
      void MarkToBeRefreshed(InputLayer inputLayer);
   }
}
