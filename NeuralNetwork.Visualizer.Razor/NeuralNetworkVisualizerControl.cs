using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Drawing;
using NeuralNetwork.Visualizer.Razor.Controls.ToolTip;
using NeuralNetwork.Visualizer.Razor.Drawing;
using NeuralNetwork.Visualizer.Razor.Infrastructure;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts;
using NeuralNetwork.Visualizer.Razor.Selection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor
{
   public abstract class NeuralNetworkVisualizerControl : ComponentBase, INeuralNetworkVisualizerControl
   {
      private NeuralNetworkVisualizerControlDrawing _neuralNetworkVisualizerControlInner;

      public event EventHandler<SelectionEventArgs<InputLayer>> SelectInputLayer;
      public event EventHandler<SelectionEventArgs<NeuronLayer>> SelectNeuronLayer;
      public event EventHandler<SelectionEventArgs<Bias>> SelectBias;
      public event EventHandler<SelectionEventArgs<Input>> SelectInput;
      public event EventHandler<SelectionEventArgs<Neuron>> SelectNeuron;
      public event EventHandler<SelectionEventArgs<Edge>> SelectEdge;

      public NeuralNetworkVisualizerControl()
      {
         this.GlobalInstanceName = "neuralnetwork_visualizer_" + Guid.NewGuid().ToString().Replace("-", "_");
      }

      public async Task InitContext(IJSRuntime jsRuntime)
      {
         var globalInstanceName = this.GlobalInstanceName;
         var jsInterop = new JsInterop(jsRuntime, globalInstanceName);
         var scriptFileRegistrarInclusion = GetScriptFileRegistrarInclusion(jsInterop);

         await RegisterScripts(scriptFileRegistrarInclusion, globalInstanceName);
         
         IDrawableSurface drawableSurfaceBuilder(IDrafter drafter)
         {
            var drawableSurface = new DrawableSurface(drafter, new CanvasBuilder(jsInterop, this.GlobalInstanceName), jsInterop);
            return drawableSurface;
         }

         _neuralNetworkVisualizerControlInner = new NeuralNetworkVisualizerControlDrawing(new ToolTipControl(jsInterop), new RegionBuilder(), drawableSurfaceBuilder);

         _neuralNetworkVisualizerControlInner.SelectInputLayer += NeuralNetworkVisualizerControlInner_SelectInputLayer;
         _neuralNetworkVisualizerControlInner.SelectNeuronLayer += NeuralNetworkVisualizerControlInner_SelectNeuronLayer;
         _neuralNetworkVisualizerControlInner.SelectBias += NeuralNetworkVisualizerControlInner_SelectBias;
         _neuralNetworkVisualizerControlInner.SelectInput += NeuralNetworkVisualizerControlInner_SelectInput;
         _neuralNetworkVisualizerControlInner.SelectNeuron += NeuralNetworkVisualizerControlInner_SelectNeuron;
         _neuralNetworkVisualizerControlInner.SelectEdge += NeuralNetworkVisualizerControlInner_SelectEdge;

         /*
         picCanvas.MouseDown += PicCanvas_MouseDown;
         picCanvas.MouseMove += PicCanvas_MouseMove;
         picCanvas.MouseLeave += PicCanvas_MouseLeave;
         */

         await Task.CompletedTask;
      }

      private async ValueTask RegisterScripts(IScriptFileRegistrarInclusion scriptFileRegistrarInclusion, string globalInstanceName)
      {
         Console.WriteLine("Registering global-instance-registration.js");

         await scriptFileRegistrarInclusion.Include("global-instance-registration.js")
            .Register(new GlobalInstanceScriptRegistration(), globalInstanceName);


         Console.WriteLine("Registering drawable-surface-registration.js");

         await scriptFileRegistrarInclusion.Include("drawable-surface-registration.js")
            .Register(new DrawableSurfaceScriptRegistration(), globalInstanceName);

         await scriptFileRegistrarInclusion.Include("tootltip-registration.js")
            .Register(new ToolTipScriptRegistration(), globalInstanceName);
      }

      private IScriptFileRegistrarInclusion GetScriptFileRegistrarInclusion(IJsInterop jsInterop)
      {
         return new ScriptFileRegistrarInclusion(new ScriptRegistrar(jsInterop), jsInterop, "NeuralNetwork.Visualizer.Assets/js/registrations/");
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

      public string GlobalInstanceName { get; }

      public IPreference Preferences => _neuralNetworkVisualizerControlInner.Preferences;

      public InputLayer InputLayer
      {
         get => _neuralNetworkVisualizerControlInner.InputLayer;
         set => _neuralNetworkVisualizerControlInner.InputLayer = value;
      }

      public IEnumerable<Element> SelectedElements => _neuralNetworkVisualizerControlInner.SelectedElements;

      public float Zoom
      {
         get => _neuralNetworkVisualizerControlInner.Zoom;
         set => _neuralNetworkVisualizerControlInner.Zoom = value;
      }

      public Size Size => _neuralNetworkVisualizerControlInner.Size;
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

      /*
      protected override async void OnSizeChanged(EventArgs e)
      {
         if (_neuralNetworkVisualizerControlInner is null)
            return;

         await _neuralNetworkVisualizerControlInner.DispatchOnSizeChange();
      }
      */

      /*
      private void PicCanvas_MouseDown(object sender, Winforms.MouseEventArgs e)
      {
         var modifierkey = ModifierKeys switch
         {
            Winforms.Keys.Control => Keys.Control,
            Winforms.Keys.Shift => Keys.Shift,
            _ => Keys.None,
         };

         _neuralNetworkVisualizerControlInner.DispatchMouseDown(e.Location.ToVisualizer(), modifierkey);
      }
      */

      private void PicCanvas_MouseLeave(object sender, EventArgs e)
      {
         _neuralNetworkVisualizerControlInner.DispatchMouseLeave();
      }

      /*
      private void PicCanvas_MouseMove(object sender, Winforms.MouseEventArgs e)
      {
         _neuralNetworkVisualizerControlInner.DispatchMouseMove(e.Location.ToVisualizer());
      }*/
   }
}
