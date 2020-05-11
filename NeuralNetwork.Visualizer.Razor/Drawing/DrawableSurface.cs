using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Razor.Drawing.Dtos;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
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

      public IDrafter Drafter { get; }

      public async Task<Size> GetSize()
      {
         return await CallDomSizeMethod("getSize");
      }

      public async Task<Size> GetDrawingSize()
      {
         return await CallDomSizeMethod("getDrawingSize");
      }

      public async Task<Image> GetImage()
      {
         return await _jsInterop.ExecuteOnInstance<Image>("DrawableSurface.getImage");
      }

      public async Task RedrawAsync()
      {
         await this.Drafter.RedrawAsync(_canvasBuilder);
      }

      private async Task<Size> CallDomSizeMethod(string domSizeMethod)
      {
         var dto = await _jsInterop.ExecuteOnInstance<SizeDto>($"DrawableSurface.{domSizeMethod}");
         return dto.ToSize();
      }
   }
}
