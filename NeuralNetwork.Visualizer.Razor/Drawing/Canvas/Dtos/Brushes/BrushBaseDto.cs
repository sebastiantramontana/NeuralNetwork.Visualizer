using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes
{
   [JsonConverter(typeof(PolymorphicJsonConverter<BrushBaseDto>))]
   internal abstract class BrushBaseDto
   {
      protected BrushBaseDto(BrushType typeDiscriminator)
      {
         this.TypeDiscriminator = typeDiscriminator;
      }

      public BrushType TypeDiscriminator { get; }
   }
}
