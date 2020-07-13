﻿using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts
{
   [JsonConverter(typeof(EnumJsonConverter<TextAligment>))]
   internal enum TextAligment
   {
      Start = 0,
      Center,
      End
   }
}
