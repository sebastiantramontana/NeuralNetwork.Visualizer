using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Interops
{
   internal class JsInterop : IJsInterop
   {
      private readonly WebAssemblyJSRuntime _jsRuntime;
      private readonly string _globalInstanceName;

      internal JsInterop(IJSRuntime jsRuntime, string globalInstanceName)
      {
         _jsRuntime = jsRuntime as WebAssemblyJSRuntime;
         _globalInstanceName = globalInstanceName;
      }

      public void ExcuteCode(string code)
      {
         _jsRuntime.InvokeVoid($"window.eval", code);
      }

      public TReturn ExcuteCode<TReturn>(string code)
      {
         return _jsRuntime.Invoke<TReturn>($"window.eval", code);
      }

      public void ExcuteFunction(string functionName, params object[] args)
      {
         _jsRuntime.InvokeVoid(functionName, args);
      }

      public TReturn ExcuteFunction<TReturn>(string functionName, params object[] args)
      {
         return _jsRuntime.Invoke<TReturn>(functionName, args);
      }

      public Task ExcuteFunctionAsync(string functionName, params object[] args)
      {
         return _jsRuntime.InvokeVoidAsync(functionName, args).AsTask();
      }

      public void ExecuteOnInstance(string functionPath, params object[] args)
      {
         _jsRuntime.InvokeVoid($"{_globalInstanceName}.{functionPath}", args);
      }

      public TReturn ExecuteOnInstance<TReturn>(string functionPath, params object[] args)
      {
         return _jsRuntime.Invoke<TReturn>($"{_globalInstanceName}.{functionPath}", args);
      }
   }
}
