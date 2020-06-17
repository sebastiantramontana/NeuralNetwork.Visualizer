using NeuralNetwork.Visualizer.Contracts.Drawing.Core.Primitives;
using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Contracts.Controls
{
   public interface IToolTipFiring
   {
      Task Show(Position position);
      Task Hide();
   }
}
