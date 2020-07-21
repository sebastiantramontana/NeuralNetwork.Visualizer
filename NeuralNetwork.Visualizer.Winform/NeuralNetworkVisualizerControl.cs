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
using NeuralNetwork.Visualizer.Winform.Drawing;
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

         IDrawableSurface drawableSurfaceBuilder(IDrafter drafter)
         {
            var drawableSurface = new DrawableSurface(picCanvas, this, drafter, _visualizerInvoker, new GdiImageCanvasBuilder());
            _drawableSurface = drawableSurface;

            return drawableSurface;
         }

         _neuralNetworkVisualizerControlInner = new NeuralNetworkVisualizerControlDrawing(new ToolTip(_visualizerInvoker, picCanvas), new RegionBuilder(), drawableSurfaceBuilder);

         CheckForIllegalCrossThreadCalls = true;
         this.BackColor = Color.White.ToGdi();

         picCanvas.MouseDown += PicCanvas_MouseDown;
         picCanvas.MouseHover += PicCanvas_MouseHover;

         _neuralNetworkVisualizerControlInner.SelectInputLayer += NeuralNetworkVisualizerControlInner_SelectInputLayer;
         _neuralNetworkVisualizerControlInner.SelectNeuronLayer += NeuralNetworkVisualizerControlInner_SelectNeuronLayer;
         _neuralNetworkVisualizerControlInner.SelectBias += NeuralNetworkVisualizerControlInner_SelectBias;
         _neuralNetworkVisualizerControlInner.SelectInput += NeuralNetworkVisualizerControlInner_SelectInput;
         _neuralNetworkVisualizerControlInner.SelectNeuron += NeuralNetworkVisualizerControlInner_SelectNeuron;
         _neuralNetworkVisualizerControlInner.SelectEdge += NeuralNetworkVisualizerControlInner_SelectEdge;
      }

      private void NeuralNetworkVisualizerControlInner_SelectEdge(object sender, SelectionEventArgs<Edge> e)
      {
         SelectEdge?.Invoke(this, e);
      }

      private void NeuralNetworkVisualizerControlInner_SelectNeuron(object sender, SelectionEventArgs<Neuron> e)
      {
         SelectNeuron?.Invoke(this, e);
      }

      private void NeuralNetworkVisualizerControlInner_SelectInput(object sender, SelectionEventArgs<Input> e)
      {
         SelectInput?.Invoke(this, e);
      }

      private void NeuralNetworkVisualizerControlInner_SelectNeuronLayer(object sender, SelectionEventArgs<NeuronLayer> e)
      {
         SelectNeuronLayer?.Invoke(this, e);
      }

      private void NeuralNetworkVisualizerControlInner_SelectInputLayer(object sender, SelectionEventArgs<InputLayer> e)
      {
         SelectInputLayer?.Invoke(this, e);
      }

      private void NeuralNetworkVisualizerControlInner_SelectBias(object sender, SelectionEventArgs<Bias> e)
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

      Size INeuralNetworkVisualizerControl.Size => _neuralNetworkVisualizerControlInner.Size;
      public Size DrawingSize => _neuralNetworkVisualizerControlInner.DrawingSize;

      public Task<Image> ExportToImage()
      {
         return _neuralNetworkVisualizerControlInner.ExportToImage();
      }

      public Task RedrawAsync()
      {
         return _neuralNetworkVisualizerControlInner.RedrawAsync();
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
      public Task ResumeAutoRedrawAsync()
      {
         return _neuralNetworkVisualizerControlInner.ResumeAutoRedrawAsync();
      }

      protected override async void OnSizeChanged(EventArgs e)
      {
         if (_neuralNetworkVisualizerControlInner is null)
            return;

         await _neuralNetworkVisualizerControlInner.DispatchOnSizeChange();

         _visualizerInvoker?.SafeInvoke(() => base.OnSizeChanged(e));
      }

      private async void PicCanvas_MouseDown(object sender, Winforms.MouseEventArgs e)
      {
         var Modifierkey = ModifierKeys switch
         {
            Winforms.Keys.Control => Keys.Control,
            Winforms.Keys.Shift => Keys.Shift,
            _ => Keys.None,
         };

         await _neuralNetworkVisualizerControlInner.DispatchMouseDown(e.Location.ToVisualizer(), Modifierkey);
      }

      private void PicCanvas_MouseHover(object sender, EventArgs e)
      {
         var cursorPosition = picCanvas
            .PointToClient(Winforms.Cursor.Position)
            .ToVisualizer();

         _neuralNetworkVisualizerControlInner.DispatchMouseHover(cursorPosition);
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
