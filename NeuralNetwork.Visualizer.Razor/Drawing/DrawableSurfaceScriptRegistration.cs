using NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts;

namespace NeuralNetwork.Visualizer.Razor.Drawing
{
   internal class DrawableSurfaceScriptRegistration : IScriptRegistration
   {
      public string FunctionName { get; } = "registerDrawableSurfaceDomAccess";
   }
}
