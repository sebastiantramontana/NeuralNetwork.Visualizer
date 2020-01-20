using NeuralNetwork.Infrastructure.Winform;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Contracts.Drawing;
using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using NeuralNetwork.Visualizer.Contracts.Preferences;
using NeuralNetwork.Visualizer.Winform.Drawing.Canvas.GdiMapping;
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gdi = System.Drawing;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   internal class DrawableSurface : IDrawableSurface, ICanvasBuilder, IDisposable
   {
      private readonly PictureBox _pictureBox;
      private readonly IInvoker _invoker;
      private readonly NeuralNetworkVisualizerControl _control;
      private readonly IGdiImageCanvasBuilder _gdiImageCanvasBuilder;

      internal DrawableSurface(PictureBox pictureBox, NeuralNetworkVisualizerControl control, IDrafter drafter, IInvoker invoker, IGdiImageCanvasBuilder gdiImageCanvasBuilder)
      {
         _pictureBox = pictureBox;
         _control = control;
         _invoker = invoker;
         this.Drafter = drafter;
         _gdiImageCanvasBuilder = gdiImageCanvasBuilder;
      }

      public Image GetImage()
      {
         var img = _invoker.SafeInvoke(() => _pictureBox.Image?.Clone() as Gdi.Image
             ?? new Gdi.Bitmap(_control.ClientSize.Width, _control.ClientSize.Height));  //Clone for safe handling

         return img.ToVisualizer();
      }

      public IDrafter Drafter { get; }
      public Size Size => _control.Size.ToVisualizer();
      public Size DrawingSize => _pictureBox.ClientSize.ToVisualizer();

      private bool _isDrawing = false;

      public async Task RedrawAsync()
      {
         if (!_control.IsHandleCreated || _isDrawing)
            return;

         _isDrawing = true;

         if (CheckBlank(_control, _pictureBox, _invoker))
         {
            _isDrawing = false;
            return;
         }

         await this.Drafter.RedrawAsync(this);
         UpdatePictureBox(_gdiImageCanvasBuilder.CurrentImage, _pictureBox, _invoker);

         _gdiImageCanvasBuilder.Dispose();
         _isDrawing = false;
      }

      public ICanvas Build(Size size)
      {
         return _gdiImageCanvasBuilder.Build(size, _control.Preferences.Quality);
      }

      private void UpdatePictureBox(Gdi.Image image, PictureBox pictureBox, IInvoker invoker)
      {
         invoker.SafeInvoke(() =>
         {
            pictureBox.ClientSize = image.Size;
            pictureBox.Image = image;
         });
      }

      private bool CheckBlank(NeuralNetworkVisualizerControl control, PictureBox pictureBox, IInvoker invoker)
      {
         if (control.InputLayer != null)
            return false;

         SetBlank(control, pictureBox, invoker);
         return true;
      }

      private void SetBlank(NeuralNetworkVisualizerControl control, PictureBox pictureBox, IInvoker invoker)
      {
         invoker.SafeInvoke(() =>
         {
            DestroyImagePictureBox(pictureBox);

            pictureBox.ClientSize = control.ClientSize;
            pictureBox.BackColor = control.BackColor;

         });
      }

      private void DestroyImagePictureBox(PictureBox pictureBox)
      {
         if (pictureBox.Image != null) //Clear before anything
         {
            pictureBox.Image.Dispose();
            pictureBox.Image = null;
         }
      }

      public void Dispose()
      {
         _gdiImageCanvasBuilder.Dispose();
      }
   }
}
