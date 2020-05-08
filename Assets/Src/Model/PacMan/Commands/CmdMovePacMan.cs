using Game.Misc;
using Game.View;



namespace Game.Model
{
    public partial class ModelPacMan
    {
        class CmdMovePacMan : ICommand
        {
            eDirection _direction;
            (ICoin coin, byte hasCoin)[,] _coinFields;
            ICherry _cherry;
            // ========================================
            (int x, int y) positionRandom;
            (int x, int y) positionFollow;
            public CmdMovePacMan(eDirection direction)
            {
                _direction = direction;
            }
            public CmdMovePacMan(eDirection direction, (ICoin coin, byte hasCoin)[,] coinFields, ICherry cherry, params (int, int)[] positionGhosts)
            {
                _direction = direction;
                _coinFields = coinFields;
                _cherry = cherry;
                positionRandom = positionGhosts[0];
                positionFollow = positionGhosts[1];
            }

            private bool IsOnCoin(int x, int y)
            {
                return _coinFields[x, y].hasCoin == 1;
            }

            private bool IsOnCherry(int x, int y)
            {
                return _cherry.X == x && _cherry.Y == y;
            }


            // ============== ICommand ================

            void ICommand.Exec(IContextWritable context)
            {
                IPacManWritable pacMan = context.CharactardsContainer.Get<IPacManWritable>();


                bool isCanMove = context.Field.IsCanMove(pacMan.X, pacMan.Y, _direction);


                if (isCanMove)
                {
                    (int x, int y) nextPositon = Direction.GetNextPosition(pacMan.X, pacMan.Y, _direction);
                    pacMan.UpdatePosition(nextPositon.x, nextPositon.y);
                    if (nextPositon.x == positionFollow.x && nextPositon.y == positionFollow.y || pacMan.X == positionRandom.x && pacMan.Y == positionRandom.y)
                    {
                        context.EventManager.Get<IPacManEventsWritable>().StopGame();
                    }
                    else
                    {
                        if (IsOnCoin(pacMan.X, pacMan.Y))
                        {
                            _coinFields[pacMan.X, pacMan.Y].coin.Hide();
                            _coinFields[pacMan.X, pacMan.Y].hasCoin = 0;
                        }
                        if (IsOnCherry(pacMan.X, pacMan.Y) && _cherry.Active)
                        {
                            _cherry.Power = true;
                            _cherry.Hide();
                        }
                        context.EventManager.Get<IPacManEventsWritable>().UpdatePacManPosition(nextPositon.x, nextPositon.y);
                    }
                }
            }
        }
    }
}