using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using System;

namespace NeuralNetwork.Visualizer.Calcs
{
   public class LayerSizesPreCalc
   {
      private const int MAX_NODE_SIDE = 40;

      internal LayerSizesPreCalc(Size currentCanvasSize, int layersCount, int maxNodes, IPreference preferences)
      {
         var initialLayerWidth = currentCanvasSize.Width / layersCount;
         var currentHeight = currentCanvasSize.Height;

         var doubleNodeMargin = preferences.NodeMargins * 2;
         float maxBorder;

         var inputPen = preferences.Inputs.Border;
         var neuronPen = preferences.Neurons.Border;
         maxBorder = Math.Max(inputPen.Width, neuronPen.Width);

         var initialTotalNodesHeight = currentHeight - (preferences.Layers.Title.Height + doubleNodeMargin);
         var initialNodeHeight = initialTotalNodesHeight / maxNodes;

         this.NodeHeight = Math.Max(initialNodeHeight, MAX_NODE_SIDE);

         if (this.NodeHeight <= initialNodeHeight)
         {
            SetNormalHeight(currentHeight, initialTotalNodesHeight);
         }
         else
         {
            EnlargeHeight(this.NodeHeight, maxNodes, preferences.Layers.Title.Height, doubleNodeMargin);
         }

         this.NodeEllipseHeight = this.NodeHeight - preferences.NodeMargins / maxNodes - (int)(maxBorder * 2);
         this.StartingY = preferences.Layers.Title.Height + preferences.NodeMargins;
         this.NodeWidth = Math.Max(initialLayerWidth - doubleNodeMargin, MAX_NODE_SIDE);

         this.Width = initialLayerWidth;
      }

      internal int Width { get; }
      internal int Height { get; private set; }

      internal int NodeWidth { get; }
      internal int NodeHeight { get; }
      internal int NodeEllipseHeight { get; }
      internal int TotalNodesHeight { get; private set; }
      internal int StartingY { get; }

      private void SetNormalHeight(int currentHeight, int initialTotalNodesHeight)
      {
         this.TotalNodesHeight = initialTotalNodesHeight;
         this.Height = currentHeight;
      }

      private void EnlargeHeight(int nodeHeight, int maxNodes, int titleHeight, int margins)
      {
         this.TotalNodesHeight = nodeHeight * (maxNodes + 1) - (titleHeight + margins);
         this.Height = this.TotalNodesHeight + titleHeight + margins;
      }
   }
}
