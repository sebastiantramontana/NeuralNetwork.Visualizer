namespace NeuralNetwork.Visualizer.Preferences.Formatting
{
    public interface IFormatter<T>
    {
        T GetFormat(double? value);
    }
}
