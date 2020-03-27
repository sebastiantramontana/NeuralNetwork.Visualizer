using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Controls.ToolTip
{
   internal interface IToolTipDomAccess
   {
      ValueTask Close();
      ValueTask Show(string title, string text);
   }
}