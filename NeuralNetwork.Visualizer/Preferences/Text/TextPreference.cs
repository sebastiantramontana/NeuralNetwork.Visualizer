using NeuralNetwork.Visualizer.Drawing;
using NeuralNetwork.Visualizer.Preferences.Brushes;
using System;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Text
{
   public class TextPreference : IDisposable
   {
      public static TextPreference Null
      {
         get
         {
            return new TextPreference
            {
               Brush = null,
               FontFamily = null,
               Format = null
            };
         }
      }

      private readonly StringFormat defualtFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.Character };

      private string _fontFamily;
      public string FontFamily
      {
         get => _fontFamily ?? (_fontFamily = "Tahoma");
         set => _fontFamily = value;
      }

      public FontStyle FontStyle { get; set; } = FontStyle.Regular;

      private IBrush _brush = new SolidBrushPreference(Color.Black);
      public IBrush Brush
      {
         get => _brush ?? (_brush = new SolidBrushPreference(Color.Transparent));
         set { _brush = value; }
      }

      private StringFormat _format;
      public StringFormat Format
      {
         get => _format?.Clone() as StringFormat ?? (_format = defualtFormat.Clone() as StringFormat);
         set => _format = value;
      }

      public TextPreference Clone()
      {
         return new TextPreference
         {
            FontFamily = this.FontFamily,
            FontStyle = this.FontStyle,
            Brush = this.Brush,
            Format = this.Format
         };
      }

      internal FontInfo CreateFontInfo()
      {
         return new FontInfo(this.FontFamily, this.FontStyle);
      }

      public void Dispose()
      {
         Destroy.Disposable(ref _format);
      }
   }
}
