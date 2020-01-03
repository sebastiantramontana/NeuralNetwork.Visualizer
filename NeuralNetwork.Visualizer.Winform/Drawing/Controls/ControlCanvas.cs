using NeuralNetwork.Infrastructure.Winform;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Drawing;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas;
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
      private readonly NeuralNetworkVisualizerControl _control;
      private readonly IDrafter _drafter;

      internal ControlCanvas(PictureBox pictureBox, NeuralNetworkVisualizerControl control, IDrafter drafter, IInvoker invoker)
      {
         _pictureBox = pictureBox;
         _control = control;
         _drafter = drafter;
         _invoker = invoker;
      }

      public Gdi.Image GetImage()
      {
         return _invoker.SafeInvoke(() => _pictureBox.Image?.Clone() as Gdi.Image
            ?? new Gdi.Bitmap(_control.ClientSize.Width, _control.ClientSize.Height));  //Clone for safe handling
      }

      public void Redraw()
      {
         if (!_control.IsHandleCreated)
            return;

         if (_control.InputLayer == null)
         {
            SetBlank();
            return;
         }

         _drafter.Redraw(this);
         Destroy.Disposable(ref _graph);
      }

      private Gdi.Graphics _graph = null;
      public ICanvas Build(Size size)
      {
         var bmp = new Gdi.Bitmap(size.Width, size.Height);
         _graph = Gdi.Graphics.FromImage(bmp);
         _invoker.SafeInvoke(() => _pictureBox.Image = bmp);

         SetQuality(_graph);

         return new GraphicsCanvas(_graph, size);
      }

      private void SetBlank()
      {
         _invoker.SafeInvoke(() =>
         {
            DestroyImageCanvas();

            _pictureBox.ClientSize = _control.ClientSize;
            _pictureBox.BackColor = _control.BackColor;

         });
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
