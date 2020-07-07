using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Brushes;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Fonts;
using NeuralNetwork.Visualizer.Razor.Drawing.Canvas.Dtos.Pens;

namespace NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation
{
   internal interface IJsDrawingCallAccumulator
   {
      void AddEllipse(int x, int y, int radiusX, int radiusY, PenDto penDto, BrushBaseDto brushDto);
      void AddRectangle(RectangleDto rectangleDto, PenDto penDto, BrushBaseDto brushDto);
      void AddLine(PositionDto p1Dto, PositionDto p2Dto, PenDto penDto);
      void AddText(string text, FontDto fontDto, RectangleDto rectangleDto, float angle);

      JsDrawingCall[] GetCalls();
   }
}
