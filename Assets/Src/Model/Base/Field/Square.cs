using Game.Misc;
using System.Collections.Generic;

namespace Game.Model
{
    public partial class ModelBase
    {
        public class Square
        {
            public int x;
            public int y;
            public List<eDirection> blockedDirections = new List<eDirection>();

            public Square(int x, int y, eDirection direction1)
            {
                this.x = x;
                this.y = y;
                blockedDirections.Add(direction1);
            }
            public Square(int x, int y, eDirection direction1, eDirection direction2)
            {
                this.x = x;
                this.y = y;
                blockedDirections.Add(direction1);
                blockedDirections.Add(direction2);
            }
        }
    }
}
