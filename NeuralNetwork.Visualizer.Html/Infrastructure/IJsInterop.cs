using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure
{
   internal interface IJsInterop
   {
      ValueTask Excute(string code);
      ValueTask<TReturn> Excute<TReturn>(string code);
      ValueTask ExecuteFunction(string functionName, params object[] args);
      ValueTask<TReturn> ExecuteFunction<TReturn>(string functionName, params object[] args);
   }
}
