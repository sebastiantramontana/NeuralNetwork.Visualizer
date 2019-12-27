using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html5
{
   public class NeuralNetworkVisualizerControl : INeuralNetworkVisualizerControl
   {
      public event EventHandler<SelectionEventArgs<Bias>> SelectBias;
      public event EventHandler<SelectionEventArgs<Edge>> SelectEdge;
      public event EventHandler<SelectionEventArgs<Input>> SelectInput;
      public event EventHandler<SelectionEventArgs<InputLayer>> SelectInputLayer;
      public event EventHandler<SelectionEventArgs<Neuron>> SelectNeuron;
      public event EventHandler<SelectionEventArgs<NeuronLayer>> SelectNeuronLayer;

      public InputLayer InputLayer { get; set; }
      public IPreference Preferences { get; }
      public IEnumerable<Element> SelectedElements { get; }
      public float Zoom { get; set; }

      public Task<Image> ExportToImage()
      {
         throw new System.NotImplementedException();
      }

      public Task RedrawAsync()
      {
         throw new System.NotImplementedException();
      }

      public Task ResumeAutoRedraw()
      {
         throw new System.NotImplementedException();
      }

      public void SuspendAutoRedraw()
      {
         throw new System.NotImplementedException();
      }
   }
}
