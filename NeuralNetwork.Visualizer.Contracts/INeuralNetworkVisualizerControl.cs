using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts
{
   public interface INeuralNetworkVisualizerControl
   {
      event EventHandler<SelectionEventArgs<Bias>> SelectBias;
      event EventHandler<SelectionEventArgs<Edge>> SelectEdge;
      event EventHandler<SelectionEventArgs<Input>> SelectInput;
      event EventHandler<SelectionEventArgs<InputLayer>> SelectInputLayer;
      event EventHandler<SelectionEventArgs<Neuron>> SelectNeuron;
      event EventHandler<SelectionEventArgs<NeuronLayer>> SelectNeuronLayer;

      InputLayer InputLayer { get; set; }
      IPreference Preferences { get; }
      IEnumerable<Element> SelectedElements { get; }
      float Zoom { get; set; }
      Size Size { get; }
      Size DrawingSize { get; }

      Task RedrawAsync();
      Task ResumeAutoRedraw();
      void SuspendAutoRedraw();
      Task<Image> ExportToImage();
   }
}