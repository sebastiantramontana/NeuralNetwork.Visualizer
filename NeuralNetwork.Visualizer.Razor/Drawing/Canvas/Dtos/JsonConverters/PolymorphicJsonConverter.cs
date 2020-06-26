using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters
{
   internal class PolymorphicJsonConverter<T> : JsonConverter<T>
   {
      public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         return default;
      }

      public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
      {
         var valueType = value.GetType();

         if (!typeof(T).IsAssignableFrom(valueType))
            return;

         WriteObject(string.Empty, value, writer);
      }

      private void WriteObject(string propertyName, object value, Utf8JsonWriter writer)
      {
         if (string.IsNullOrWhiteSpace(propertyName))
         {
            writer.WriteStartObject();
         }
         else
         {
            writer.WriteStartObject(propertyName);
         }

         var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

         foreach (var prop in properties)
         {
            WriteProperty(value, prop, writer);
         }

         writer.WriteEndObject();
      }

      private void WriteProperty(object value, PropertyInfo property, Utf8JsonWriter writer)
      {
         var propertyName = char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1);
         var propertyValue = property.GetValue(value);

         switch (Type.GetTypeCode(property.PropertyType))
         {
            case TypeCode.Boolean:
               writer.WriteBoolean(propertyName, (bool)propertyValue);
               break;
            case TypeCode.Char:
            case TypeCode.String:
               writer.WriteString(propertyName, propertyValue.ToString());
               break;
            case TypeCode.DateTime:
               writer.WriteString(propertyName, (DateTime)propertyValue);
               break;
            case TypeCode.DBNull:
            case TypeCode.Empty:
               break;
            case TypeCode.Object:
               WriteObject(propertyName, propertyValue, writer);
               break;
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
            case TypeCode.Int32:
            case TypeCode.UInt32:
            case TypeCode.Int64:
            case TypeCode.UInt64:
            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Decimal:
               WriteNumberValue(propertyName, propertyValue, property.PropertyType, writer);
               break;
            default:
               break;
         }
      }

      private void WriteNumberValue(string propertyName, object propertyValue, Type propertyType, Utf8JsonWriter writer)
      {
         if (propertyType.IsEnum)
         {
            writer.WriteString(propertyName, propertyValue.ToString());
         }
         else
         {
            writer.WriteNumber(propertyName, (double)propertyValue);
         }
      }
   }
}
