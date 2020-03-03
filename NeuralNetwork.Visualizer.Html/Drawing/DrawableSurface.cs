using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Html.Infrastructure;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Drawing
{
   internal class DrawableSurface : IDrawableSurface
   {
      private readonly ICanvasBuilder _canvasBuilder;
      private readonly IJsInterop _jsInterop;
      private readonly string _jsInstancePath;

      internal DrawableSurface(IDrafter drafter, ICanvasBuilder canvasBuilder, IJsInterop jsInterop, string globalInstanceName)
      {
         this.Drafter = drafter;
         _canvasBuilder = canvasBuilder;
         _jsInterop = jsInterop;
         _jsInstancePath = CreateJsInstancePath(globalInstanceName).Result;
      }

      public Size Size => _jsInterop.ExecuteFunction<Size>(_jsInstancePath + ".getSize").Result;
      public Size DrawingSize => _jsInterop.ExecuteFunction<Size>(_jsInstancePath + ".getDrawingSize").Result;
      public IDrafter Drafter { get; }

      public Image GetImage()
      {
         return _jsInterop.ExecuteFunction<Image>(_jsInstancePath + ".getImage").Result;
      }

      public async Task RedrawAsync()
      {
         await this.Drafter.RedrawAsync(_canvasBuilder);
      }

      private async Task<string> CreateJsInstancePath(string globalInstanceName)
      {
         var objName = await _jsInterop.ExecuteFunction<string>("createDrawableSurface", globalInstanceName);
         return $"{ globalInstanceName}.{ objName};";
      }
   }
}
