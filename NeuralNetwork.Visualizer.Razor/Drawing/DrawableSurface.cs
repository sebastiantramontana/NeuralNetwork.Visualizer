using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Razor.Drawing.Dtos;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System;
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
         return await CallDomMethod<ImageDto, Image>("getImage", dto => dto.ToImage());
      }

      public async Task RedrawAsync()
      {
         await this.Drafter.RedrawAsync(_canvasBuilder);
      }

      private async Task<Size> CallDomSizeMethod(string domSizeMethod)
      {
         return await CallDomMethod<SizeDto, Size>(domSizeMethod, dto => dto.ToSize());
      }

      private async Task<TPrimitive> CallDomMethod<TDto, TPrimitive>(string domMethod, Func<TDto, TPrimitive> converter)
      {
         var dto = await _jsInterop.ExecuteOnInstance<TDto>($"DrawableSurface.{domMethod}");
         return converter.Invoke(dto);
      }
   }
}
