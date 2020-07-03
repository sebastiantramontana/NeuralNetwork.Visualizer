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
using System.Linq;
using System.Threading.Tasks;

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

      public override Task Draw(ICanvas canvas)
      {
         var taskBase = base.Draw(canvas);
         var taskLabel = DrawLabel(canvas);

         return Task.WhenAll(taskBase, taskLabel);
      }

      protected override Task DrawContent(ICanvas canvas, Rectangle rect)
      {
         var sizesPositions = GetSizePositions(rect);

         var roundingDigits = _preferences.Neurons.RoundingDigits;

         var tasks = new List<Task>();

         if (this.Element.SumValue.HasValue)
         {
            var sumFont = _preferences.Neurons.SumValueFormatter.GetFormat(this.Element.SumValue.Value);
            tasks.Add(canvas.DrawText('\u2211' + " " + Math.Round(this.Element.SumValue.Value, roundingDigits).ToString(), sumFont, sizesPositions.SumRectangle));
         }

         tasks.Add(DrawActivationFunction(sizesPositions.ActivationFunctionPosition, sizesPositions.ActivationFunctionSize, canvas));

         if (this.Element.OutputValue.HasValue)
         {
            var outputFont = _preferences.Neurons.OutputValueFormatter.GetFormat(this.Element.OutputValue.Value);

            tasks.Add(canvas.DrawText(Math.Round(this.Element.OutputValue.Value, roundingDigits).ToString(), outputFont, sizesPositions.OutputRectangle));
         }

         tasks.Add(DrawEdges(sizesPositions.InputPosition, canvas, sizesPositions.OutputRectangle.Size.Height));

         return Task.WhenAll(tasks);
      }

      private Task DrawLabel(ICanvas canvas)
      {
         if (this.Element.Layer.Next != null || _preferences.OutputFontLabel == FontLabel.Null || _cache.EllipseRectangle is null)
            return Task.CompletedTask;

         var fontLabel = _preferences.OutputFontLabel;

         if (_cache.OutputLabelRectangle is null)
         {
            var rect = _cache.EllipseRectangle;
            var labelWidth = canvas.Size.Width - (rect.Position.X + rect.Size.Width + _preferences.NodeMargins);
            var labelHeight = fontLabel.Size;
            _cache.OutputLabelRectangle = new Rectangle(new Position(rect.Position.X + rect.Size.Width + _preferences.NodeMargins, (rect.Size.Height - labelHeight) / 2), new Size(labelWidth, labelHeight));
         }

         var label = this.Element.Id;

         return canvas.DrawText(label, fontLabel, _cache.OutputLabelRectangle);
      }

      private Task DrawEdges(Position inputPosition, ICanvas canvas, int textEdgeHeight)
      {
         var tasks = new List<Task>(this.Element.Edges.Count());

         foreach (var edge in this.Element.Edges)
         {
            var previousNode = _previousNodes[edge.Source];
            var outputPositionTrans = previousNode.Canvas.Translate(previousNode.EdgeStartPosition, _edgesCanvas);
            var inputPositionTrans = canvas.Translate(inputPosition, _edgesCanvas);

            var edgeDrawing = new EdgeDrawing(edge, _preferences.Edges, outputPositionTrans, inputPositionTrans, textEdgeHeight, _edgesCache, _selectableElementRegister, _selectionChecker, _regionBuilder);
            tasks.Add(edgeDrawing.Draw(_edgesCanvas));
         }

         return Task.WhenAll(tasks);
      }

      private (Rectangle SumRectangle, Position ActivationFunctionPosition, Size ActivationFunctionSize, Rectangle OutputRectangle, Position InputPosition)
          GetSizePositions(Rectangle rect)
      {
         if (_cache.OutputSize is null)
         {
            _cache.OutputSize = _cache.SumSize; //the same size
         }

         var side = rect.Size.Width;

         var inputPosition = new Position(rect.Position.X, _cache.GetInputPositionY(rect.Position.Y));
         var valuesX = (rect.Position.X + side / 2) - (_cache.SumSize.Width / 2);

         var activationFunctionPosition = _cache.GetActivationFunctionPosition(rect);
         var sumRectangle = new Rectangle(new Position(valuesX, (activationFunctionPosition.Y - _cache.SumSize.Height) - _preferences.NodeMargins), _cache.SumSize);
         var outputRectangle = new Rectangle(new Position(valuesX, _cache.GetOutputPositionY(rect.Position.Y)), _cache.OutputSize);

         return (sumRectangle, activationFunctionPosition, _cache.ActivationFunctionSize, outputRectangle, inputPosition);
      }

      private Task DrawActivationFunction(Position position, Size size, ICanvas canvas)
      {
         if (size.Width <= 8 || size.Height <= 8)
         {
            return Task.CompletedTask;
         }

         switch (this.Element.ActivationFunction)
         {
            case ActivationFunction.Relu:
               return DrawStrokedActivationFunction(size, (pen, stroke) =>
                {
                   var task1 = canvas.DrawLine(new Position(position.X, (position.Y + size.Height) - stroke), new Position(position.X + size.Width / 2, (position.Y + size.Height) - stroke), pen);
                   var task2 = canvas.DrawLine(new Position(position.X + size.Width / 2 - stroke / 2, (position.Y + size.Height) - stroke), new Position((position.X + size.Width) - stroke, position.Y), pen);

                   return Task.WhenAll(task1, task2);
                });

            case ActivationFunction.None:
               //Don't draw anything!
               return Task.CompletedTask;

            case ActivationFunction.BinaryStep:
               return DrawStrokedActivationFunction(size, (pen, stroke) =>
               {
                  var task1 = canvas.DrawLine(new Position(position.X + size.Width / 2, position.Y + stroke / 2), new Position(position.X + size.Width, position.Y + stroke / 2), pen);
                  var task2 = canvas.DrawLine(new Position(position.X + size.Width / 2, position.Y), new Position(position.X + size.Width / 2, position.Y + size.Height), pen);
                  var task3 = canvas.DrawLine(new Position(position.X, position.Y + size.Height - stroke / 2), new Position(position.X + size.Width / 2, position.Y + size.Height - stroke / 2), pen);

                  return Task.WhenAll(task1, task2, task3);
               });

            case ActivationFunction.Linear:
               return DrawStrokedActivationFunction(size, (pen, stroke) =>
              {
                 return canvas.DrawLine(new Position(position.X, position.Y + size.Height), new Position(position.X + size.Width, position.Y), pen);
              });

            case ActivationFunction.LeakyRelu:
               return DrawStrokedActivationFunction(size, (pen, stroke) =>
               {
                  var task1 = canvas.DrawLine(new Position(position.X, (position.Y + size.Height) - stroke), new Position(position.X + size.Width / 2, (position.Y + size.Height - size.Height / 10) - stroke), pen);
                  var task2 = canvas.DrawLine(new Position(position.X + size.Width / 2 - stroke / 2, (position.Y + size.Height - size.Height / 10) - stroke), new Position((position.X + size.Width) - stroke, position.Y), pen);

                  return Task.WhenAll(task1, task2);
               });

            case ActivationFunction.Softmax:
               return DrawStrokedActivationFunction(size, (pen, stroke) =>
              {
                 var x_centered = position.X + size.Width / 2 - stroke / 2;

                 var task1 = canvas.DrawLine(new Position(x_centered - (int)(stroke * 0.5) - (int)(stroke * 1.5), position.Y), new Position(x_centered - (int)(stroke * 0.5) + (int)(stroke * 1.5), position.Y), pen);
                 var task2 = canvas.DrawLine(new Position(x_centered - (int)(stroke * 0.5), position.Y), new Position(x_centered - (int)(stroke * 0.5), position.Y + size.Height), pen);
                 var task3 = canvas.DrawLine(new Position(x_centered - (int)(stroke * 0.5) - (int)(stroke * 1.5), position.Y + size.Height), new Position(x_centered - (int)(stroke * 0.5) + (int)(stroke * 1.5), position.Y + size.Height), pen);
                 var task4 = canvas.DrawLine(new Position(x_centered + (int)(stroke * 2.5), position.Y + (int)(size.Height * 0.2)), new Position(x_centered + (int)(stroke * 2.5), position.Y + size.Height + stroke / 2), pen);

                 return Task.WhenAll(task1, task2, task3, task4);
              });

            case ActivationFunction.Sigmoid:
               return DrawByCharActivationFunction('\u0283', "verdana", position, size, canvas);

            case ActivationFunction.Tanh:
               return DrawByCharActivationFunction('\u222B', "Verdana", position, size, canvas);

            default:
               throw new InvalidOperationException("Inexistent drawing for the following activation function: " + this.Element.ActivationFunction);
         }
      }

      private Task DrawStrokedActivationFunction(Size size, Func<Pen, int, Task> drawAction)
      {
         var stroke = Math.Min(size.Width, size.Height) / 15;
         var pen = new Pen(new SolidBrush(Color.Black), LineStyle.Solid, stroke, Cap.None, Cap.None);

         return drawAction(pen, stroke);
      }

      private Task DrawByCharActivationFunction(char character, string fontfamily, Position position, Size size, ICanvas canvas)
      {
         var factor = size.Height / 5;
         var factorSize = factor * 2 - factor / 3;

         var format = new TextFormat(HorizontalAlignment.Center, VerticalAlignment.Middle, TextTrimming.None);
         var brush = new SolidBrush(Color.Black);
         var font = new FontLabel(fontfamily, FontStyle.Italic, 8, brush, format);

         return canvas.DrawText(character.ToString(), font, new Rectangle(new Position(position.X - factor, position.Y - factor), new Size(size.Width + factorSize, size.Height + factorSize)));
      }
   }
}
