using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Razor.Infrastructure;

namespace NeuralNetwork.Visualizer.Razor.Controls.ToolTip
{
   internal class ToolTipControl : IToolTip
   {
      private readonly IJsInterop _jsInterop;

      internal ToolTipControl(IJsInterop jsInterop)
      {
         _jsInterop = jsInterop;
      }

      public async void Show(string title, string text)
      {
         await _jsInterop.ExecuteOnInstance("ToolTip.show");
      }

      public async void Close()
      {
         await _jsInterop.ExecuteOnInstance("ToolTip.close");
      }
   }
}
