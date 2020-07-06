using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Razor.Infrastructure.Interops;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Controls.ToolTip
{
   internal class ToolTipControl : IToolTip
   {
      private readonly IJsInterop _jsInterop;

      internal ToolTipControl(IJsInterop jsInterop)
      {
         _jsInterop = jsInterop;
      }

      public void Show(string title, string text)
      {
         _jsInterop.ExecuteOnInstance("ToolTip.show");
      }

      public void Close()
      {
         _jsInterop.ExecuteOnInstance("ToolTip.close");
      }
   }
}
