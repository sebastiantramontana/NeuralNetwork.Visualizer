namespace NeuralNetworkVisualizer.Preferences.Formatting
{
    public interface IFormatter<T>
    {
        T GetFormat(double? value);
    }
}
