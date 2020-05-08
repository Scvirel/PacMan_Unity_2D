
using UnityEngine;

namespace Game.Misc
{
    public enum eDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }

    public static class Direction
    {
        public static (int x, int y)GetNextPosition(int x, int y, eDirection direction)
        {
            switch (direction)
            {
                case eDirection.LEFT:
                    return (x - 1, y);
                case eDirection.RIGHT:
                    return (x + 1, y);
                case eDirection.UP:
                    return (x, y + 1);
                default:
                    return (x, y - 1);
            }
        }

        public static int GetNextDirection(eDirection curent)
        {
            switch (curent)
            {
                case eDirection.LEFT:return 1;
                case eDirection.RIGHT: return 2;
                case eDirection.UP: return 3;
                case eDirection.DOWN: return 0;
                default: return 0;
                    
            }
            
        }

        public static int GetRandomDirection()
        {
            return Random.Range(0, 4);
        }
        
    }
}