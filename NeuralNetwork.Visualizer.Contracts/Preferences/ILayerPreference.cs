namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
   public interface ILayerPreference : ISelectable2DPreference
   {
      ILayerTitlePreference Title { get; set; }
   }
}