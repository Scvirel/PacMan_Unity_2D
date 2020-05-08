using Game.Misc;
using System.Collections.Generic;

namespace Game.Model
{
    public partial class ModelBase
    {
        protected interface IField
        {
            int Width { get; }
            int Height { get; }
            List<Square> Blocks { get; }
            bool IsCanMove(int x, int y, eDirection direction);
        }

        // ##############################################

        class Field : IField
        {
            IField IField { get { return this; } }

            List<Square> IField.Blocks => new List<Square> {
                #region UpBlock

                new Square(3,1,eDirection.UP),
                new Square(4,1,eDirection.UP),
                new Square(5,1,eDirection.UP),
                new Square(6,1,eDirection.UP),
                new Square(9,1,eDirection.UP),
                new Square(10,1,eDirection.UP),
                new Square(11,1,eDirection.UP),
                new Square(12,1,eDirection.UP),
                new Square(2,4,eDirection.UP),
                new Square(5,4,eDirection.UP),
                new Square(6,4,eDirection.UP),
                new Square(9,4,eDirection.UP),
                new Square(10,4,eDirection.UP),
                new Square(13,4,eDirection.UP),
                new Square(2,6,eDirection.UP),
                new Square(3,6,eDirection.UP),
                new Square(12,6,eDirection.UP),
                new Square(13,6,eDirection.UP),
                new Square(6,7,eDirection.UP),
                new Square(5,10,eDirection.UP),
                new Square(10,10,eDirection.UP),

                #endregion
                #region LeftBlocks

                new Square(3,8,eDirection.LEFT),
                new Square(4,4,eDirection.LEFT),
                new Square(5,6,eDirection.LEFT),
                new Square(5,7,eDirection.LEFT),
                new Square(4,9,eDirection.LEFT),
                new Square(12,3,eDirection.LEFT),
                new Square(5,3,eDirection.LEFT),
                new Square(13,8,eDirection.LEFT),
                new Square(13,9,eDirection.LEFT),
                new Square(13,10,eDirection.LEFT),
                new Square(11,6,eDirection.LEFT),
                new Square(11,7,eDirection.LEFT),
                new Square(8,4,eDirection.LEFT),
                new Square(8,5,eDirection.LEFT),
                new Square(9,8,eDirection.LEFT),
                new Square(9,9,eDirection.LEFT),

                #endregion
                #region RightBlocks

                new Square(3,3,eDirection.RIGHT),
                new Square(10,3,eDirection.RIGHT),
                new Square(7,4,eDirection.RIGHT),
                new Square(7,5,eDirection.RIGHT),
                new Square(4,6,eDirection.RIGHT),
                new Square(4,7,eDirection.RIGHT),
                new Square(10,6,eDirection.RIGHT),
                new Square(10,7,eDirection.RIGHT),
                new Square(11,4,eDirection.RIGHT),
                new Square(2,8,eDirection.RIGHT),
                new Square(2,9,eDirection.RIGHT),
                new Square(2,10,eDirection.RIGHT),
                new Square(5,8,eDirection.RIGHT),
                new Square(5,9,eDirection.RIGHT),
                new Square(11,9,eDirection.RIGHT),
                new Square(12,8,eDirection.RIGHT),

                #endregion
                #region DownBlocks

                new Square(2,5,eDirection.DOWN),
                new Square(3,5,eDirection.DOWN),
                new Square(5,5,eDirection.DOWN),
                new Square(6,5,eDirection.DOWN),
                new Square(9,5,eDirection.DOWN),
                new Square(10,5,eDirection.DOWN),
                new Square(12,5,eDirection.DOWN),
                new Square(13,5,eDirection.DOWN),
                new Square(3,2,eDirection.DOWN),
                new Square(6,2,eDirection.DOWN),
                new Square(9,2,eDirection.DOWN),
                new Square(12,2,eDirection.DOWN),
                new Square(4,11,eDirection.DOWN),
                new Square(5,11,eDirection.DOWN),
                new Square(2,5,eDirection.DOWN),
                new Square(3,5,eDirection.DOWN),
                new Square(10,11,eDirection.DOWN),
                new Square(11,11,eDirection.DOWN),
                new Square(6,10,eDirection.DOWN),
                new Square(8,10,eDirection.DOWN),
                new Square(7,8,eDirection.DOWN),
                
                #endregion
                #region 2SideBlocks
                new Square(4,2,eDirection.RIGHT,eDirection.DOWN),
                new Square(3,4,eDirection.RIGHT,eDirection.UP),
                new Square(4,3,eDirection.RIGHT,eDirection.LEFT),
                new Square(10,2,eDirection.RIGHT,eDirection.DOWN),
                new Square(5,2,eDirection.LEFT,eDirection.DOWN),
                new Square(11,2,eDirection.LEFT,eDirection.DOWN),
                new Square(12,4,eDirection.LEFT,eDirection.UP),
                new Square(11,3,eDirection.RIGHT,eDirection.LEFT),
                new Square(7,6,eDirection.RIGHT,eDirection.UP),
                new Square(8,6,eDirection.LEFT,eDirection.UP),
                new Square(7,7,eDirection.DOWN,eDirection.UP),
                new Square(8,7,eDirection.DOWN,eDirection.UP),
                new Square(2,7,eDirection.RIGHT,eDirection.DOWN),
                new Square(3,7,eDirection.LEFT,eDirection.DOWN),
                new Square(12,7,eDirection.RIGHT,eDirection.DOWN),
                new Square(13,7,eDirection.LEFT,eDirection.DOWN),
                new Square(3,9,eDirection.RIGHT,eDirection.LEFT),
                new Square(3,10,eDirection.RIGHT,eDirection.LEFT),
                new Square(12,9,eDirection.RIGHT,eDirection.LEFT),
                new Square(12,10,eDirection.RIGHT,eDirection.LEFT),
                new Square(4,10,eDirection.UP,eDirection.LEFT),
                new Square(11,10,eDirection.RIGHT,eDirection.UP),
                new Square(6,9,eDirection.UP,eDirection.LEFT),
                new Square(6,8,eDirection.DOWN,eDirection.LEFT),
                new Square(8,8,eDirection.DOWN,eDirection.RIGHT),
                new Square(8,9,eDirection.RIGHT,eDirection.UP),
	            #endregion
            };

            bool IsOutOfRange(int x, int y)
            { return x < 0 || y < 0 || x >= IField.Width || y >= IField.Height; }

            bool IsColide(int xPos, int yPos,eDirection direction)
            {
                Square temp = IField.Blocks.Find(result => result.x == xPos && result.y == yPos);

                return temp != null && temp.blockedDirections.Exists(result => result == direction);
            }
            // ============ IField ======================

            int IField.Width => 16;
            int IField.Height => 12;

            bool IField.IsCanMove(int x, int y, eDirection direction)
            {
                (int x, int y) nextPositon = Direction.GetNextPosition(x, y, direction);
                return !IsOutOfRange(nextPositon.x, nextPositon.y) && !IsColide(x,y,direction);
            }
        }
    }
}