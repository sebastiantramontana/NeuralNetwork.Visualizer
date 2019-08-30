using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Text
{
   public class OutputFontLabel : FontLabel
   {
      private static readonly StringFormat _outputDefaultFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.Character };

      public static OutputFontLabel Null = new OutputFontLabel(string.Empty, 0, 0, Color.Empty, null);
      public static OutputFontLabel Default = new OutputFontLabel("Tahoma", FontStyle.Regular, 20, Color.Black, _outputDefaultFormat);

      public OutputFontLabel(string family, FontStyle style, int size, Color color, StringFormat format) : base(family, style, size, color, format ?? _outputDefaultFormat)
      {
      }
   }
}
