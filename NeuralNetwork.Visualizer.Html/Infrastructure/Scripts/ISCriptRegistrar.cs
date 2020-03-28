using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Html.Infrastructure.Scripts
{
   internal interface IScriptRegistrar
   {
      ValueTask Register(IScriptRegistration script);
   }
}
