using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Visualizer.Preferences.Core;
using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Selection
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
