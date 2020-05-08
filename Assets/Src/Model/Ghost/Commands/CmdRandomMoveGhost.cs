using Game.Misc;

namespace Game.Model
{
    public partial class ModelGhost
    {
        class CmdRandomMoveGhost : ICommand
        {
            IModelPacMan _pacModel;
            public CmdRandomMoveGhost(IModelPacMan model)
            {
                _pacModel = model;
            }

            public void Exec(IContextWritable context)
            {
                IGhostWritableRandom ghost = context.CharactardsContainer.Get<IGhostWritableRandom>();

                ghost.Direction = (eDirection)Direction.GetRandomDirection();
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);

                if (ghost.CanMove)
                {
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    if (nextPositon.x == ghost.X && nextPositon.y == ghost.Y)
                    {
                        _pacModel.StopGame();return;
                    }
                    ghost.UpdatePositionRandom(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateRandomPosition(nextPositon.x, nextPositon.y);
                }  
            }
        }
    }
}