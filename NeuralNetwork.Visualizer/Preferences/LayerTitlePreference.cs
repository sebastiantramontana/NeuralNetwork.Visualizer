using NeuralNetwork.Visualizer.Preferences.Brushes;
using NeuralNetwork.Visualizer.Preferences.Text;
using System;
using System.Drawing;

namespace NeuralNetwork.Visualizer.Preferences
{
    public class LayerTitlePreference : IDisposable
    {
        private TextPreference _font;
        public TextPreference Font
        {
            get => _font ?? (_font = new TextPreference());
            set => _font = value;
        }

        private IBrush _background;
        public IBrush Background
        {
            get => _background ?? (_background = new SolidBrushPreference(Color.Transparent));
            set => _background = value;
        }

        public int Height { get; set; }

        public void Dispose()
        {
            Destroy.Disposable(ref _font);
        }
    }
}
