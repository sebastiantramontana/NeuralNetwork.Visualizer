using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters
{
   internal class ArrayJsonConverter : JsonConverter<byte[]>
   {
      public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         return new byte[0];
      }

      public override void Write(Utf8JsonWriter writer, byte[] values, JsonSerializerOptions options)
      {
         writer.WriteStartArray();

         foreach (var val in values)
         {
            writer.WriteNumberValue(val);
         }

         writer.WriteEndArray();
      }
   }
}
