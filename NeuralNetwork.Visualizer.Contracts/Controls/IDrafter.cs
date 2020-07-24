using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IDrafter
   {
      Task<bool> RedrawAsync(ICanvasBuilder canvasBuilder, Action drawBlankAction);
      IRegionBuilder RegionBuilder { get; }
   }
}
