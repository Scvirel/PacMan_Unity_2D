

using Game.Misc;

namespace Game.Model
{
    public partial class ModelGhost
    {
        protected class GhostRandom : IGhostWritableRandom
        {
            private int _x;
            private int _y;
            private eDirection direction;
            private eDirection previous;
            private bool canMove;
            public GhostRandom(int x, int y)
            {
                _x = x;
                _y = y;
            }

            public bool CanMove { get { return canMove; } set { canMove = value; } }

            int IGhostRandom.X => _x;
            int IGhostRandom.Y => _y;

            eDirection IGhostRandom.Direction { get { return direction; } set { direction = value; } }
            eDirection IGhostRandom.Previous { get { return previous; } set { previous = value; } }

            public void UpdatePositionRandom(int x, int y)
            {
                _x = x;
                _y = y;
            }
        }
        protected class GhostFollow : IGhostWritableFollow
        {
            private int _x;
            private int _y;
            private eDirection direction;
            private eDirection previous;
            private bool canMove;
            public GhostFollow(int x, int y)
            {
                _x = x;
                _y = y;
            }

            public bool CanMove { get { return canMove; } set { canMove = value; } }

            int IGhostFollow.X => _x;
            int IGhostFollow.Y => _y;

            eDirection IGhostFollow.Direction { get { return direction; } set { direction = value; } }
            eDirection IGhostFollow.Previous { get { return previous; } set { previous = value; } }

            public void UpdatePositionFollow(int x, int y)
            {
                _x = x;
                _y = y;
            }
        }
    }
}
        

