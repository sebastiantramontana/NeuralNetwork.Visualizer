namespace NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation
{
   internal class JsDrawingCall
   {
      internal JsDrawingCall(JsDrawingMethod jsDrawingMethod, object[] args)
      {
         this.JsDrawingMethod = jsDrawingMethod;
         this.Args = args;
      }

      public JsDrawingMethod JsDrawingMethod { get; }
      public object[] Args { get; }
   }
}
