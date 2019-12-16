using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Core;

namespace NeuralNetwork.Visualizer.Preferences.Pens
{
   public class Pen
   {
      public static readonly Pen Null = new Pen(SolidBrush.Null, LineStyle.Solid, 0, Cap.None, Cap.None);

      public Pen(IBrush brush, LineStyle lineStyle, int width, Cap startCap, Cap endCap)
      {
         this.Brush = brush;
         this.LineStyle = lineStyle;
         this.Width = width;
         this.StartCap = startCap;
         this.EndCap = endCap;
      }

      public IBrush Brush { get; }
      public LineStyle LineStyle { get; }
      public int Width { get; }
      public Cap StartCap { get; }
      public Cap EndCap { get; }

      public static Pen BasicFromColor(Color color)
      {
         return new Pen(new SolidBrush(color), LineStyle.Solid, 1, Cap.None, Cap.None);
      }
   }
}
