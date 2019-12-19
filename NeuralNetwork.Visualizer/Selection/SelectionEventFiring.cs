using NeuralNetwork.Model;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Windows.Forms;
using SelectBias = System.Func<System.EventHandler<NeuralNetwork.Visualizer.Contracts.Selection.SelectionEventArgs<NeuralNetwork.Model.Nodes.Bias>>>;
using SelectEdge = System.Func<System.EventHandler<NeuralNetwork.Visualizer.Contracts.Selection.SelectionEventArgs<NeuralNetwork.Model.Nodes.Edge>>>;
using SelectInput = System.Func<System.EventHandler<NeuralNetwork.Visualizer.Contracts.Selection.SelectionEventArgs<NeuralNetwork.Model.Nodes.Input>>>;
using SelectInputLayer = System.Func<System.EventHandler<NeuralNetwork.Visualizer.Contracts.Selection.SelectionEventArgs<NeuralNetwork.Model.Layers.InputLayer>>>;
using SelectNeuron = System.Func<System.EventHandler<NeuralNetwork.Visualizer.Contracts.Selection.SelectionEventArgs<NeuralNetwork.Model.Nodes.Neuron>>>;
using SelectNeuronLayer = System.Func<System.EventHandler<NeuralNetwork.Visualizer.Contracts.Selection.SelectionEventArgs<NeuralNetwork.Model.Layers.NeuronLayer>>>;

namespace NeuralNetwork.Visualizer.Selection
{
   internal class SelectionEventFiring : ISelectionEventFiring
   {
      private readonly NeuralNetworkVisualizerControl _control;
      private readonly IElementSelector _selector;

      private readonly SelectInputLayer _selectInputLayer;
      private readonly SelectNeuronLayer _selectNeuronLayer;
      private readonly SelectBias _selectBias;
      private readonly SelectInput _selectInput;
      private readonly SelectNeuron _selectNeuron;
      private readonly SelectEdge _selectEdge;

      internal SelectionEventFiring(NeuralNetworkVisualizerControl control, IElementSelector selector,
          SelectInputLayer selectInputLayer,
          SelectNeuronLayer selectNeuronLayer,
          SelectBias selectBias,
          SelectInput selectInput,
          SelectNeuron selectNeuron,
          SelectEdge selectEdge)
      {
         _control = control;
         _selector = selector;

         _selectInputLayer = selectInputLayer;
         this._selectNeuronLayer = selectNeuronLayer;
         _selectBias = selectBias;
         _selectInput = selectInput;
         _selectNeuron = selectNeuron;
         _selectEdge = selectEdge;
      }

      public void FireSelectionEvent(Position position)
      {
         if (!_control.Preferences.Selectable)
            return;

         Func<Position, Element> selectFunc;
         bool isSelected;

         switch (Control.ModifierKeys)
         {
            case Keys.Control:
               selectFunc = _selector.Unselect;
               isSelected = false;
               break;
            case Keys.Shift:
               selectFunc = _selector.AddToSelection;
               isSelected = true;
               break;
            default:
               selectFunc = _selector.SelectOnly;
               isSelected = true;
               break;
         }

         var element = selectFunc(position);

         if (element == null)
         {
            return;
         }

         FireSelectionEvent(element, isSelected);
         _control.Redraw();
      }

      private void FireSelectionEvent(Element element, bool isSelected)
      {
         if (FireSelectionEvent(element, isSelected, _selectInputLayer))
            return;

         if (FireSelectionEvent(element, isSelected, _selectNeuronLayer))
            return;

         if (FireSelectionEvent(element, isSelected, _selectBias))
            return;

         if (FireSelectionEvent(element, isSelected, _selectInput))
            return;

         if (FireSelectionEvent(element, isSelected, _selectNeuron))
            return;

         if (FireSelectionEvent(element, isSelected, _selectEdge))
            return;
      }

      private bool FireSelectionEvent<TElement>(Element element, bool isSelected, Func<EventHandler<SelectionEventArgs<TElement>>> eventFunc) where TElement : Element
      {
         var fired = false;

         if (element is TElement typedElement)
         {
            var eventHandler = eventFunc();
            eventHandler?.Invoke(_control, new SelectionEventArgs<TElement>(typedElement, isSelected));
            fired = true;
         }

         return fired;
      }
   }
}
