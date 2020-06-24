using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes
{
   [JsonConverter(typeof(JsonStringEnumConverter))]
   internal enum BrushType
   {
      Solid,
      LinearGradient
   }
}
