using NeuralNetwork.Infrastructure.Winform;
using NeuralNetwork.Visualizer.Calcs;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   internal class ControlCanvas : IControlCanvas
   {
      private readonly PictureBox _pictureBox;
      private readonly IInvoker _invoker;

      internal ControlCanvas(PictureBox pictureBox, NeuralNetworkVisualizerControl control, IInvoker invoker)
      {
         _pictureBox = pictureBox;
         Control = control;
         _invoker = invoker;
      }

      public NeuralNetworkVisualizerControl Control { get; }

      public Size Size
      {
         get => _pictureBox.ClientSize.ToVisualizer();
         set => _pictureBox.ClientSize = value.ToGdi();
      }

      public Gdi.Image Image
      {
         get { return _invoker.SafeInvoke(() => _pictureBox.Image?.Clone() as Gdi.Image ?? new Gdi.Bitmap(Control.ClientSize.Width, Control.ClientSize.Height)); } //Clone for safe handling
         set => _pictureBox.Image = value;
      }

      public bool IsReady => Control.IsHandleCreated;

      public void SetBlank()
      {
         DestroyImageCanvas();

         _pictureBox.ClientSize = Control.ClientSize;
         _pictureBox.BackColor = Control.BackColor;
      }

      public (Gdi.Graphics Graph, Gdi.Image Image, LayerSizesPreCalc LayerSizes) GetGraphics()
      {
         var imgSize = GetImageSize(Control.Size.ToVisualizer());
         var sizes = GetDrawingSizes(imgSize);

         _pictureBox.ClientSize = sizes.CanvasSize.ToGdi();
         Gdi.Bitmap bmp = new Gdi.Bitmap(sizes.CanvasSize.Width, sizes.CanvasSize.Height);
         Gdi.Graphics graph = Gdi.Graphics.FromImage(bmp);

         SetQuality(graph);

         return (graph, bmp, sizes.LayerSize);
      }

      private Size GetImageSize(Size canvasSize)
      {
         var size = new Size((int)(Control.Zoom * canvasSize.Width), (int)(Control.Zoom * canvasSize.Height));
         return size;
      }

      private (LayerSizesPreCalc LayerSize, Size CanvasSize) GetDrawingSizes(Size initialSize)
      {
         var layersCount = Control.InputLayer.CountLayers();
         var maxNodes = Control.InputLayer.GetMaxNodeCountInLayer();
         var preferences = Control.Preferences;

         var layerSize = new LayerSizesPreCalc(initialSize, layersCount, maxNodes, preferences);
         var canvasSize = new Size(initialSize.Width, layerSize.Height);

         return (layerSize, canvasSize);
      }

      private void DestroyImageCanvas()
      {
         if (_pictureBox.Image != null) //Clear before anything
         {
            _pictureBox.Image.Dispose();
            _pictureBox.Image = null;
         }
      }

      private void SetQuality(Gdi.Graphics graphics)
      {
         switch (Control.Preferences.Quality)
         {
            case RenderQuality.Low:
               graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
               graphics.CompositingQuality = CompositingQuality.HighSpeed;
               graphics.SmoothingMode = SmoothingMode.HighSpeed;
               graphics.InterpolationMode = InterpolationMode.Low;
               graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
               break;
            case RenderQuality.Medium:
               graphics.PixelOffsetMode = PixelOffsetMode.Half;
               graphics.CompositingQuality = CompositingQuality.AssumeLinear;
               graphics.SmoothingMode = SmoothingMode.AntiAlias;
               graphics.InterpolationMode = InterpolationMode.Bicubic;
               graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
               break;
            case RenderQuality.High:
               graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
               graphics.CompositingQuality = CompositingQuality.HighQuality;
               graphics.SmoothingMode = SmoothingMode.HighQuality;
               graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
               graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
               break;
            default:
               throw new InvalidOperationException($"Quality not implemented: {Control.Preferences.Quality}");
         }
      }
   }
}
