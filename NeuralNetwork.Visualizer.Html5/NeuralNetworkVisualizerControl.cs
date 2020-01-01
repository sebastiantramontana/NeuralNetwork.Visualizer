using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html5
{
   public class NeuralNetworkVisualizerControl : INeuralNetworkVisualizerControl
   {
      public Model.Layers.InputLayer InputLayer { get; set; }
      public IPreference Preferences { get; }
      public System.Collections.Generic.IEnumerable<Model.Element> SelectedElements { get; }
      public float Zoom { get; set; }

      public event EventHandler<Contracts.Selection.SelectionEventArgs<Model.Nodes.Bias>> SelectBias;
      public event EventHandler<Contracts.Selection.SelectionEventArgs<Model.Nodes.Edge>> SelectEdge;
      public event EventHandler<Contracts.Selection.SelectionEventArgs<Model.Nodes.Input>> SelectInput;
      public event EventHandler<Contracts.Selection.SelectionEventArgs<Model.Layers.InputLayer>> SelectInputLayer;
      public event EventHandler<Contracts.Selection.SelectionEventArgs<Model.Nodes.Neuron>> SelectNeuron;
      public event EventHandler<Contracts.Selection.SelectionEventArgs<Model.Layers.NeuronLayer>> SelectNeuronLayer;

      public Task<Image> ExportToImage()
      {
         throw new NotImplementedException();
      }

      public Task RedrawAsync()
      {
         throw new NotImplementedException();
      }

      public Task ResumeAutoRedraw()
      {
         throw new NotImplementedException();
      }

      public void SuspendAutoRedraw()
      {
         throw new NotImplementedException();
      }
   }
}
