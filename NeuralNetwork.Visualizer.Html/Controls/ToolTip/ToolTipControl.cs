using NeuralNetwork.Visualizer.Contracts.Controls;

namespace NeuralNetwork.Visualizer.Html.Controls.ToolTip
{
   internal class ToolTipControl : IToolTip
   {
      private readonly IToolTipDomAccess _toolTipDomAccess;

      internal ToolTipControl(IToolTipDomAccess toolTipDomAccess)
      {
         _toolTipDomAccess = toolTipDomAccess;
      }

      public async void Show(string title, string text)
      {
         await _toolTipDomAccess.Show(title, text);
      }

      public async void Close()
      {
         await _toolTipDomAccess.Close();
      }
   }
}
