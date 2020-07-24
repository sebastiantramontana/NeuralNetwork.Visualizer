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
      public Size Size => CallDomSizeMethod("getSize");
      public Size DrawingSize => CallDomSizeMethod("getDrawingSize");
      public Image Image => CallDomMethod<ImageDto, Image>("getImage", dto => dto.ToVisualizer());
      public Task RedrawAsync() => this.Drafter.RedrawAsync(_canvasBuilder, ClearCanvas);

      private Size CallDomSizeMethod(string domSizeMethod) => CallDomMethod<SizeDto, Size>(domSizeMethod, dto => dto.ToVisualizer());

      private void ClearCanvas()
      {
         _jsInterop.ExecuteOnInstance("clearCanvas");
      }

      private TPrimitive CallDomMethod<TDto, TPrimitive>(string domMethod, Func<TDto, TPrimitive> converter)
      {
         var dto = _jsInterop.ExecuteOnInstance<TDto>($"DrawableSurface.{domMethod}");
         return converter.Invoke(dto);
      }
   }
}
