using NeuralNetwork.Visualizer.Html.Infrastructure.Scripts;

namespace NeuralNetwork.Visualizer.Html.Controls.ToolTip
{
   internal class ToolTipScriptRegistration : IScriptRegistration
   {
      public ToolTipScriptRegistration(string globalInstanceName)
      {
         Code = $@"((globalInstanceName) => {{
                  const thisToolTip = this;

                  changeDisplay = (value) => {{
                     let tooltip = document.getElementById('tooltip-' + globalInstanceName);
                     tooltip.style.display = value;
                  }};

                  changeText = (idPart, text) => {{
                     let elem = document.getElementById('tooltip-' + idPart + '-' + globalInstanceName);
                     elem.innerHtml = text;
                  }};

                  window[globalInstanceName].ToolTip = {{
                     show: (title, text) => {{
                        thisToolTip.changeText('title', title);
                        thisToolTip.changeText('text', text);
                        thisToolTip.changeDisplay('block');
                     }},

                     close: () => {{
                        thisToolTip.changeDisplay('none');
                     }}
                  }};
               }})('{globalInstanceName}');";
      }

      public string Code { get; }
   }
}
