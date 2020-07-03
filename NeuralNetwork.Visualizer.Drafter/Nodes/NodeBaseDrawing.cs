using NeuralNetwork.Model.Nodes;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Contracts.Selection;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Drawing.Nodes
{
   internal abstract class NodeBaseDrawing<TNode> : ElementDrawingBase<TNode>, INodeDrawing where TNode : NodeBase
   {
      private readonly INodePreference _preferences;
      private readonly NodeSizesPreCalc _cache;
      private readonly ISelectableElementRegister _selectableElementRegister;
      private readonly IElementSelectionChecker _selectionChecker;
      private readonly IRegionBuilder _regionBuilder;

      internal NodeBaseDrawing(TNode element, INodePreference preferences, NodeSizesPreCalc cache, ISelectableElementRegister selectableElementRegister, IElementSelectionChecker selectionChecker, IRegionBuilder regionBuilder) : base(element)
      {
         _preferences = preferences;
         _cache = cache;
         _selectableElementRegister = selectableElementRegister;
         _selectionChecker = selectionChecker;
         _regionBuilder = regionBuilder;
      }

      public Position EdgeStartPosition { get; private set; }
      public ICanvas Canvas { get; private set; }

      public override Task Draw(ICanvas canvas)
      {
         if (_cache.EllipseRectangle is null)
         {
            var side = Math.Min(canvas.Size.Width, canvas.Size.Height);

            var x_centered = (canvas.Size.Width - side) / 2;
            var y_centered = (canvas.Size.Height - side) / 2;

            _cache.EllipseRectangle = new Rectangle(new Position(x_centered, y_centered), new Size(side, side));
         }

         RegisterSelectableNodeEllipse(canvas);

         this.Canvas = canvas;
         this.EdgeStartPosition = new Position(_cache.EllipseRectangle.Position.X + _cache.EllipseRectangle.Size.Width, _cache.EllipseRectangle.Position.Y + (_cache.EllipseRectangle.Size.Height / 2));

         bool isSelected = _selectionChecker.IsSelected(this.Element);

         var info = _preferences.GetInfoBySelection(isSelected);

         var taskEllipse = canvas.DrawEllipse(_cache.EllipseRectangle, info.Border, info.Background);
         var taskContent = DrawContent(canvas, _cache.EllipseRectangle);

         return Task.WhenAll(taskEllipse, taskContent);
      }

      private void RegisterSelectableNodeEllipse(ICanvas canvas)
      {
         _selectableElementRegister.Register(new RegistrationInfo(this.Element, canvas, _regionBuilder.Ellipse(_cache.EllipseRectangle), 2));
      }

      public NodeBase Node => this.Element;
      protected abstract Task DrawContent(ICanvas canvas, Rectangle rect);
   }
}
