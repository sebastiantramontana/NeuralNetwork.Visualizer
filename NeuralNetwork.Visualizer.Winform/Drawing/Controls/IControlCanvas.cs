using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   internal interface IControlCanvas
   {
      Size Size { get; set; }
      Gdi.Image Image { get; set; }
      NeuralNetworkVisualizerControl Control { get; }
      bool IsReady { get; }

      void SetBlank();
      (Gdi.Graphics Graph, Gdi.Image Image, LayerSizesPreCalc LayerSizes) GetGraphics();
   }
}
