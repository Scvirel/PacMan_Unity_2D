namespace Game.Model
{
    public partial class ModelPacMan
    {
        protected class PacMan : IPacManWritable
        {
            int _x;
            int _y;

            // =============================

            public PacMan(int x, int y)
            {
                _x = x;
                _y = y;
            }

            // ======= IPacMan =============

            int IPacMan.X { get { return _x; }set { _x = value; } }
            int IPacMan.Y { get { return _y; } set { _y = value; } }

            // ====== ICharacterWritable ==

            void IPacManWritable.UpdatePosition(int x, int y)
            {
                _x = x;
                _y = y;
            }
           
        }
    }
}