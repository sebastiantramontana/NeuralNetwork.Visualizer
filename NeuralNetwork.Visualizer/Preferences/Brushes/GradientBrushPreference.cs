using System.Drawing;
using System.Drawing.Drawing2D;

namespace NeuralNetwork.Visualizer.Preferences.Brushes
{
    public class GradientBrushPreference : IBrushPreference
    {
        private readonly Color _color1;
        private readonly Color _color2;
        private readonly int _angle;

        public GradientBrushPreference(Color color1, Color color2, int angle)
        {
            this._color1 = color1;
            this._color2 = color2;
            this._angle = angle;
        }

        internal Rectangle Rectangle { get; set; }
        public Brush CreateBrush()
        {
            return new LinearGradientBrush(this.Rectangle, _color1, _color2, _angle);
        }
    }
}
