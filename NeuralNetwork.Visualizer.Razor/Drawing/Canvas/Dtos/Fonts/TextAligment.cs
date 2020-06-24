using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts
{
   [JsonConverter(typeof(JsonStringEnumConverter))]
   internal enum TextAligment
   {
      Start,
      Center,
      End
   }
}
