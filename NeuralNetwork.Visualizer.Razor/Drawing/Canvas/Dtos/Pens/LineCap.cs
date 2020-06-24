using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens
{
   [JsonConverter(typeof(JsonStringEnumConverter))]
   internal enum LineCap
   {
      Butt,
      Round,
      Square
   }
}
