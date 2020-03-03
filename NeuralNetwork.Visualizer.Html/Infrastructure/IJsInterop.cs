using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure
{
   internal interface IJsInterop
   {
      Task Excute(string code);
      Task<TReturn> Excute<TReturn>(string code);
      Task ExecuteFunction(string functionName, params object[] args);
      Task<TReturn> ExecuteFunction<TReturn>(string functionName, params object[] args);
   }
}
