using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation
{
   [JsonConverter(typeof(EnumJsonConverter<JsDrawingMethod>))]
   internal enum JsDrawingMethod
   {
      Unknown = 0,
      Ellipse,
      Rectangle,
      Line,
      Text
   }
}
