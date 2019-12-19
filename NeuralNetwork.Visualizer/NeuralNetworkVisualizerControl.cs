using NeuralNetwork.Infrastructure.Winform;
using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Drawing.Canvas.GdiMapping;
using NeuralNetwork.Visualizer.Drawing.Controls;
using NeuralNetwork.Visualizer.Preferences;
using NeuralNetwork.Visualizer.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork.Visualizer
{
   public partial class NeuralNetworkVisualizerControl : UserControl, INeuralNetworkVisualizerControl
   {
      private bool _redrawWhenPropertyChange = false;

      private readonly IControlDrawing _controlDrawing;
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

         _controlDrawing = new ControlDrawing(new ControlCanvas(this.picCanvas, this, _visualizerInvoker), _selector, selectableElementRegisterResolver, selectableElementRegisterResolver, _visualizerInvoker);
         _toolTipFiring = new ToolTipFiring(this, picCanvas, selectableElementRegisterResolver, _visualizerInvoker);
         _selectionEventFiring = new SelectionEventFiring(this, _selector,
                                    () => this.SelectInputLayer,
                                    () => this.SelectNeuronLayer,
                                    () => this.SelectBias,
                                    () => this.SelectInput,
                                    () => this.SelectNeuron,
                                    () => this.SelectEdge);

         Control.CheckForIllegalCrossThreadCalls = true;
         this.BackColor = Color.White.ToGdi();

         picCanvas.MouseDown += PicCanvas_MouseDown;
         picCanvas.MouseMove += PicCanvas_MouseMove;
         picCanvas.MouseLeave += PicCanvas_MouseLeave;
      }

      [Browsable(false)]
      public IPreference Preferences { get; } = Preference.Create();

      private InputLayer _InputLayer = null;
      [Browsable(false)]
      public InputLayer InputLayer
      {
         get
         {
            return _InputLayer;
         }
         set
         {
            _InputLayer = value;

            if (_InputLayer != null)
               _InputLayer.PropertyChanged += InputLayer_PropertyChanged;

            _zoom = 1f; //restart zoom
            _selector.UnselectAll();

            if (_redrawWhenPropertyChange)
            {
               RedrawInternal();
            }
         }
      }

      public IEnumerable<Element> SelectedElements => _selector.SelectedElements;

      private bool _selectable = false;
      public bool Selectable
      {
         get => _selectable;
         set
         {
            _selectable = value;

            if (!_selectable)
            {
               _selector.UnselectAll();

               if (_redrawWhenPropertyChange)
               {
                  RedrawInternal();
               }
            }
         }
      }

      private float _zoom = 1f;
      [Browsable(false)]
      public float Zoom
      {
         get => _zoom;
         set
         {
            if (_InputLayer == null)
            {
               return; //nothing to do
            }

            _zoom = Constrain(0.1f, value, 10.0f); //limit the zoom value: Graphics will throw exception if not.

            if (_redrawWhenPropertyChange)
            {
               RedrawInternal();
            }
         }
      }

      public Task<Image> ExportToImage()
      {
         return _controlDrawing.GetImage();
      }

      public void Redraw()
      {
         RedrawInternal();
         FinishRedrawFromOuter();
      }

      public async Task RedrawAsync()
      {
         await RedrawInternalAsync();
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

      private async Task AutoRedraw()
      {
         if (_isAutoRedrawSuspended)
         {
            return;
         }

         switch (Preferences.AutoRedrawMode)
         {
            case AutoRedrawMode.AutoRedrawSync:
               Redraw();
               break;
            case AutoRedrawMode.AutoRedrawAsync:
               await RedrawAsync();
               break;
            case AutoRedrawMode.NoAutoRedraw:
            default:
               break;
         }
      }

      private void FinishRedrawFromOuter()
      {
         _redrawWhenPropertyChange = true;
      }

      private void RedrawInternal()
      {
         _controlDrawing?.Redraw();
      }

      private async Task RedrawInternalAsync()
      {
         await _controlDrawing?.RedrawAsync();
      }

      private Size _previousSize = Contracts.Drawing.Core.Primitives.Size.Null;
      protected override async void OnSizeChanged(EventArgs e)
      {
         _previousSize = this.ClientSize.ToVisualizer();

         if (!this.ClientSize.IsEmpty)
         {
            if (!_previousSize.IsNull)
            {
               if (Preferences.AsyncRedrawOnResize)
               {
                  if (_redrawWhenPropertyChange)
                  {
                     await RedrawInternalAsync();
                  }
               }
               else
               {
                  if (_redrawWhenPropertyChange)
                  {
                     RedrawInternal();
                  }
               }
            }
         }

         _visualizerInvoker?.SafeInvoke(() => base.OnSizeChanged(e));
      }

      private void PicCanvas_MouseDown(object sender, MouseEventArgs e)
      {
         if (!_redrawWhenPropertyChange)
            return;

         _selectionEventFiring.FireSelectionEvent(e.Location.ToVisualizer());
      }

      private void PicCanvas_MouseLeave(object sender, EventArgs e)
      {
         if (!_redrawWhenPropertyChange)
            return;

         _toolTipFiring.Hide();
      }

      private void PicCanvas_MouseMove(object sender, MouseEventArgs e)
      {
         if (!_redrawWhenPropertyChange)
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
