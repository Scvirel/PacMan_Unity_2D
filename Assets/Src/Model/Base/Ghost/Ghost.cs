
using Game.Misc;

namespace Game.Model
{
    public partial class ModelBase
    {
        protected interface IGhostRandom
        {
            int X { get; }
            int Y { get; }
            eDirection Direction{get;set;}
            eDirection Previous { get; set; }
            bool CanMove { get; set; }
        }
        protected interface IGhostFollow
        {
            int X { get; }
            int Y { get; }
            eDirection Direction { get; set; }
            eDirection Previous { get; set; }
            bool CanMove { get; set; }
        }

        protected interface IGhostWritableRandom : IGhostRandom
        {
            void UpdatePositionRandom(int x, int y);
        }
        protected interface IGhostWritableFollow : IGhostFollow
        {
            void UpdatePositionFollow(int x, int y);
        }
    }
}

