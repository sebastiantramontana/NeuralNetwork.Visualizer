using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;

namespace NeuralNetwork.Visualizer.Calcs
{
   public class NeuronSizesPreCalc : NodeSizesPreCalc
   {
      public int ActivationFunctionVerticalMargins { get; } = 7;

      private Size _sumSize = null;
      public Size SumSize
      {
         get
         {
            if (_sumSize is null)
            {
               var valuesHeight = this.Div3 / 2;
               var valuesWidth = valuesHeight * 5.23; //buena proporción
               _sumSize = new Size((int)valuesWidth, (int)valuesHeight);
            }

            return _sumSize;
         }
      }

      private Size _activationFunctionSize = null;
      public Size ActivationFunctionSize
      {
         get
         {
            if (_activationFunctionSize is null)
            {
               var div_3 = (int)this.Div3;
               _activationFunctionSize = new Size(div_3, div_3 - this.ActivationFunctionVerticalMargins * 2);
            }

            return _activationFunctionSize;
         }
      }


      private double? _ellipseHeightDiv2 = null;
      public int GetInputPositionY(int fromY)
      {
         if (!_ellipseHeightDiv2.HasValue)
         {
            _ellipseHeightDiv2 = this.EllipseRectangle.Size.Height / 2;
         }

         return (int)(fromY + _ellipseHeightDiv2.Value);
      }

      private double? _div3 = null;
      private double Div3
      {
         get
         {
            if (!_div3.HasValue)
            {
               _div3 = this.EllipseRectangle.Size.Height / 3;
            }

            return _div3.Value;
         }
      }

      public Position GetActivationFunctionPosition(Rectangle origin)
      {
         return new Position((int)(origin.Position.X + this.Div3), (int)(origin.Position.Y + this.Div3 + this.ActivationFunctionVerticalMargins));
      }

      private double? _ouputPositionYOffset = null;
      public int GetOutputPositionY(int fromY)
      {
         if (!_ouputPositionYOffset.HasValue)
         {
            _ouputPositionYOffset = this.Div3 * 2;
         }

         return (int)(fromY + _ouputPositionYOffset.Value);
      }
   }
}
