using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal interface IScriptFileRegistrarInclusion
   {
      ValueTask<IScriptRegistrar> Include(string fileName);
   }
}
