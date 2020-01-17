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
      private readonly NeuralNetworkVisualizerControlDrawing _neuralNetworkVisualizerControlInner;
      private readonly Invoker _visualizerInvoker;
      private IDisposable _drawableSurface;

      public event EventHandler<SelectionEventArgs<InputLayer>> SelectInputLayer;
      public event EventHandler<SelectionEventArgs<NeuronLayer>> SelectNeuronLayer;
      public event EventHandler<SelectionEventArgs<Bias>> SelectBias;
      public event EventHandler<SelectionEventArgs<Input>> SelectInput;
      public event EventHandler<SelectionEventArgs<Neuron>> SelectNeuron;
      public event EventHandler<SelectionEventArgs<Edge>> SelectEdge;

      public NeuralNetworkVisualizerControl()
      {
         InitializeComponent();

         _visualizerInvoker = new Invoker(this);

         Func<IDrafter, IDrawableSurface> drawableSurfaceBuilder = (drafter) =>
         {
            var drawableSurface = new DrawableSurface(picCanvas, this, drafter, _visualizerInvoker);
            _drawableSurface = drawableSurface;

            return drawableSurface;
         };

         _neuralNetworkVisualizerControlInner = new NeuralNetworkVisualizerControlDrawing(new ToolTip(_visualizerInvoker, picCanvas), new RegionBuilder(), drawableSurfaceBuilder);

         CheckForIllegalCrossThreadCalls = true;
         this.BackColor = Color.White.ToGdi();

         picCanvas.MouseDown += PicCanvas_MouseDown;
         picCanvas.MouseMove += PicCanvas_MouseMove;
         picCanvas.MouseLeave += PicCanvas_MouseLeave;

         _neuralNetworkVisualizerControlInner.SelectInputLayer += _neuralNetworkVisualizerControlInner_SelectInputLayer;
         _neuralNetworkVisualizerControlInner.SelectNeuronLayer += _neuralNetworkVisualizerControlInner_SelectNeuronLayer;
         _neuralNetworkVisualizerControlInner.SelectBias += _neuralNetworkVisualizerControlInner_SelectBias;
         _neuralNetworkVisualizerControlInner.SelectInput += _neuralNetworkVisualizerControlInner_SelectInput;
         _neuralNetworkVisualizerControlInner.SelectNeuron += _neuralNetworkVisualizerControlInner_SelectNeuron;
         _neuralNetworkVisualizerControlInner.SelectEdge += _neuralNetworkVisualizerControlInner_SelectEdge;
      }

      private void _neuralNetworkVisualizerControlInner_SelectEdge(object sender, SelectionEventArgs<Edge> e)
      {
         SelectEdge?.Invoke(this, e);
      }

      private void _neuralNetworkVisualizerControlInner_SelectNeuron(object sender, SelectionEventArgs<Neuron> e)
      {
         SelectNeuron?.Invoke(this, e);
      }

      private void _neuralNetworkVisualizerControlInner_SelectInput(object sender, SelectionEventArgs<Input> e)
      {
         SelectInput?.Invoke(this, e);
      }

      private void _neuralNetworkVisualizerControlInner_SelectNeuronLayer(object sender, SelectionEventArgs<NeuronLayer> e)
      {
         SelectNeuronLayer?.Invoke(this, e);
      }

      private void _neuralNetworkVisualizerControlInner_SelectInputLayer(object sender, SelectionEventArgs<InputLayer> e)
      {
         SelectInputLayer?.Invoke(this, e);
      }

      private void _neuralNetworkVisualizerControlInner_SelectBias(object sender, SelectionEventArgs<Bias> e)
      {
         SelectBias?.Invoke(this, e);
      }

      [Browsable(false)]
      public IPreference Preferences => _neuralNetworkVisualizerControlInner.Preferences;

      [Browsable(false)]
      public InputLayer InputLayer
      {
         get => _neuralNetworkVisualizerControlInner.InputLayer;
         set => _neuralNetworkVisualizerControlInner.InputLayer = value;
      }

      [Browsable(false)]
      public IEnumerable<Element> SelectedElements => _neuralNetworkVisualizerControlInner.SelectedElements;

      [Browsable(false)]
      public float Zoom
      {
         get => _neuralNetworkVisualizerControlInner.Zoom;
         set => _neuralNetworkVisualizerControlInner.Zoom = value;
      }

      [Browsable(false)]
      Size INeuralNetworkVisualizerControl.Size => _neuralNetworkVisualizerControlInner.Size;

      [Browsable(false)]
      public Size DrawingSize => _neuralNetworkVisualizerControlInner.DrawingSize;

      public Image ExportToImage()
      {
         return _neuralNetworkVisualizerControlInner.ExportToImage();
      }

      public async Task RedrawAsync()
      {
         await _neuralNetworkVisualizerControlInner.RedrawAsync();
      }

      /// <summary>
      /// <para>Suspend auto redraw when Preferences.AutoRedrawMode is AutoRedrawSync or AutoRedrawAsync.</para>
      /// <c>Avoid auto readraw overhead when will be multiple changes on InputLayer model.</c>
      /// </summary>
      public void SuspendAutoRedraw()
      {
         _neuralNetworkVisualizerControlInner.SuspendAutoRedraw();
      }

      /// <summary>
      /// <para>Resume any previous auto redraw suspension.</para>
      /// <para>If Preferences.AutoRedrawMode is AutoRedrawSync or AutoRedrawAsync performs a redraw.</para>
      /// </summary>
      public async Task ResumeAutoRedrawAsync()
      {
         await _neuralNetworkVisualizerControlInner.ResumeAutoRedrawAsync();
      }

      protected override async void OnSizeChanged(EventArgs e)
      {
         if (_neuralNetworkVisualizerControlInner is null)
            return;

         await _neuralNetworkVisualizerControlInner.DispatchOnSizeChange();

         _visualizerInvoker?.SafeInvoke(() => base.OnSizeChanged(e));
      }

      private void PicCanvas_MouseDown(object sender, Winforms.MouseEventArgs e)
      {
         var Modifierkey = ModifierKeys switch
         {
            Winforms.Keys.Control => Keys.Control,
            Winforms.Keys.Shift => Keys.Shift,
            _ => Keys.None,
         };

         _neuralNetworkVisualizerControlInner.DispatchMouseDown(e.Location.ToVisualizer(), Modifierkey);
      }

      private void PicCanvas_MouseLeave(object sender, EventArgs e)
      {
         _neuralNetworkVisualizerControlInner.DispatchMouseLeave();
      }

      private void PicCanvas_MouseMove(object sender, Winforms.MouseEventArgs e)
      {
         _neuralNetworkVisualizerControlInner.DispatchMouseMove(e.Location.ToVisualizer());
      }

      protected override void Dispose(bool disposing)
      {
         _drawableSurface.Dispose();

         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
