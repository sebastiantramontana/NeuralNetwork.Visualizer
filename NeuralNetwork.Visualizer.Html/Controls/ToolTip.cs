using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Html.Infrastructure;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Controls
{
   internal class ToolTip : IToolTip
   {
      private readonly IJsInterop _jsInterop;
      private readonly string _globalInstanceName;
      private readonly string jsInstancePath = null;

      internal ToolTip(IJsInterop jsInterop, string globalInstanceName)
      {
         _jsInterop = jsInterop;
         _globalInstanceName = globalInstanceName;
         jsInstancePath = CreateJsInstancePath(_globalInstanceName).Result;
      }

      public async void Show(string title, string text)
      {
         await _jsInterop.ExecuteFunction(jsInstancePath + ".show", title, text);
      }

      public async void Close()
      {
         await _jsInterop.ExecuteFunction(jsInstancePath + ".close");
      }

      private async Task<string> CreateJsInstancePath(string globalInstanceName)
      {
         var objName = await _jsInterop.ExecuteFunction<string>("createToolTip", globalInstanceName);
         return $"{ globalInstanceName}.{ objName};";
      }
   }
}
