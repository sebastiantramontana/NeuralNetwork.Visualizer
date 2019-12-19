namespace NeuralNetwork.Visualizer.Contracts.Preferences
{
    public interface IFormatter<T>
    {
        T GetFormat(double? value);
    }
}
