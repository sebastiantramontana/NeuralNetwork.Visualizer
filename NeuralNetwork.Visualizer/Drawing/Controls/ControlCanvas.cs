using NeuralNetwork.Visualizer.Drawing.Cache;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace NeuralNetwork.Visualizer.Drawing.Controls
{
   internal class ControlCanvas : IControlCanvas
   {
      private readonly PictureBox _pictureBox;
      private readonly NeuralNetworkVisualizerControl _control;

      internal ControlCanvas(PictureBox pictureBox, NeuralNetworkVisualizerControl control)
      {
         _pictureBox = pictureBox;
         _control = control;
      }

      public NeuralNetworkVisualizerControl Control => _control;

      public Size Size
      {
         get => _pictureBox.ClientSize;
         set => _pictureBox.ClientSize = value;
      }

      public Image Image
      {
         get { return SafeInvoke(() => _pictureBox.Image?.Clone() as Image ?? new Bitmap(_control.ClientSize.Width, _control.ClientSize.Height)); } //Clone for safe handling
         set => _pictureBox.Image = value;
      }

      public bool IsReady => _control.IsHandleCreated;

      public void SafeInvoke(Action action)
      {
         if (_control.InvokeRequired)
         {
            _control.Invoke(action);
         }
         else
         {
            action();
         }
      }

      public T SafeInvoke<T>(Func<T> action)
      {
         return (_control.InvokeRequired ? (T)_control.Invoke(action) : action());
      }

      public void SetBlank()
      {
         DestroyImageCanvas();

         _pictureBox.ClientSize = _control.ClientSize;
         _pictureBox.BackColor = _control.BackColor;
      }

      public (Graphics Graph, Image Image, LayerSizesPreCalc LayerSizes) GetGraphics()
      {
         var imgSize = GetImageSize(_control.Size);
         var sizes = GetDrawingSizes(imgSize);

         _pictureBox.ClientSize = sizes.CanvasSize;
         Bitmap bmp = new Bitmap(sizes.CanvasSize.Width, sizes.CanvasSize.Height);
         Graphics graph = Graphics.FromImage(bmp);

         SetQuality(graph);

         return (graph, bmp, sizes.LayerSize);
      }

      private Size GetImageSize(Size canvasSize)
      {
         var size = new Size((int)(_control.Zoom * canvasSize.Width), (int)(_control.Zoom * canvasSize.Height));
         return size;
      }

      private (LayerSizesPreCalc LayerSize, Size CanvasSize) GetDrawingSizes(Size initialSize)
      {
         var layersCount = _control.InputLayer.CountLayers();
         var maxNodes = _control.InputLayer.GetMaxNodeCountInLayer();
         var preferences = _control.Preferences;

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

      private void SetQuality(Graphics graphics)
      {
         switch (_control.Preferences.Quality)
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
               throw new InvalidOperationException($"Quality not implemented: {_control.Preferences.Quality}");
         }
      }
   }
}
