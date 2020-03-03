using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure
{
   internal class JsInterop : IJsInterop
   {
      private readonly IJSRuntime _jsRuntime;

      internal JsInterop(IJSRuntime jsRuntime)
      {
         _jsRuntime = jsRuntime;
      }

      public async Task Excute(string code)
      {
         await ExecuteFunction($"window.eval('{code})'");
      }

      public async Task<TReturn> Excute<TReturn>(string code)
      {
         return await ExecuteFunction<TReturn>($"window.eval('{code})'");
      }

      public async Task ExecuteFunction(string functionName, params object[] args)
      {
         await _jsRuntime.InvokeVoidAsync(functionName, args);
      }

      public async Task<TReturn> ExecuteFunction<TReturn>(string functionName, params object[] args)
      {
         return await _jsRuntime.InvokeAsync<TReturn>(functionName, args);
      }
   }
}
