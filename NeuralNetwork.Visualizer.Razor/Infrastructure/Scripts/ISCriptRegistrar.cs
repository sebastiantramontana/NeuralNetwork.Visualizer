using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal interface IScriptRegistrar
   {
      IScriptRegistrarInclusion Register(IScriptRegistration script, string globalInstaceName);
   }
}
