using NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts;

namespace NeuralNetwork.Visualizer.Razor.Controls.ToolTip
{
   internal class ToolTipScriptRegistration : IScriptRegistration
   {
      public string FunctionName { get; } = "registerToolTipDomAccess";
   }
}
