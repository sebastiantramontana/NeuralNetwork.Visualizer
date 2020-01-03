using NeuralNetwork.Infrastructure.Winform;
using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Drawing;
using NeuralNetwork.Visualizer.Drawing.Controls;
using NeuralNetwork.Visualizer.Drawing.Selection;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using NeuralNetwork.Visualizer.Winform.Drawing.Controls;
using NeuralNetwork.Visualizer.Winform.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Winforms = System.Windows.Forms;

namespace NeuralNetwork.Visualizer.Winform
{
   public partial class NeuralNetworkVisualizerControl : Winforms.UserControl, INeuralNetworkVisualizerControl
   {
      private bool _readyToRedrawWhenPropertyChange = false;

      private readonly IControlCanvas _controlCanvas;
      private readonly IElementSelector _selector;
      private readonly Invoker _visualizerInvoker;
      private readonly ISelectionEventFiring _selectionEventFiring;
      private readonly IToolTipFiring _toolTipFiring;

      public event EventHandler<SelectionEventArgs<InputLayer>> SelectInputLayer;
      public event EventHandler<SelectionEventArgs<NeuronLayer>> SelectNeuronLayer;
      public event EventHandler<SelectionEventArgs<Bias>> SelectBias;
      public event EventHandler<SelectionEventArgs<Input>> SelectInput;
      public event EventHandler<SelectionEventArgs<Neuron>> SelectNeuron;
      public event EventHandler<SelectionEventArgs<Edge>> SelectEdge;

      public NeuralNetworkVisualizerControl()
      {
         InitializeComponent();

         var selectableElementRegisterResolver = new SelectableElementRegister();
         _selector = new ElementSelector(selectableElementRegisterResolver);
         _visualizerInvoker = new Invoker(this);
         var drafter = new Drafter(this, _selector, selectableElementRegisterResolver, selectableElementRegisterResolver, new RegionBuilder());
         _toolTipFiring = new ToolTipFiring(new ToolTip(_visualizerInvoker, picCanvas), this, selectableElementRegisterResolver);
         _selectionEventFiring = new SelectionEventFiring(this, _selector,
                                    () => this.SelectInputLayer,
                                    () => this.SelectNeuronLayer,
                                    () => this.SelectBias,
                                    () => this.SelectInput,
                                    () => this.SelectNeuron,
                                    () => this.SelectEdge);
         _controlCanvas = new ControlCanvas(this.picCanvas, this, drafter, _visualizerInvoker);

         Winforms.Control.CheckForIllegalCrossThreadCalls = true;
         this.BackColor = Color.White.ToGdi();

         picCanvas.MouseDown += PicCanvas_MouseDown;
         picCanvas.MouseMove += PicCanvas_MouseMove;
         picCanvas.MouseLeave += PicCanvas_MouseLeave;
      }

      private IPreference _preferences = null;
      [Browsable(false)]
      public IPreference Preferences => _preferences ?? (_preferences = Preference.Create(Preferences_PropertyChanged));

      private InputLayer _InputLayer = null;
      [Browsable(false)]
      public InputLayer InputLayer
      {
         get => _InputLayer;
         set => Task.Run(async () => await SetInputLayer(value));
      }

      [Browsable(false)]
      public IEnumerable<Element> SelectedElements => _selector.SelectedElements;

      private float _zoom = 1f;
      [Browsable(false)]
      public float Zoom
      {
         get => _zoom;
         set => Task.Run(async () => await MakeZoom(value));
      }

      Size INeuralNetworkVisualizerControl.Size => this.Size.ToVisualizer();

      public Size DrawingSize => picCanvas.ClientSize.ToVisualizer();

      public async Task<Image> ExportToImage()
      {
         return await Task.Run(() => _controlCanvas.GetImage().ToVisualizer());
      }

      public async Task RedrawAsync()
      {
         await _controlCanvas.RedrawAsync();
         FinishRedrawFromOuter();
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

      /// <summary>
      /// <para>Resume any previous auto redraw suspension.</para>
      /// <para>If Preferences.AutoRedrawMode is AutoRedrawSync or AutoRedrawAsync performs a redraw.</para>
      /// </summary>
      public async Task ResumeAutoRedraw()
      {
         _isAutoRedrawSuspended = false;
         await AutoRedraw();
      }

      private async void InputLayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         await AutoRedraw();
         _selector.MarkToBeRefreshed(_InputLayer);
      }

      private async void Preferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         if (e.PropertyName == nameof(this.Preferences.Selectable))
         {
            CheckSelectionPreferenceChanged();
         }

         await AutoRedraw();
      }

      private async Task SetInputLayer(InputLayer inputLayer)
      {
         _InputLayer = inputLayer;

         if (_InputLayer != null)
            _InputLayer.PropertyChanged += InputLayer_PropertyChanged;

         _zoom = 1f; //restart zoom
         _selector.UnselectAll();

         if (_readyToRedrawWhenPropertyChange)
         {
            await _controlCanvas.RedrawAsync();
         }
      }

      private async Task MakeZoom(float factor)
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
            await RedrawAsync();
         }
      }

      private void FinishRedrawFromOuter()
      {
         _readyToRedrawWhenPropertyChange = true;
      }

      private Size _previousSize = Contracts.Drawing.Core.Primitives.Size.Null;
      protected override async void OnSizeChanged(EventArgs e)
      {
         _previousSize = this.ClientSize.ToVisualizer();

         if (!this.ClientSize.IsEmpty)
         {
            if (!_previousSize.IsNull)
            {
               if (_readyToRedrawWhenPropertyChange)
               {
                  await _controlCanvas.RedrawAsync();
               }
            }
         }

         _visualizerInvoker?.SafeInvoke(() => base.OnSizeChanged(e));
      }

      private void PicCanvas_MouseDown(object sender, Winforms.MouseEventArgs e)
      {
         if (!_readyToRedrawWhenPropertyChange)
            return;

         var selectionEvent = Winforms.Control.ModifierKeys switch
         {
            Winforms.Keys.Control => SelectionEvent.Unselect,
            Winforms.Keys.Shift => SelectionEvent.AddToSelection,
            _ => SelectionEvent.SelectOnly,
         };

         _selectionEventFiring.FireSelectionEvent(e.Location.ToVisualizer(), selectionEvent);
      }

      private void PicCanvas_MouseLeave(object sender, EventArgs e)
      {
         if (!_readyToRedrawWhenPropertyChange)
            return;

         _toolTipFiring.Hide();
      }

      private void PicCanvas_MouseMove(object sender, Winforms.MouseEventArgs e)
      {
         if (!_readyToRedrawWhenPropertyChange)
            return;

         _toolTipFiring.Show(e.Location.ToVisualizer());
      }

      private T Constrain<T>(T low, T value, T max) where T : IComparable<T>
      {
         return (value.CompareTo(low) < 0 ? low : (value.CompareTo(max) > 0 ? max : value));
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
