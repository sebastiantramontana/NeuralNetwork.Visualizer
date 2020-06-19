using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos.Brushes
{
   internal abstract class BrushBaseDto
   {
      protected BrushBaseDto(BrushType type)
      {
         this.Type = type;
      }

      public BrushType Type { get; }
   }
}
