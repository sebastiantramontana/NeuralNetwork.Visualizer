using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;

namespace NeuralNetwork.Visualizer.Razor.Selection
{
   internal class Region : IRegion
   {
      public bool IsVisible(Position position)
      {
         return false; //TODO: UNMOCK
      }
   }
}
