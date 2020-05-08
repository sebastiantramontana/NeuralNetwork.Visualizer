using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Interops
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

      public async Task ExcuteCode(string code)
      {
         await _jsRuntime.InvokeVoidAsync($"window.eval", code);
      }

      public async Task<TReturn> ExcuteCode<TReturn>(string code)
      {
         return await _jsRuntime.InvokeAsync<TReturn>($"window.eval", code);
      }

      public async Task ExcuteFunction(string functionName, params object[] args)
      {
         await _jsRuntime.InvokeVoidAsync(functionName, args);
      }

      public async Task<TReturn> ExcuteFunction<TReturn>(string functionName, params object[] args)
      {
         return await _jsRuntime.InvokeAsync<TReturn>(functionName, args);
      }

      public async Task ExecuteOnInstance(string functionPath, params object[] args)
      {
         await _jsRuntime.InvokeVoidAsync($"{_globalInstanceName}.{functionPath}", args);
      }

      public async Task<TReturn> ExecuteOnInstance<TReturn>(string functionPath, params object[] args)
      {
         return await _jsRuntime.InvokeAsync<TReturn>($"{_globalInstanceName}.{functionPath}", args);
      }
   }
}
