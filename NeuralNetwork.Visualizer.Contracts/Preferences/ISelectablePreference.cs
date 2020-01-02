namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface ISelectablePreference<T>
   {
      T GetInfoBySelection(bool isSelected);
   }
}
