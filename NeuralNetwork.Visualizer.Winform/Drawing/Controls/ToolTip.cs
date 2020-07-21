using NeuralNetwork.Infrastructure.Winform;
using NeuralNetwork.Visualizer.Contracts.Controls;
using NeuralNetwork.Visualizer.Drawing;
using System.Windows.Forms;
using WinToolTip = System.Windows.Forms.ToolTip;

namespace NeuralNetwork.Visualizer.Winform.Drawing.Controls
{
   public class ToolTip : IToolTip
   {
      private WinToolTip _tooltip = null;
      private readonly IInvoker _invoker;
      private readonly Control _controlToToolTip;

      public ToolTip(IInvoker invoker, Control controlToToolTip)
      {
         _invoker = invoker;
         _controlToToolTip = controlToToolTip;
      }

      public void Show(string title, string text)
      {
         _tooltip = new WinToolTip
         {
            ToolTipIcon = ToolTipIcon.Info,
            UseFading = true,

            ToolTipTitle = title
         };

         _invoker.SafeInvoke(() => _tooltip.Show(text, _controlToToolTip));
      }

      public void Close()
      {
         Destroy.Disposable(ref _tooltip);
      }
   }
}
