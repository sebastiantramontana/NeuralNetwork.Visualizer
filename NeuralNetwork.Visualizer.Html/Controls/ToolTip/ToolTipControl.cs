using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Html.Infrastructure;

namespace NeuralNetwork.Visualizer.Html.Controls.ToolTip
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
         await _jsInterop.ExecuteInstance("ToolTip.show");
      }

      public async void Close()
      {
         await _jsInterop.ExecuteInstance("ToolTip.close");
      }
   }
}
