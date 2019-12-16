namespace NeuralNetwork.Visualizer.Preferences.Brushes
{
   public abstract class BrushBase<TBrush> : IBrush<TBrush> where TBrush : class, IBrush
   {
      public bool Equals(IBrush other)
      {
         return Equals(other as TBrush);
      }

      public override bool Equals(object obj)
      {
         return base.Equals(obj as TBrush);
      }

      public abstract bool Equals(TBrush other);
      public abstract override int GetHashCode();

      public static bool operator ==(BrushBase<TBrush> left, BrushBase<TBrush> right)
      {
         return (left?.Equals(right)) ?? false;
      }

      public static bool operator !=(BrushBase<TBrush> left, BrushBase<TBrush> right)
      {
         return !(left == right);
      }
   }
}
