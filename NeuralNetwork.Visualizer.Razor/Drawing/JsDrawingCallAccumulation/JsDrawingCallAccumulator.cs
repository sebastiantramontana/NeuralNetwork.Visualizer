using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation
{
   internal class JsDrawingCallAccumulator : IJsDrawingCallAccumulator
   {
      private readonly ICollection<JsDrawingCall> _calls;
      internal JsDrawingCallAccumulator()
      {
         _calls = new List<JsDrawingCall>();
      }

      public void AddEllipse(int x, int y, int radiusX, int radiusY, PenDto penDto, BrushBaseDto brushDto)
      {
         _calls.Add(new JsDrawingCall(JsDrawingMethod.Ellipse, new object[] { x, y, radiusX, radiusY, penDto, brushDto }));
      }

      public void AddLine(PositionDto p1Dto, PositionDto p2Dto, PenDto penDto)
      {
         _calls.Add(new JsDrawingCall(JsDrawingMethod.Line, new object[] { p1Dto, p2Dto, penDto }));
      }

      public void AddRectangle(RectangleDto rectangleDto, PenDto penDto, BrushBaseDto brushDto)
      {
         _calls.Add(new JsDrawingCall(JsDrawingMethod.Rectangle, new object[] { rectangleDto, penDto, brushDto }));
      }

      public void AddText(string text, FontDto fontDto, RectangleDto rectangleDto, float angle)
      {
         _calls.Add(new JsDrawingCall(JsDrawingMethod.Text, new object[] { text, fontDto, rectangleDto, angle }));
      }

      public JsDrawingCall[] GetCalls()
      {
         return _calls.ToArray();
      }
   }
}
