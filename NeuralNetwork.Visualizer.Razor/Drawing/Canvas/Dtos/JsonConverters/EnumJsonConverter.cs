using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters
{
   internal class EnumJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
   {
      public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         return default;
      }

      public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
      {
         var enumName = Enum.GetName(value.GetType(), value).ToCamelCase();

         writer.WriteStringValue(enumName);
      }
   }
}
