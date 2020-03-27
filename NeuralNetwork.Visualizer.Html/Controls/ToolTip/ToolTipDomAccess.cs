using NeuralNetwork.Visualizer.Html.Infrastructure;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Controls.ToolTip
{
   internal class ToolTipDomAccess : IToolTipDomAccess
   {
      private readonly IJsInterop _jsInterop;
      private readonly string _jsInstancePath;

      internal ToolTipDomAccess(IJsInterop jsInterop, string jsInstancePath)
      {
         _jsInterop = jsInterop;
         _jsInstancePath = jsInstancePath;
      }
      public async ValueTask Show(string title, string text)
      {
         await _jsInterop.ExecuteFunction(_jsInstancePath + ".show", title, text);
      }

      public async ValueTask Close()
      {
         await _jsInterop.ExecuteFunction(_jsInstancePath + ".close");
      }
   }
}
