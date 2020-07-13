﻿using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Razor.Drawing.JsDrawingCallAccumulation;
using System;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Canvas
{
   internal class HtmlCanvas : ICanvas
   {
      private readonly IJsDrawingCallAccumulator _jsDrawingCallAccumulator;

      internal HtmlCanvas(Size size, IJsDrawingCallAccumulator jsDrawingCallAccumulator)
      {
         this.Size = size;
         _jsDrawingCallAccumulator = jsDrawingCallAccumulator;
      }

      public Size Size { get; }

      public void DrawEllipse(Rectangle rect, Pen pen, IBrush brush)
      {
         var radiusX = rect.Size.Width / 2;
         var radiusY = rect.Size.Height / 2;
         var adaptedCenteredX = rect.Position.X + radiusX;
         var adaptedCenteredY = rect.Position.Y + radiusY;
         var penDto = pen?.ToDto(rect);
         var brushDto = brush?.ToDto(rect);

         _jsDrawingCallAccumulator.AddEllipse(adaptedCenteredX, adaptedCenteredY, radiusX, radiusY, penDto, brushDto);
      }

      public void DrawLine(Position p1, Position p2, Pen pen)
      {
         var p1Dto = p1.ToDto();
         var p2Dto = p2.ToDto();
         var penDto = pen?.ToDto(new Rectangle(p1, new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y))));

         _jsDrawingCallAccumulator.AddLine(p1Dto, p2Dto, penDto);
      }

      public void DrawRectangle(Rectangle rect, Pen pen, IBrush brush)
      {
         var rectangleDto = rect.ToDto();
         var penDto = pen?.ToDto(rect);
         var brushDto = brush?.ToDto(rect);

         _jsDrawingCallAccumulator.AddRectangle(rectangleDto, penDto, brushDto);
      }

      public void DrawText(string text, FontLabel font, Rectangle rect)
      {
         DrawText(text, font, rect, 0);
      }

      public void DrawText(string text, FontLabel font, Rectangle rect, float angle)
      {
         var adaptedCenteredX = rect.Position.X + rect.Size.Width / 2;
         var adaptedCenteredY = rect.Position.Y + rect.Size.Height / 2;
         var htmlCanvasRect = new Rectangle(new Position(adaptedCenteredX, adaptedCenteredY), rect.Size);

         var rectangleDto = htmlCanvasRect.ToDto();
         var fontDto = font?.ToDto(rect);

         _jsDrawingCallAccumulator.AddText(text, fontDto, rectangleDto, angle);
      }

      public Position Translate(Position position, ICanvas destination)
      {
         if (object.ReferenceEquals(destination, this) || destination is HtmlCanvas)
            return position;

         var posTranslated = destination.Translate(new Position(0, 0), this);
         position = new Position(position.X - posTranslated.X, position.Y - posTranslated.Y);
         return position;
      }
   }
}
