using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure
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

      public async ValueTask ExcuteCode(string code)
      {
         await _jsRuntime.InvokeVoidAsync($"window.eval", code);
      }

      public async ValueTask<TReturn> ExcuteCode<TReturn>(string code)
      {
         return await _jsRuntime.InvokeAsync<TReturn>($"window.eval", code);
      }

      public async ValueTask ExcuteFunction(string functionName, params object[] args)
      {
         await _jsRuntime.InvokeVoidAsync(functionName, args);
      }

      public async ValueTask<TReturn> ExcuteFunction<TReturn>(string functionName, params object[] args)
      {
         return await _jsRuntime.InvokeAsync<TReturn>(functionName, args);
      }

      public async ValueTask ExecuteOnInstance(string functionPath, params object[] args)
      {
         await _jsRuntime.InvokeVoidAsync($"window[{_globalInstanceName}].{functionPath}", args);
      }

      public async ValueTask<TReturn> ExecuteOnInstance<TReturn>(string functionPath, params object[] args)
      {
         return await _jsRuntime.InvokeAsync<TReturn>($"window[{_globalInstanceName}].{functionPath}", args);
      }
   }
}
