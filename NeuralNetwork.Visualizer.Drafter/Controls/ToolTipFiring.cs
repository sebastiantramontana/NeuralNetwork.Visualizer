using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
   public class ToolTipFiring : IToolTipFiring
   {
      private readonly IToolTip _toolTip;
      private readonly INeuralNetworkVisualizerControl _control;
      private readonly ISelectionResolver _selectionResolver;

      private Timer _timeout = null;
      private Position _lastToolTipLocation = null;

      public ToolTipFiring(IToolTip toolTip, INeuralNetworkVisualizerControl control, ISelectionResolver selectionResolver)
      {
         _toolTip = toolTip;
         _control = control;
         _selectionResolver = selectionResolver;
         _lastToolTipLocation = new Position(0, 0);
      }

      public void Show(Position position)
      {
         if (!Validate(position))
            return;

         DestroyFiring();

         _timeout = CreateTimer();

         _timeout.Elapsed += (s, ev) =>
         {
            DestroyFiring();

            var elem = _selectionResolver.GetElementFromLocation(position);

            if (elem != null)
            {
               ShowToolTip(elem);
               _lastToolTipLocation = position;
            }
         };

         _timeout.Start();
      }

      public void Hide()
      {
         DestroyFiring();
      }

      private void DestroyFiring()
      {
         Destroy.Disposable(ref _timeout);
         _toolTip.Close();
      }

      private void ShowToolTip(Element element)
      {
         string text = GetElementText(element);
         _toolTip.Show(element.Id, text);
      }

      private Timer CreateTimer()
      {
         return new Timer
         {
            AutoReset = false,
            Enabled = true,
            Interval = 500
         };
      }

      private string GetElementText(Element element)
      {
         StringBuilder builder = new StringBuilder();

         switch (element)
         {
            case InputLayer inputLayer:
               AddInputLayerText(inputLayer, builder);
               break;
            case NeuronLayer neuronLayer:
               AddNeuronLayerText(neuronLayer, builder);
               break;
            case Bias bias:
               AddBiasText(bias, builder);
               break;
            case Input input:
               AddInputText(input, builder);
               break;
            case Neuron neuron:
               AddNeuronText(neuron, builder);
               break;
            case Edge edge:
               AddEdgeText(edge, builder);
               break;
         }

         AddElementText(element, builder);
         return builder.ToString();
      }

      private void AddInputLayerText(InputLayer layer, StringBuilder builder)
      {
         AddLayerText(layer, builder);
      }

      private void AddNeuronLayerText(NeuronLayer layer, StringBuilder builder)
      {
         AddLayerText(layer, builder);
         builder.AppendLine("Previous layer: " + layer.Previous.Id);
      }

      private void AddBiasText(Bias bias, StringBuilder builder)
      {
         AddNodeText(bias, builder);
      }

      private void AddInputText(Input input, StringBuilder builder)
      {
         AddNodeText(input, builder);
      }

      private void AddNeuronText(Neuron neuron, StringBuilder builder)
      {
         string actFunc = neuron.ActivationFunction.ToString();
         string sumValue = neuron.SumValue?.ToString() ?? "(none)";
         string edgesCount = neuron.Edges.Count().ToString();

         builder.AppendLine("Activation function: " + actFunc);
         builder.AppendLine("Sum value: " + sumValue);

         AddNodeText(neuron, builder);

         builder.AppendLine("Edges: " + edgesCount);
      }

      private void AddEdgeText(Edge edge, StringBuilder builder)
      {
         string source = edge.Source.Id;
         string destination = edge.Destination.Id;
         string weight = edge.Weight?.ToString() ?? "(none)";

         builder.AppendLine("Source: " + source);
         builder.AppendLine("Destination: " + destination);
         builder.AppendLine("Weight: " + weight);
      }

      private void AddNodeText(NodeBase node, StringBuilder builder)
      {
         string outputValue = node.OutputValue?.ToString() ?? "(none)";
         string layer = node.Layer.Id;

         builder.AppendLine("Output value: " + outputValue);
         builder.AppendLine("Layer: " + layer);
      }

      private void AddLayerText(LayerBase layer, StringBuilder builder)
      {
         string bias = layer.Bias?.Id ?? "(none)";
         string nodesCount = layer.GetAllNodes().Count().ToString();
         string next = layer.Next?.Id ?? "(this is the output layer)";

         builder.AppendLine("Bias: " + bias);
         builder.AppendLine("Nodes count: " + nodesCount);
         builder.AppendLine("Next layer: " + next);
      }

      private void AddElementText(Element element, StringBuilder builder)
      {
         string strobj = element.Object?.ToString() ?? "(none)";
         builder.AppendLine("Object: " + strobj);
      }

      private bool Validate(Position position)
      {
         return _control.InputLayer != null && ValidateLocation(position);
      }

      private bool ValidateLocation(Position position)
      {
         return Math.Abs(position.X - _lastToolTipLocation.X) > 5 || Math.Abs(position.Y - _lastToolTipLocation.Y) > 5;
      }
   }
}
