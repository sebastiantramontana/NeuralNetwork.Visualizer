using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts
{
   [JsonConverter(typeof(JsonStringEnumConverter))]
   internal enum TextBaseline
   {
      Middle = 0,
      Top,
      Bottom
   }
}
