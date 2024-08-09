using System;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Vertex
    {
        public float x;
        public float y;
        public float z;

        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Vertex other = (Vertex)obj;
            return Math.Abs(x - other.x) < 0.01 && Math.Abs(y - other.y) < 0.01 && Math.Abs(z - other.z) < 0.01;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                hash = hash * 23 + z.GetHashCode();
                return hash;
            }
        }
    }
}