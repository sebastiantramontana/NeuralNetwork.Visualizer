﻿namespace NeuralNetwork.Visualizer.Razor.Drawing.Dtos
{
   internal class RectangleDto
   {
      public RectangleDto(PositionDto position, SizeDto size)
      {
         this.Position = position;
         this.Size = size;
      }

      public PositionDto Position { get; }
      public SizeDto Size { get; }
   }
}
