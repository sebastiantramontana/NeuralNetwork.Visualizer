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

      public Task ExcuteCode(string code)
      {
         return _jsRuntime.InvokeVoidAsync($"window.eval", code).AsTask();
      }

      public Task<TReturn> ExcuteCode<TReturn>(string code)
      {
         return _jsRuntime.InvokeAsync<TReturn>($"window.eval", code).AsTask();
      }

      public Task ExcuteFunction(string functionName, params object[] args)
      {
         return _jsRuntime.InvokeVoidAsync(functionName, args).AsTask();
      }

      public Task<TReturn> ExcuteFunction<TReturn>(string functionName, params object[] args)
      {
         return _jsRuntime.InvokeAsync<TReturn>(functionName, args).AsTask();
      }

      public Task ExecuteOnInstance(string functionPath, params object[] args)
      {
         return _jsRuntime.InvokeVoidAsync($"{_globalInstanceName}.{functionPath}", args).AsTask();
      }

      public Task<TReturn> ExecuteOnInstance<TReturn>(string functionPath, params object[] args)
      {
         return _jsRuntime.InvokeAsync<TReturn>($"{_globalInstanceName}.{functionPath}", args).AsTask();
      }
   }
}
