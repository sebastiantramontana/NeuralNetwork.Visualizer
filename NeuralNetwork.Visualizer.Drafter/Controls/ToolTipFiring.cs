using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System.Linq;
using System.Text;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
   public class ToolTipFiring : IToolTipFiring
   {
      private readonly IToolTip _toolTip;
      private readonly ISelectionResolver _selectionResolver;

      public ToolTipFiring(IToolTip toolTip, ISelectionResolver selectionResolver)
      {
         _toolTip = toolTip;
         _selectionResolver = selectionResolver;
      }

      public void Show(Position position)
      {
         DestroyFiring();

         var elem = _selectionResolver.GetElementFromLocation(position);

         if (elem != null)
         {
            ShowToolTip(elem);
         }
      }

      public void Hide()
      {
         DestroyFiring();
      }

      private void DestroyFiring()
      {
         _toolTip.Close();
      }

      private void ShowToolTip(Element element)
      {
         string text = GetElementText(element);
         _toolTip.Show(element.Id, text);
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
   }
}
