using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal class BiasDrawing : SimpleNodeDrawing<Bias>
   {
      internal BiasDrawing(Bias element, IPreference preferences, SimpleNodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker) : base(element, preferences.Biases, cache, selectionChecker, selectableElementRegister)
      {
      }
   }
}
