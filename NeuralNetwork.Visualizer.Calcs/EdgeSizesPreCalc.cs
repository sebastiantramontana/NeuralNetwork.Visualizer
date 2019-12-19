namespace NeuralNetwork.Visualizer.Calcs
{
   public class EdgeSizesPreCalc
   {
      private int _xPositionFromPrevious = -1;
      private EdgeSizesPreCalcValues previousValues;

      public EdgeSizesPreCalcValues GetValues(int xPositionFrom, int xPositionTo)
      {
         if (xPositionFrom == _xPositionFromPrevious)
            return previousValues;

         previousValues = PreCalc(xPositionFrom, xPositionTo);
         _xPositionFromPrevious = xPositionFrom;

         return previousValues;
      }

      private EdgeSizesPreCalcValues PreCalc(int xPositionFrom, int xPositionTo)
      {
         var totalWidth = xPositionTo - xPositionFrom;
         var textWidth = (int)(totalWidth / 4.0);

         double xOffsetFar = totalWidth - totalWidth / 3;
         var widthPortionFar = xOffsetFar / totalWidth;
         var farX = xPositionTo - totalWidth / 3;

         double xOffsetNear = totalWidth / 4.0;
         var widthPortionNear = xOffsetNear / totalWidth;
         var nearX = (int)(xPositionFrom + xOffsetNear);

         return new EdgeSizesPreCalcValues(totalWidth, widthPortionNear, widthPortionFar, nearX, farX, textWidth);
      }

      public class EdgeSizesPreCalcValues
      {
         internal EdgeSizesPreCalcValues(int totalWidth, double widthPortionNear, double widthPortionFar, int nearX, int farX, int textWidth)
         {
            this.TotalWidth = totalWidth;
            this.WidthPortionNear = widthPortionNear;
            this.WidthPortionFar = widthPortionFar;
            this.NearX = nearX;
            this.FarX = farX;
            this.TextWidth = textWidth;
         }

         public int TotalWidth { get; private set; }
         public double WidthPortionNear { get; private set; }
         public double WidthPortionFar { get; private set; }
         public int NearX { get; private set; }
         public int FarX { get; private set; }
         public int TextWidth { get; private set; }
      }
   }
}
