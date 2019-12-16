using System;

namespace NeuralNetwork.Visualizer.Preferences.Core
{
   public class Position : IEquatable<Position>
   {
      public Position(int x, int y)
      {
         this.X = x;
         this.Y = y;
      }

      public int X { get; }
      public int Y { get; }

      public override bool Equals(object obj)
      {
         return Equals(obj as Position);
      }

      public bool Equals(Position other)
      {
         return !(other is null)
            && other.X == this.X
            && other.Y == this.Y;
      }

      public override int GetHashCode()
      {
         return (this.X, this.Y).GetHashCode();
      }

      public static bool operator ==(Position pos1, Position pos2)
      {
         return (pos1?.Equals(pos2)) ?? false;
      }

      public static bool operator !=(Position pos1, Position pos2)
      {
         return !(pos1 == pos2);
      }

      public static Position operator +(Position pos1, Position pos2)
      {
         var x = (pos1?.X ?? 0) + (pos2?.X ?? 0);
         var y = (pos1?.Y ?? 0) + (pos2?.Y ?? 0);

         return new Position(x, y);
      }

      public static Position operator +(Position pos1, int amount)
      {
         var x = (pos1?.X ?? 0) + amount;
         var y = (pos1?.Y ?? 0) + amount;

         return new Position(x, y);
      }

      public static Position operator -(Position pos1, Position pos2)
      {
         var x = (pos1?.X ?? 0) - (pos2?.X ?? 0);
         var y = (pos1?.Y ?? 0) - (pos2?.Y ?? 0);

         return new Position(x, y);
      }

      public static Position operator -(Position pos1, int amount)
      {
         return pos1 + -amount;
      }
   }
}
