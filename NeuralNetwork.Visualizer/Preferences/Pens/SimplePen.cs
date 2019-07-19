using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences.Pens
{
   public class SimplePen : IPen
   {
      private readonly Pen _pen;

      public SimplePen(Pen pen)
      {
         _pen = pen.Clone() as Pen;
      }

      public Pen CreatePen()
      {
         return _pen.Clone() as Pen;
      }

      public void Dispose()
      {
         _pen.Dispose();
      }
   }
}
