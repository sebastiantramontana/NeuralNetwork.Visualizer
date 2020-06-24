using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.JsonConverters;
using System.Text.Json.Serialization;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens
{
   internal class PenDto
   {
      public PenDto(int width, BrushBaseDto brush, byte[] lineDash, LineCap cap)
      {
         this.Width = width;
         this.Brush = brush;
         this.LineDash = lineDash;
         this.Cap = cap;
      }

      public int Width { get; }
      public BrushBaseDto Brush { get; }

      [JsonConverter(typeof(ArrayJsonConverter))]
      public byte[] LineDash { get; }

      public LineCap Cap { get; }
   }
}
