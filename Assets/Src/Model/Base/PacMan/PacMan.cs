namespace Game.Model
{
    public partial class ModelBase
    {
        protected interface IPacMan
        {
            int X { get; set; }
            int Y { get; set; }
        }

        protected interface IPacManWritable : IPacMan
        {
            void UpdatePosition(int x, int y);
        }
    }
}