using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation
{
   [JsonConverter(typeof(JsonStringEnumConverter))]
   internal enum JsDrawingMethod
   {
      Unknown,
      Ellipse,
      Rectangle,
      Line,
      Text
   }
}
