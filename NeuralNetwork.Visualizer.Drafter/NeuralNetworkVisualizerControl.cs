using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Drawing.Controls;
using NeuralNetwork.Visualizer.Drawing.Selection;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Winform.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing
{
   public class NeuralNetworkVisualizerControl : INeuralNetworkVisualizerControl
   {
      private bool _readyToRedrawWhenPropertyChange = false;

      private readonly IElementSelector _selector;
      private readonly ISelectionEventFiring _selectionEventFiring;
      private readonly IToolTipFiring _toolTipFiring;
      private readonly IDrawableSurface _drawableSurface;

      public event EventHandler<SelectionEventArgs<Bias>> SelectBias;
      public event EventHandler<SelectionEventArgs<Edge>> SelectEdge;
      public event EventHandler<SelectionEventArgs<Input>> SelectInput;
      public event EventHandler<SelectionEventArgs<InputLayer>> SelectInputLayer;
      public event EventHandler<SelectionEventArgs<Neuron>> SelectNeuron;
      public event EventHandler<SelectionEventArgs<NeuronLayer>> SelectNeuronLayer;

      public NeuralNetworkVisualizerControl(IToolTip toolTip, IRegionBuilder regionBuilder, IDrawableSurface drawableSurface)
      {
         var selectableElementRegisterResolver = new SelectableElementRegister();
         _selector = new ElementSelector(selectableElementRegisterResolver);

         var drafter = new Drafter(this, _selector, selectableElementRegisterResolver, selectableElementRegisterResolver, regionBuilder);
         _toolTipFiring = new ToolTipFiring(toolTip, this, selectableElementRegisterResolver);
         _selectionEventFiring = new SelectionEventFiring(this, _selector,
                                    () => this.SelectInputLayer,
                                    () => this.SelectNeuronLayer,
                                    () => this.SelectBias,
                                    () => this.SelectInput,
                                    () => this.SelectNeuron,
                                    () => this.SelectEdge);
         _drawableSurface = drawableSurface;
      }

      private InputLayer _InputLayer = null;
      public InputLayer InputLayer
      {
         get => _InputLayer;
         set => SetInputLayer(value);
      }

      private IPreference _preferences = null;
      public IPreference Preferences => _preferences ?? (_preferences = Preference.Create(Preferences_PropertyChanged));

      public IEnumerable<Element> SelectedElements => _selector.SelectedElements;

      private float _zoom = 1f;
      public float Zoom
      {
         get => _zoom;
         set => MakeZoom(value);
      }

      Size INeuralNetworkVisualizerControl.Size => _drawableSurface.Size;

      public Size DrawingSize => _drawableSurface.DrawingSize;

      public async Task RedrawAsync()
      {
         await _drawableSurface.RedrawAsync();
         SetReadyForAutoRedraw();
      }

      public Task ResumeAutoRedrawAsync()
      {
         throw new NotImplementedException();
      }

      private bool _isAutoRedrawSuspended = false;
      /// <summary>
      /// <para>Suspend auto redraw when Preferences.AutoRedrawMode is AutoRedrawSync or AutoRedrawAsync.</para>
      /// <c>Avoid auto readraw overhead when will be multiple changes on InputLayer model.</c>
      /// </summary>
      public void SuspendAutoRedraw()
      {
         _isAutoRedrawSuspended = true;
      }

      public async Task<Image> ExportToImage()
      {
         return await _drawableSurface.GetImage();
      }

      private Size _previousSize;
      public async Task DispatchOnSizeChange()
      {
         var currentSize = _drawableSurface.Size;

         if (_drawableSurface.Size.IsNull || currentSize == _previousSize || !_readyToRedrawWhenPropertyChange)
         {
            return;
         }

         _previousSize = currentSize;
         await _drawableSurface.RedrawAsync();
      }

      public void DispatchMouseDown(Position position, Keys modifierKeys)
      {
         if (!_readyToRedrawWhenPropertyChange)
            return;

         SelectionEvent selectionEvent;

         switch (modifierKeys)
         {
            case Keys.Control:
               selectionEvent = SelectionEvent.Unselect;
               break;
            case Keys.Shift:
               selectionEvent = SelectionEvent.AddToSelection;
               break;
            case Keys.None:
            default:
               selectionEvent = SelectionEvent.SelectOnly;
               break;
         }

         _selectionEventFiring.FireSelectionEvent(position, selectionEvent);
      }

      public void DispatchMouseLeave()
      {
         if (!_readyToRedrawWhenPropertyChange)
            return;

         _toolTipFiring.Hide();
      }

      public void DispatchMouseMove(Position position)
      {
         if (!_readyToRedrawWhenPropertyChange)
            return;

         _toolTipFiring.Show(position);
      }

      private void SetReadyForAutoRedraw()
      {
         _readyToRedrawWhenPropertyChange = true;
      }

      private async void Preferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         if (e.PropertyName == nameof(this.Preferences.Selectable))
         {
            CheckSelectionPreferenceChanged();
         }

         await AutoRedraw();
      }

      private async void InputLayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         await AutoRedraw();
         _selector.MarkToBeRefreshed(_InputLayer);
      }

      private void CheckSelectionPreferenceChanged()
      {
         if (!this.Preferences.Selectable)
         {
            _selector.UnselectAll();
         }
      }

      private async Task AutoRedraw()
      {
         if (!_isAutoRedrawSuspended && _readyToRedrawWhenPropertyChange && this.Preferences.AutoRedrawOnChanges)
         {
            await _drawableSurface.RedrawAsync();
         }
      }

      private async void SetInputLayer(InputLayer inputLayer)
      {
         _InputLayer = inputLayer;

         if (_InputLayer != null)
            _InputLayer.PropertyChanged += InputLayer_PropertyChanged;

         _zoom = 1f; //restart zoom
         _selector.UnselectAll();

         if (_readyToRedrawWhenPropertyChange)
         {
            await RedrawAsync();
         }
      }

      private async void MakeZoom(float factor)
      {
         if (_InputLayer == null)
         {
            return; //nothing to do
         }

         _zoom = Constrain(0.1f, factor, 10.0f); //limit the zoom value: Graphics will throw exception if not.

         if (_readyToRedrawWhenPropertyChange)
         {
            await RedrawAsync();
         }
      }

      private T Constrain<T>(T low, T value, T max) where T : IComparable<T>
      {
         return (value.CompareTo(low) < 0 ? low : (value.CompareTo(max) > 0 ? max : value));
      }

   }
}
