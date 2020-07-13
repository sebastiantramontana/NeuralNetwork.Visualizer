using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts
{
   [JsonConverter(typeof(EnumJsonConverter<TextBaseline>))]
   internal enum TextBaseline
   {
      Middle = 0,
      Top,
      Bottom
   }
}
