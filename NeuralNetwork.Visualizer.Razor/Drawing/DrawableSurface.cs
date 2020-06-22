using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos;
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

      public Task<Size> GetSize()
      {
         return CallDomSizeMethod("getSize");
      }

      public Task<Size> GetDrawingSize()
      {
         return CallDomSizeMethod("getDrawingSize");
      }

      public Task<Image> GetImage()
      {
         return CallDomMethod<ImageDto, Image>("getImage", dto => dto.ToVisualizer());
      }

      public Task RedrawAsync()
      {
         return this.Drafter.RedrawAsync(_canvasBuilder);
      }

      private Task<Size> CallDomSizeMethod(string domSizeMethod)
      {
         return CallDomMethod<SizeDto, Size>(domSizeMethod, dto => dto.ToVisualizer());
      }

      private async Task<TPrimitive> CallDomMethod<TDto, TPrimitive>(string domMethod, Func<TDto, TPrimitive> converter)
      {
         var dto = await _jsInterop.ExecuteOnInstance<TDto>($"DrawableSurface.{domMethod}").ConfigureAwait(false);
         return converter.Invoke(dto);
      }
   }
}
