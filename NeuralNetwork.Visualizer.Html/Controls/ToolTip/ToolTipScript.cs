using NeuralNetwork.Visualizer.Html.Infrastructure;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Controls.ToolTip
{
   internal class ToolTipScript : IScript<IToolTipDomAccess>
   {
      private readonly IJsInterop _jsInterop;
      private readonly string _globalInstanceName;

      public ToolTipScript(IJsInterop jsInterop, string globalInstanceName)
      {
         _jsInterop = jsInterop;
         _globalInstanceName = globalInstanceName;
      }

      public ValueTask<IToolTipDomAccess> CreateDomAccess()
      {
         return new ToolTipDomAccess(_jsInterop, CreateJsInstancePath(_globalInstanceName));
      }

      private async ValueTask<string> CreateJsInstancePath(string globalInstanceName)
      {
         var objName = await _jsInterop.ExecuteFunction<string>("createToolTip", globalInstanceName);
         return $"{ globalInstanceName}.{ objName};";
      }
   }
}
