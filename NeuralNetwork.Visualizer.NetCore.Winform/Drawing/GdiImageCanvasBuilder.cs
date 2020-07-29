using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Drawing;
using NeuralNetwork.Visualizer.NetCore.Winform.Drawing.Canvas;
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.NetCore.Winform.Drawing
{
   internal class GdiImageCanvasBuilder : IGdiImageCanvasBuilder
   {
      private Gdi.Image _image = null;
      private Gdi.Graphics _graph = null;

      public Gdi.Image CurrentImage => _image.Clone() as Gdi.Image;

      public ICanvas Build(Size size, RenderQuality quality)
      {
         _image = new Gdi.Bitmap(size.Width, size.Height);
         _graph = Gdi.Graphics.FromImage(_image);

         SetQuality(quality, _graph);

         return new GraphicsCanvas(_graph, size);
      }

      private void SetQuality(RenderQuality quality, Gdi.Graphics graphics)
      {
         switch (quality)
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
               throw new InvalidOperationException($"Quality not implemented: {quality}");
         }
      }

      public void Dispose()
      {
         Destroy.Disposable(ref _graph);
         Destroy.Disposable(ref _image);
      }
   }
}
