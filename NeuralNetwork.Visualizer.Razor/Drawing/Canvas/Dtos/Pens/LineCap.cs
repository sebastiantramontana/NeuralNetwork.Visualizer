using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens
{
   [JsonConverter(typeof(EnumJsonConverter<LineCap>))]
   internal enum LineCap
   {
      Butt,
      Round,
      Square
   }
}
