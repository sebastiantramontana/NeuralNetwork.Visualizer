using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos.Brushes
{
   internal class SolidBrushDto : BrushBaseDto
   {
      internal SolidBrushDto(Color color) : base(BrushType.Solid)
      {
         this.Color = color;
      }

      public Color Color { get; }
   }
}
