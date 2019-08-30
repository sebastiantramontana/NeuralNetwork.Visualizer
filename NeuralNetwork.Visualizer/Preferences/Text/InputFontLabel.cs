using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Text
{
   public class InputFontLabel : FontLabel
   {
      private static readonly StringFormat _inputDefaultFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.Character };

      public static InputFontLabel Null = new InputFontLabel(string.Empty, 0, 0, Color.Empty, null);
      public static InputFontLabel Default = new InputFontLabel("Tahoma", FontStyle.Regular, 20, Color.Black, _inputDefaultFormat);

      public InputFontLabel(string family, FontStyle style, int size, Color color, StringFormat format) : base(family, style, size, color, format ?? _inputDefaultFormat)
      {
      }
   }
}
