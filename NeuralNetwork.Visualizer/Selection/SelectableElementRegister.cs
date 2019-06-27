using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Visualizer.Drawing.Canvas;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NeuralNetwork.Visualizer.Selection
{
   internal class SelectableElementRegister : ISelectableElementRegister, ISelectionResolver
   {
      private readonly IDictionary<Element, RegistrationInfo> _registeredElements;
      private ICanvas _currentRootCanvas;

      public SelectableElementRegister()
      {
         _registeredElements = new Dictionary<Element, RegistrationInfo>();
      }

      public Element GetElementFromLocation(Point location)
      {
         RefreshSelection();

         return _registeredElements
             .Values
             .Where(ri => ri.Region
                 .IsVisible(_currentRootCanvas
                     .Translate(location, ri.Canvas)))
             .OrderByDescending(ri => ri.ZIndex)
             .FirstOrDefault()?
             .Element;
      }

      private bool _needToBeRefreshed = false;
      private InputLayer _currentInputLayer;

      public void MarkToBeRefreshed(InputLayer inputLayer)
      {
         _currentInputLayer = inputLayer;
         _needToBeRefreshed = true;
      }

      public void Register(RegistrationInfo info)
      {
         _registeredElements[info.Element] = info;
      }

      public void SetCurrentRootCanvas(ICanvas currentRootCanvas)
      {
         _currentRootCanvas = currentRootCanvas;
      }

      private void RefreshSelection()
      {
         if (!_needToBeRefreshed)
         {
            return;
         }

         var registeredElementsToRemove = new List<Element>();

         foreach (var registeredElement in _registeredElements)
         {
            var foundElement = _currentInputLayer.Find(registeredElement.Key.Id);
            if (foundElement == null)
            {
               registeredElementsToRemove.Add(registeredElement.Key);
            }
         }

         foreach (var selectedElement in registeredElementsToRemove)
         {
            _registeredElements.Remove(selectedElement);
         }

         _needToBeRefreshed = false;
      }
   }
}
