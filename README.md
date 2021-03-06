# NeuralNetwork.Visualizer
Easy neural network visualizer winform control for .Net

This is the new version of [NeuralNetworkVisualizer](https://github.com/sebastiantramontana/NeuralNetworkVisualizer)

## Screenshots
### Normal without layers titles
![Normal](https://github.com/sebastiantramontana/NeuralNetwork.Visualizer/raw/master/docs/Normal.PNG)
### Normal resized to small
![Little Size](https://github.com/sebastiantramontana/NeuralNetwork.Visualizer/raw/master/docs/NormalLittle.PNG)
### With layers titles
![Layers Titles](https://github.com/sebastiantramontana/NeuralNetwork.Visualizer/raw/master/docs/NormalWithTitles.PNG)
### Several nodes
![Several Nodes](https://github.com/sebastiantramontana/NeuralNetwork.Visualizer/raw/master/docs/SeveralNodes.PNG)
### Zoomed in
![Zoomed](https://github.com/sebastiantramontana/NeuralNetwork.Visualizer/raw/master/docs/SeveralNodesZoomed.png)
### Elements selection
In the following screenshot: Input nodes (dark green), edges connectors (orange), neurons (dark blue) and the output layer (gray background and orange borders) were selected.
![Elements Selection](https://github.com/sebastiantramontana/NeuralNetwork.Visualizer/raw/master/docs/NormalSelectedElements.png)
### Tooltips
![Tooltip text](https://github.com/sebastiantramontana/NeuralNetwork.Visualizer/raw/master/docs/NormalTooltipText.png)

## Example

```C#
	    /**************** Using... **********************/
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

            /******** Configure Some Preferences: ********/
            
        //Drawing behavior
	NeuralNetworkVisualizerControl1.Preferences.AutoRedrawOnChanges = true;
	NeuralNetworkVisualizerControl1.Preferences.Quality = RenderQuality.High; //Low, Medium, High. Medium is default
            
            //Font, Colors, etc.
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

	//To remove layer titles
	//NeuralNetworkVisualizerControl1.Preferences.Layers = null;

	    /***** Some Functionalities *****/

	NeuralNetworkVisualizerControl1.RedrawAsync(); //Redraw() was removed
	
	//Adjust zoom
	NeuralNetworkVisualizerControl1.Zoom = 2.0f; //1.0 is 'normal' and default, fit the whole drawing to control size

	//Get the current rendered NN to save to disk or whatever
	Image img = NeuralNetworkVisualizerControl1.Image.ToGdi();

            /*************** Set the NN Model *****************/

            var _input = new InputLayer("Input")
            {
                Bias = new Bias("bias") { OutputValue = 1.234 }
            };

            _input.AddNode(new Input("e2") { OutputValue = 0.455 });
            _input.AddNode(new Input("e3") { OutputValue = 0.78967656 });
            _input.AddNode(new Input("e4") { OutputValue = 0.876545 });

            var hidden = new NeuronLayer("Hidden");

            hidden.AddNode(new Neuron("o1") { ActivationFunction = ActivationFunction.LeakyRelu, OutputValue = 2.364, SumValue = 2.364 });
            hidden.AddNode(new Neuron("o2") { ActivationFunction = ActivationFunction.Tanh, OutputValue = 0.552, SumValue = 55.44 });
            hidden.AddNode(new Neuron("o3") { ActivationFunction = ActivationFunction.Sigmoid, OutputValue = 0.876545, SumValue = 11.22 });

            _input.Connect(hidden); //Connect(...) method creates nodes connections

            var output = new NeuronLayer("Output");
            output.AddNode(new Neuron("s1") { ActivationFunction = ActivationFunction.BinaryStep, OutputValue = 0.78967656, SumValue = 0.5544 });
            output.AddNode(new Neuron("s2") { ActivationFunction = ActivationFunction.Softmax, OutputValue = 0.876545, SumValue = 0.5644 });

            hidden.Connect(output);

            var aleatorio = new Random(31);

            foreach (var p in hidden.Nodes)
            {
                foreach (var edge in p.Edges)
                {
                    edge.Weight = aleatorio.NextDouble();
                }
            }

            foreach (var p in output.Nodes)
            {
                foreach (var edge in p.Edges)
                {
                    edge.Weight = aleatorio.NextDouble();
                }
            }

            NeuralNetworkVisualizerControl1.InputLayer = _input; //Automatic rendering
            //NeuralNetworkVisualizerControl1.InputLayer = null; //Leave blank when needed
            
            /*************** Make NN Elements Selectable *****************/
            //The selectable elements are: Layers, Nodes (all types) and Edge connectors.
            // Do a single click for single selection.
            // Press **SHIFT** key when click for multiple one.
            // Press **CTRL** key when click to unselect an element.
                        
            NeuralNetworkVisualizerControl1.Preferences.Selectable = false; //Now, default is true
            
            //Each selectable element has its own typed-safe "Select" event
            NeuralNetworkVisualizerControl1.SelectBias += NeuralNetworkVisualizerControl1_SelectBias;
            NeuralNetworkVisualizerControl1.SelectEdge += NeuralNetworkVisualizerControl1_SelectEdge;
            NeuralNetworkVisualizerControl1.SelectInput += NeuralNetworkVisualizerControl1_SelectInput;
            NeuralNetworkVisualizerControl1.SelectInputLayer += NeuralNetworkVisualizerControl1_SelectInputLayer;
            NeuralNetworkVisualizerControl1.SelectNeuron += NeuralNetworkVisualizerControl1_SelectNeuron;
            NeuralNetworkVisualizerControl1.SelectNeuronLayer += NeuralNetworkVisualizerControl1_SelectNeuronLayer;
            
            private void NeuralNetworkVisualizerControl1_SelectNeuronLayer(object sender, SelectionEventArgs<NeuronLayer> e)
	    {
	        //...
	    }

	    private void NeuralNetworkVisualizerControl1_SelectNeuron(object sender, SelectionEventArgs<Neuron> e)
	    {
		//...
	    }

            private void NeuralNetworkVisualizerControl1_SelectInputLayer(object sender, SelectionEventArgs<InputLayer> e)
            {
                //...
            }

            private void NeuralNetworkVisualizerControl1_SelectInput(object sender, SelectionEventArgs<Input> e)
            {
                //...
            }

            private void NeuralNetworkVisualizerControl1_SelectEdge(object sender, SelectionEventArgs<Edge> e)
            {
                //...
            }

            private void NeuralNetworkVisualizerControl1_SelectBias(object sender, SelectionEventArgs<Bias> e)
            {
                //...
            }
	    
	    private async void AddHiddenBias()
	    {
		 NeuralNetworkVisualizerControl1.SuspendAutoRedraw(); //Suspend temporarily the auto redraw mode when will there are many changes on model to avoid redraw overhead!
		 
		 //make changes in the model...
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

		 await NeuralNetworkVisualizerControl1.ResumeAutoRedraw(); //resume auto redraw for model changes take effect
	    }
            
            
