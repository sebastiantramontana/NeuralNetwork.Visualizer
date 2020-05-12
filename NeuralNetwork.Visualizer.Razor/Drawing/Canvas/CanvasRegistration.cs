using NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas
{
   internal class CanvasRegistration : IScriptRegistration
   {
      public string FunctionName { get; } = "registerCanvasDomAccess";
   }
}
