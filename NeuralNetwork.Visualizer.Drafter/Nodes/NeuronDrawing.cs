using NeuralNetwork.Model;
using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Brushes;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Pens;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Text;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal class NeuronDrawing : NodeBaseDrawing<Neuron>
   {
      private readonly IDictionary<NodeBase, INodeDrawing> _previousNodes;
      private readonly ICanvas _edgesCanvas;
      private readonly IPreference _preferences;
      private readonly NeuronSizesPreCalc _cache;
      private readonly EdgeSizesPreCalc _edgesCache;
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly IRegionBuilder _regionBuilder;

      internal NeuronDrawing(Neuron element, IDictionary<NodeBase, INodeDrawing> previousNodes, ICanvas edgesCanvas, IPreference preferences, NeuronSizesPreCalc cache, EdgeSizesPreCalc edgesCache, IElementSelectionChecker selectionChecker, ISelectableElementRegister selectableElementRegister, IRegionBuilder regionBuilder) : base(element, preferences.Neurons, cache, selectableElementRegister, selectionChecker, regionBuilder)
      {
         _previousNodes = previousNodes;
         _edgesCanvas = edgesCanvas;
         _preferences = preferences;
         _cache = cache;
         _edgesCache = edgesCache;
         _selectionChecker = selectionChecker;
         _selectableElementRegister = selectableElementRegister;
         _regionBuilder = regionBuilder;
      }

      public override void Draw(ICanvas canvas)
      {
         base.Draw(canvas);
         DrawLabel(canvas);
      }

      protected override void DrawContent(ICanvas canvas, Rectangle rect)
      {
         var sizesPositions = GetSizePositions(rect);

         DrawSumValue(sizesPositions.SumRectangle, canvas);
         DrawActivationFunction(sizesPositions.ActivationFunctionRectangle, canvas);
         DrawOutputValue(sizesPositions.OutputRectangle, canvas);
         DrawEdges(sizesPositions.InputPosition, canvas, sizesPositions.OutputRectangle.Size.Height);
      }

      private void DrawSumValue(Rectangle rectangle, ICanvas canvas)
      {
         if (!this.Element.SumValue.HasValue)
            return;

         var sumFont = _preferences.Neurons.SumValueFormatter.GetFormat(this.Element.SumValue.Value);
         canvas.DrawText('\u2211' + " " + Math.Round(this.Element.SumValue.Value, _preferences.Neurons.RoundingDigits).ToString(), sumFont, rectangle);
      }

      private void DrawOutputValue(Rectangle rectangle, ICanvas canvas)
      {
         if (!this.Element.OutputValue.HasValue)
            return;

         var outputFont = _preferences.Neurons.OutputValueFormatter.GetFormat(this.Element.OutputValue.Value);
         canvas.DrawText(Math.Round(this.Element.OutputValue.Value, _preferences.Neurons.RoundingDigits).ToString(), outputFont, rectangle);
      }

      private void DrawLabel(ICanvas canvas)
      {
         if (this.Element.Layer.Next != null || _preferences.OutputFontLabel == FontLabel.Null || _cache.EllipseRectangle is null)
            return;

         var fontLabel = _preferences.OutputFontLabel;

         if (_cache.OutputLabelRectangle is null)
         {
            var rect = _cache.EllipseRectangle;
            var labelWidth = canvas.Size.Width - (rect.Position.X + rect.Size.Width + _preferences.NodeMargins);
            var labelHeight = rect.Size.Height / 3;

            _cache.OutputLabelRectangle = new Rectangle(new Position(rect.Position.X + rect.Size.Width + _preferences.NodeMargins, (rect.Size.Height - labelHeight) / 2), new Size(labelWidth, labelHeight));
         }

         var label = this.Element.Id;

         canvas.DrawText(label, fontLabel, _cache.OutputLabelRectangle);
      }

      private void DrawEdges(Position inputPosition, ICanvas canvas, int textEdgeHeight)
      {
         foreach (var edge in this.Element.Edges)
         {
            var previousNode = _previousNodes[edge.Source];
            var outputPositionTrans = previousNode.Canvas.Translate(previousNode.EdgeStartPosition, _edgesCanvas);
            var inputPositionTrans = canvas.Translate(inputPosition, _edgesCanvas);

            var edgeDrawing = new EdgeDrawing(edge, _preferences.Edges, outputPositionTrans, inputPositionTrans, textEdgeHeight, _edgesCache, _selectableElementRegister, _selectionChecker, _regionBuilder);
            edgeDrawing.Draw(_edgesCanvas);
         }
      }

      private (Rectangle SumRectangle, Rectangle ActivationFunctionRectangle, Rectangle OutputRectangle, Position InputPosition)
          GetSizePositions(Rectangle rect)
      {
         if (_cache.OutputSize is null)
         {
            _cache.OutputSize = _cache.SumSize; //the same size
         }

         var side = rect.Size.Width;

         var inputPosition = new Position(rect.Position.X, _cache.GetInputPositionY(rect.Position.Y));
         var valuesX = (rect.Position.X + side / 2) - (_cache.SumSize.Width / 2);

         var activationFunctionRectangle = new Rectangle(_cache.GetActivationFunctionPosition(rect), _cache.ActivationFunctionSize);
         var sumRectangle = new Rectangle(new Position(valuesX, (activationFunctionRectangle.Position.Y - _cache.SumSize.Height) - _cache.ActivationFunctionVerticalMargins), _cache.SumSize);
         var outputRectangle = new Rectangle(new Position(valuesX, _cache.GetOutputPositionY(rect.Position.Y)), _cache.OutputSize);

         return (sumRectangle, activationFunctionRectangle, outputRectangle, inputPosition);
      }

      private void DrawActivationFunction(Rectangle rectangle, ICanvas canvas)
      {
         if (rectangle.Size.Width <= 8 || rectangle.Size.Height <= 8)
         {
            return;
         }

         var position = rectangle.Position;
         var size = rectangle.Size;

         switch (this.Element.ActivationFunction)
         {
            case ActivationFunction.Relu:
               DrawStrokedActivationFunction(rectangle.Size, (pen, stroke) =>
               {
                  canvas.DrawLine(new Position(position.X, (position.Y + size.Height) - stroke), new Position(position.X + size.Width / 2, (position.Y + size.Height) - stroke), pen);
                  canvas.DrawLine(new Position(position.X + size.Width / 2 - stroke / 2, (position.Y + size.Height) - stroke), new Position((position.X + size.Width) - stroke, position.Y), pen);
               });

               break;

            case ActivationFunction.None:
               //Don't draw anything!
               return;

            case ActivationFunction.BinaryStep:
               DrawStrokedActivationFunction(size, (pen, stroke) =>
               {
                  canvas.DrawLine(new Position(position.X + size.Width / 2, position.Y + stroke / 2), new Position(position.X + size.Width, position.Y + stroke / 2), pen);
                  canvas.DrawLine(new Position(position.X + size.Width / 2, position.Y), new Position(position.X + size.Width / 2, position.Y + size.Height), pen);
                  canvas.DrawLine(new Position(position.X, position.Y + size.Height - stroke / 2), new Position(position.X + size.Width / 2, position.Y + size.Height - stroke / 2), pen);
               });

               break;

            case ActivationFunction.Linear:
               DrawStrokedActivationFunction(size, (pen, stroke) =>
               {
                  canvas.DrawLine(new Position(position.X, position.Y + size.Height), new Position(position.X + size.Width, position.Y), pen);
               });

               break;

            case ActivationFunction.LeakyRelu:
               DrawStrokedActivationFunction(size, (pen, stroke) =>
               {
                  canvas.DrawLine(new Position(position.X, (position.Y + size.Height) - stroke), new Position(position.X + size.Width / 2, (position.Y + size.Height - size.Height / 10) - stroke), pen);
                  canvas.DrawLine(new Position(position.X + size.Width / 2 - stroke / 2, (position.Y + size.Height - size.Height / 10) - stroke), new Position((position.X + size.Width) - stroke, position.Y), pen);
               });

               break;

            case ActivationFunction.Softmax:
               DrawStrokedActivationFunction(size, (pen, stroke) =>
               {
                  var x_centered = position.X + size.Width / 2 - stroke / 2;

                  canvas.DrawLine(new Position(x_centered - (int)(stroke * 0.5) - (int)(stroke * 1.5), position.Y), new Position(x_centered - (int)(stroke * 0.5) + (int)(stroke * 1.5), position.Y), pen);
                  canvas.DrawLine(new Position(x_centered - (int)(stroke * 0.5), position.Y), new Position(x_centered - (int)(stroke * 0.5), position.Y + size.Height), pen);
                  canvas.DrawLine(new Position(x_centered - (int)(stroke * 0.5) - (int)(stroke * 1.5), position.Y + size.Height), new Position(x_centered - (int)(stroke * 0.5) + (int)(stroke * 1.5), position.Y + size.Height), pen);
                  canvas.DrawLine(new Position(x_centered + (int)(stroke * 2.5), position.Y + (int)(size.Height * 0.2)), new Position(x_centered + (int)(stroke * 2.5), position.Y + size.Height + stroke / 2), pen);
               });

               break;

            case ActivationFunction.Sigmoid:
               DrawByCharActivationFunction('S', "verdana", FontStyle.Italic, rectangle, canvas);
               break;

            case ActivationFunction.Tanh:
               DrawByCharActivationFunction('\u222B', "verdana", FontStyle.Regular, rectangle, canvas);
               break;

            default:
               throw new InvalidOperationException("Inexistent drawing for the following activation function: " + this.Element.ActivationFunction);
         }
      }

      private void DrawStrokedActivationFunction(Size size, Action<Pen, int> drawAction)
      {
         var stroke = Math.Min(size.Width, size.Height) / 15;
         var pen = new Pen(new SolidBrush(Color.Black), LineStyle.Solid, stroke, Cap.None, Cap.None);

         drawAction(pen, stroke);
      }

      private void DrawByCharActivationFunction(char character, string fontfamily, FontStyle fontStyle, Rectangle rectangle, ICanvas canvas)
      {
         var format = new TextFormat(HorizontalAlignment.Center, VerticalAlignment.Middle, TextTrimming.None);
         var brush = new SolidBrush(Color.Black);
         var font = new FontLabel(fontfamily, fontStyle, brush, format);

         canvas.DrawText(character.ToString(), font, rectangle);
      }
   }
}
