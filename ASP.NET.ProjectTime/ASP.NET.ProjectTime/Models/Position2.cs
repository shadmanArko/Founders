using System;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Position2
    {
        public int x;
        public int y;

        public Position2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Position2 otherPosition = (Position2)obj;
            return (x == otherPosition.x) && (y == otherPosition.y);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(x, y).GetHashCode();
        }
    }
}