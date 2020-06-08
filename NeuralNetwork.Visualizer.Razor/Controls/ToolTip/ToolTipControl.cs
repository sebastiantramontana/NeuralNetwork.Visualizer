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

      public Task Show(string title, string text)
      {
         return _jsInterop.ExecuteOnInstance("ToolTip.show");
      }

      public Task Close()
      {
         return _jsInterop.ExecuteOnInstance("ToolTip.close");
      }
   }
}
