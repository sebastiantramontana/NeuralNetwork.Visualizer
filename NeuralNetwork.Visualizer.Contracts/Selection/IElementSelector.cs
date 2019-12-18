using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
   internal interface IElementSelector : IElementSelectionChecker
   {
      IEnumerable<Element> SelectedElements { get; }

      Element SelectOnly(Position position);
      Element AddToSelection(Position position);
      Element Unselect(Position position);
      void MarkToBeRefreshed(InputLayer inputLayer);
      void UnselectAll();
   }
}
