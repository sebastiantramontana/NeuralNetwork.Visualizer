using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes
{
   [JsonConverter(typeof(EnumJsonConverter<BrushType>))]
   internal enum BrushType
   {
      Solid = 0,
      LinearGradient
   }
}
