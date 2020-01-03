using NeuralNetwork.Model;
using NeuralNetwork.Model.Layers;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using NeuralNetwork.Visualizer.Preferences.Formatting;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
   public partial class Form1 : Form
   {
      private InputLayer _input;
      public Form1()
      {
         InitializeComponent();

         NeuralNetworkVisualizerControl1.SelectBias += NeuralNetworkVisualizerControl1_SelectBias;
         NeuralNetworkVisualizerControl1.SelectEdge += NeuralNetworkVisualizerControl1_SelectEdge;
         NeuralNetworkVisualizerControl1.SelectInput += NeuralNetworkVisualizerControl1_SelectInput;
         NeuralNetworkVisualizerControl1.SelectInputLayer += NeuralNetworkVisualizerControl1_SelectInputLayer;
         NeuralNetworkVisualizerControl1.SelectNeuron += NeuralNetworkVisualizerControl1_SelectNeuron;
         NeuralNetworkVisualizerControl1.SelectNeuronLayer += NeuralNetworkVisualizerControl1_SelectNeuronLayer;
      }

      private void NeuralNetworkVisualizerControl1_SelectNeuronLayer(object sender, SelectionEventArgs<NeuronLayer> e)
      {
         ShowSelectedElements();
         ShowLastSelection($"Neuron Layer: {e.Element.Id}", e.IsSelected);
      }

      private void NeuralNetworkVisualizerControl1_SelectNeuron(object sender, SelectionEventArgs<Neuron> e)
      {
         ShowSelectedElements();
         ShowLastSelection($"Neuron: {e.Element.Id}", e.IsSelected);
      }

      private void NeuralNetworkVisualizerControl1_SelectInputLayer(object sender, SelectionEventArgs<InputLayer> e)
      {
         ShowSelectedElements();
         ShowLastSelection($"Input Layer: {e.Element.Id}", e.IsSelected);
      }

      private void NeuralNetworkVisualizerControl1_SelectInput(object sender, SelectionEventArgs<Input> e)
      {
         ShowSelectedElements();
         ShowLastSelection($"Input: {e.Element.Id}", e.IsSelected);
      }

      private void NeuralNetworkVisualizerControl1_SelectEdge(object sender, SelectionEventArgs<Edge> e)
      {
         ShowSelectedElements();
         ShowLastSelection($"Edge: {e.Element.Id}", e.IsSelected);
      }

      private void NeuralNetworkVisualizerControl1_SelectBias(object sender, SelectionEventArgs<Bias> e)
      {
         ShowSelectedElements();
         ShowLastSelection($"Bias: {e.Element.Id}", e.IsSelected);
      }

      private void ShowSelectedElements()
      {
         txtSelectedElements.Text = string.Join(", ", NeuralNetworkVisualizerControl1.SelectedElements.Select(se => se.Id));
      }

      private void ShowLastSelection(string text, bool isSelected)
      {
         txtLastSelected.BackColor = isSelected ? Color.LightGreen.ToGdi() : Color.LightPink.ToGdi();
         txtLastSelected.Text = text;
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         NeuralNetworkVisualizerControl1.Preferences.AutoRedrawOnChanges = true;
         NeuralNetworkVisualizerControl1.Preferences.Quality = RenderQuality.High;

         cboQuality.Items.Add(RenderQuality.Low);
         cboQuality.Items.Add(RenderQuality.Medium);
         cboQuality.Items.Add(RenderQuality.High);

         cboQuality.SelectedItem = NeuralNetworkVisualizerControl1.Preferences.Quality;

         NeuralNetworkVisualizerControl1.Preferences.Inputs.OutputValueFormatter = new ByValueSignFormatter<FontLabel>(
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Red)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Gray)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Black)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Black))
         );

         NeuralNetworkVisualizerControl1.Preferences.Neurons.OutputValueFormatter = new ByValueSignFormatter<FontLabel>(
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Red)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Gray)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Black)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Black))
         );

         NeuralNetworkVisualizerControl1.Preferences.Edges.WeightFormatter = new ByValueSignFormatter<FontLabel>(
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Red)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Gray)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Black)),
             new FontLabel(FontLabel.Default, new SolidBrush(Color.Black))
         );

         NeuralNetworkVisualizerControl1.Preferences.Edges.ConnectorFormatter = new CustomFormatter<Pen>((v) => v == 0.0 ? Pen.BasicFromColor(Color.LightGray) : Pen.BasicFromColor(Color.Black));

         //To remove layer's titles
         NeuralNetworkVisualizerControl1.Preferences.Layers.Title = null;
      }

      private void btnStart_Click(object sender, EventArgs e)
      {
         _input = new InputLayer("Input")
         {
            Bias = new Bias("bias") { OutputValue = 1.234 }
         };

         _input.AddNode(new Input("e1") { OutputValue = 0.255 });
         _input.AddNode(new Input("e2") { OutputValue = 0.455 });
         _input.AddNode(new Input("e3") { OutputValue = -0.78967656 });
         _input.AddNode(new Input("e4") { OutputValue = 0.0 });
         //_input.AddNode(new Input("e5") { OutputValue = 0.255 });
         //_input.AddNode(new Input("e6") { OutputValue = 0.455 });
         //_input.AddNode(new Input("e7") { OutputValue = -0.78967656 });
         //_input.AddNode(new Input("e8") { OutputValue = 0.011 });
         //_input.AddNode(new Input("e9") { OutputValue = 0.2255 });
         //_input.AddNode(new Input("e10") { OutputValue = 43.455 });
         //_input.AddNode(new Input("e11") { OutputValue = -11.67656 });
         //_input.AddNode(new Input("e12") { OutputValue = -1.001 });

         var hidden = new NeuronLayer("Hidden");

         hidden.AddNode(new Neuron("o1") { ActivationFunction = ActivationFunction.Tanh, OutputValue = 2.364, SumValue = 2.364 });
         hidden.AddNode(new Neuron("o2") { ActivationFunction = ActivationFunction.LeakyRelu, OutputValue = -0.552, SumValue = 55.44 });
         hidden.AddNode(new Neuron("o4") { ActivationFunction = ActivationFunction.Relu, OutputValue = 1.324, SumValue = 4.34 });
         //hidden.AddNode(new Neuron("o3") { ActivationFunction = ActivationFunction.Linear, OutputValue = 0.0, SumValue = 19.22 });
         //hidden.AddNode(new Neuron("o5") { ActivationFunction = ActivationFunction.Sigmoid, OutputValue = -0.12, SumValue = 25.224 });
         //hidden.AddNode(new Neuron("o6") { ActivationFunction = ActivationFunction.Tanh, OutputValue = 10.3, SumValue = 1.222 });

         _input.Connect(hidden);

         var output = new NeuronLayer("Output");
         output.AddNode(new Neuron("s1") { ActivationFunction = ActivationFunction.Softmax, OutputValue = 0.567656, SumValue = 0.454 });
         output.AddNode(new Neuron("s2") { ActivationFunction = ActivationFunction.Softmax, OutputValue = 0.176545, SumValue = 0.54 });
         //output.AddNode(new Neuron("s3") { ActivationFunction = ActivationFunction.Softmax, OutputValue = 0.9545, SumValue = 0.133 });
         //output.AddNode(new Neuron("s4") { ActivationFunction = ActivationFunction.Softmax, OutputValue = 0.145, SumValue = 0.88 });

         hidden.Connect(output);

         var aleatorio = new Random(2);

         foreach (var p in hidden.Nodes)
         {
            foreach (var edge in p.Edges)
            {
               int sign = aleatorio.Next(-1, 2);
               edge.Weight = aleatorio.NextDouble() * sign;
            }
         }

         foreach (var p in output.Nodes)
         {
            foreach (var edge in p.Edges)
            {
               int sign = aleatorio.Next(-1, 1);
               edge.Weight = aleatorio.NextDouble() * sign;
            }
         }

         NeuralNetworkVisualizerControl1.InputLayer = _input;

         btnChangeValue.Enabled = btnAddBias.Enabled = btnClear.Enabled = trackZoom.Enabled = cboQuality.Enabled = true;
      }

      private void btnChangeValue_Click(object sender, EventArgs e)
      {
         NeuralNetworkVisualizerControl1.SuspendAutoRedraw();

         var edge = _input.Find<Edge>("Input.bias - Hidden.o1");
         edge.Weight = 0.123;

         var node = _input.Nodes.Single(n => n.Id == "e3");
         node.OutputValue = 1.44444;

         NeuralNetworkVisualizerControl1.ResumeAutoRedraw();
      }

      private void btnAddBias_Click(object sender, EventArgs e)
      {
         AddHiddenBias();
      }

      private void AddHiddenBias()
      {
         NeuralNetworkVisualizerControl1.SuspendAutoRedraw();

         var newbias = new Bias("HiddenBias") { OutputValue = 0.777 };
         _input.Next.Bias = newbias;

         var outputs = _input.Next.Next.Nodes;
         var edges = outputs.SelectMany(o => o.Edges.Where(e => e.Source == newbias));

         double weight = 1.99;
         foreach (var edge in edges)
         {
            edge.Weight = weight;
            weight++;
         }

         NeuralNetworkVisualizerControl1.ResumeAutoRedraw();
      }

      private void trackZoom_Scroll(object sender, EventArgs e)
      {
         NeuralNetworkVisualizerControl1.Zoom = trackZoom.Value / 10f;
      }

      private void btnClear_Click(object sender, EventArgs e)
      {
         NeuralNetworkVisualizerControl1.InputLayer = null;
         btnChangeValue.Enabled = btnAddBias.Enabled = btnClear.Enabled = trackZoom.Enabled = cboQuality.Enabled = false;
      }

      private void cboQuality_SelectedIndexChanged(object sender, EventArgs e)
      {
         NeuralNetworkVisualizerControl1.Preferences.Quality = (RenderQuality)cboQuality.SelectedItem;
         NeuralNetworkVisualizerControl1.Redraw();
      }

      private void chSelectable_CheckedChanged(object sender, EventArgs e)
      {
         NeuralNetworkVisualizerControl1.Preferences.Selectable = chSelectable.Checked;
      }
   }
}
