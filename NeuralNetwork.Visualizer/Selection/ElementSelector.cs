using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Visualizer.Selection
{
   internal class ElementSelector : IElementSelector
   {
      private readonly ICollection<Element> _selectedElements;
      private readonly ISelectionResolver _selectionResolver;

      internal ElementSelector(ISelectionResolver selectionResolver)
      {
         _selectedElements = new List<Element>();
         _selectionResolver = selectionResolver;
      }

      public IEnumerable<Element> SelectedElements
      {
         get
         {
            RefreshSelection();
            return _selectedElements.ToArray();
         }
      }

      public bool IsSelected(Element element)
      {
         RefreshSelection();
         return _selectedElements.Contains(element);
      }

      public Element AddToSelection(Position position)
      {
         RefreshSelection();

         var elem = _selectionResolver.GetElementFromLocation(position);

         if (elem != null && !_selectedElements.Contains(elem))
         {
            _selectedElements.Add(elem);
         }

         return elem;
      }

      public Element SelectOnly(Position position)
      {
         _needToBeRefreshed = false;
         _selectedElements.Clear();

         return AddToSelection(position);
      }

      public Element Unselect(Position position)
      {
         RefreshSelection();

         var elem = _selectionResolver.GetElementFromLocation(position);

         if (elem == null || !_selectedElements.Contains(elem))
         {
            return null;
         }

         _selectedElements.Remove(elem);
         return elem;
      }

      public void UnselectAll()
      {
         _needToBeRefreshed = false;
         _selectedElements.Clear();
      }

      private bool _needToBeRefreshed = false;
      private InputLayer _currentInputLayer;

      public void MarkToBeRefreshed(InputLayer inputLayer)
      {
         _currentInputLayer = inputLayer;
         _needToBeRefreshed = true;
         _selectionResolver.MarkToBeRefreshed(inputLayer);
      }

      private void RefreshSelection()
      {
         if (!_needToBeRefreshed)
         {
            return;
         }

         var selectedElementsToRemove = new List<Element>();

         foreach (var element in _selectedElements)
         {
            var foundElement = _currentInputLayer.Find(element.Id);
            if (foundElement == null)
            {
               selectedElementsToRemove.Add(element);
            }
         }

         foreach (var selectedElement in selectedElementsToRemove)
         {
            _selectedElements.Remove(selectedElement);
         }

         _needToBeRefreshed = false;
      }
   }
}
