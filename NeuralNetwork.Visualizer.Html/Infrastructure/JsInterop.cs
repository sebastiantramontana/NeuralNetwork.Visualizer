using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure
{
   internal class JsInterop : IJsInterop
   {
      private readonly IJSRuntime _jsRuntime;
      private readonly string _globalInstanceName;

      internal JsInterop(IJSRuntime jsRuntime, string globalInstanceName)
      {
         _jsRuntime = jsRuntime;
         _globalInstanceName = globalInstanceName;
      }

      public async ValueTask Excute(string code)
      {
         await _jsRuntime.InvokeVoidAsync($"window.eval", code);
      }

      public async ValueTask<TReturn> Excute<TReturn>(string code)
      {
         return await _jsRuntime.InvokeAsync<TReturn>($"window.eval", code);
      }

      public async ValueTask ExecuteInstance(string functionPath, params object[] args)
      {
         await _jsRuntime.InvokeVoidAsync($"window[{_globalInstanceName}].{functionPath}", args);
      }

      public async ValueTask<TReturn> ExecuteInstance<TReturn>(string functionPath, params object[] args)
      {
         return await _jsRuntime.InvokeAsync<TReturn>($"window[{_globalInstanceName}].{functionPath}", args);
      }
   }
}
