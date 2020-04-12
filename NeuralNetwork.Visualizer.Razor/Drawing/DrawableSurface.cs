using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Razor.Infrastructure;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal class DrawableSurface : IDrawableSurface
   {
      private readonly ICanvasBuilder _canvasBuilder;
      private readonly IJsInterop _jsInterop;

      internal DrawableSurface(IDrafter drafter, ICanvasBuilder canvasBuilder, IJsInterop jsInterop)
      {
         this.Drafter = drafter;
         _canvasBuilder = canvasBuilder;
         _jsInterop = jsInterop;
      }

      public Size Size => _jsInterop.ExecuteOnInstance<Size>("DrawableSurface.getSize").Result;
      public Size DrawingSize => _jsInterop.ExecuteOnInstance<Size>("DrawableSurface.getDrawingSize").Result;
      public IDrafter Drafter { get; }

      public Image GetImage()
      {
         return _jsInterop.ExecuteOnInstance<Image>("DrawableSurface.getImage").Result;
      }

      public async Task RedrawAsync()
      {
         await this.Drafter.RedrawAsync(_canvasBuilder);
      }
   }
}
