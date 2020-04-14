using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal interface IScriptRegistrarInclusion : IScriptFileRegistrarInclusion, IScriptRegistrar
   {
      ValueTask Execute();
   }
}
