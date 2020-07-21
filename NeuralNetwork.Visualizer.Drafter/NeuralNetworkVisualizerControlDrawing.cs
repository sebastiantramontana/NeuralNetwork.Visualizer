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
   public class NeuralNetworkVisualizerControlDrawing : INeuralNetworkVisualizerControl
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

      public NeuralNetworkVisualizerControlDrawing(IToolTip toolTip, IRegionBuilder regionBuilder, Func<IDrafter, IDrawableSurface> drawableSurfaceBuilder)
      {
         var selectableElementRegisterResolver = new SelectableElementRegister();
         _selector = new ElementSelector(selectableElementRegisterResolver);

         var drafter = new Drafter(this, _selector, selectableElementRegisterResolver, selectableElementRegisterResolver, regionBuilder);

         _toolTipFiring = new ToolTipFiring(toolTip, selectableElementRegisterResolver);
         _selectionEventFiring = new SelectionEventFiring(this, _selector,
                                    () => this.SelectInputLayer,
                                    () => this.SelectNeuronLayer,
                                    () => this.SelectBias,
                                    () => this.SelectInput,
                                    () => this.SelectNeuron,
                                    () => this.SelectEdge);
         _drawableSurface = drawableSurfaceBuilder(drafter);
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

      public Size Size => _drawableSurface.Size;
      public Size DrawingSize => _drawableSurface.DrawingSize;

      public Task RedrawAsync()
      {
         SetReadyForAutoRedraw();
         return _drawableSurface.RedrawAsync();
      }

      public Task ResumeAutoRedrawAsync()
      {
         _isAutoRedrawSuspended = false;
         return AutoRedraw();
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

      public Task<Image> ExportToImage()
      {
         return Task.FromResult(_drawableSurface.Image);
      }

      private Size _previousSize;
      public async Task DispatchOnSizeChange()
      {
         if (!CheckEventCanBeDispatched())
            return;

         var currentSize = _drawableSurface.Size;

         if (currentSize.IsNull || currentSize == _previousSize)
         {
            return;
         }

         _previousSize = currentSize;
         await _drawableSurface.RedrawAsync().ConfigureAwait(false);
      }

      public Task DispatchMouseDown(Position position, Keys modifierKeys)
      {
         if (!CheckEventCanBeDispatched())
            return Task.CompletedTask;

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

         return _selectionEventFiring.FireSelectionEvent(position, selectionEvent);
      }

      public void DispatchMouseHover(Position position)
      {
         if (!CheckEventCanBeDispatched())
            return;

         _toolTipFiring.Show(position);
      }

      private bool CheckEventCanBeDispatched()
      {
         return !(this.InputLayer is null) && _readyToRedrawWhenPropertyChange;
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

         await AutoRedraw().ConfigureAwait(false);
      }

      private async void InputLayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         await AutoRedraw().ConfigureAwait(false);
         _selector.MarkToBeRefreshed(_InputLayer);
      }

      private void CheckSelectionPreferenceChanged()
      {
         if (!this.Preferences.Selectable)
         {
            _selector.UnselectAll();
         }
      }

      private Task AutoRedraw()
      {
         if (!_isAutoRedrawSuspended && _readyToRedrawWhenPropertyChange && this.Preferences.AutoRedrawOnChanges)
         {
            return _drawableSurface.RedrawAsync();
         }

         return Task.CompletedTask;
      }

      private async void SetInputLayer(InputLayer inputLayer)
      {
         _InputLayer = inputLayer;

         if (_InputLayer != null)
            _InputLayer.PropertyChanged += InputLayer_PropertyChanged;

         _zoom = 1f;
         _selector.UnselectAll();

         if (_readyToRedrawWhenPropertyChange)
         {
            await RedrawAsync().ConfigureAwait(false);
         }
      }

      private async void MakeZoom(float factor)
      {
         if (_InputLayer == null)
         {
            return;
         }

         _zoom = Constrain(0.1f, factor, 10.0f); //limit the zoom value: Graphics will throw exception if not.

         if (_readyToRedrawWhenPropertyChange)
         {
            await RedrawAsync().ConfigureAwait(false);
         }
      }

      private T Constrain<T>(T low, T value, T max) where T : IComparable<T>
      {
         return (value.CompareTo(low) < 0 ? low : (value.CompareTo(max) > 0 ? max : value));
      }

   }
}
