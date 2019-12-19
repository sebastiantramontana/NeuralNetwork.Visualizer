using NeuralNetwork.Model;

namespace NeuralNetwork.Visualizer.Contracts.Selection
{
   public interface IElementSelectionChecker
   {
      bool IsSelected(Element element);
   }
}
