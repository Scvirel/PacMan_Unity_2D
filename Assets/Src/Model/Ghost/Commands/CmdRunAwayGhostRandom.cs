
using Game.Misc;

namespace Game.Model
{
    public partial class ModelGhost
    {
        class CmdRunAwayGhostRandom : ICommand
        {
            (int x, int y) _pacManPosition;
            IModelPacMan pacModel;
            public CmdRunAwayGhostRandom((int, int) pacManPosition,IModelPacMan model)
            {
                _pacManPosition = pacManPosition;
                pacModel = model;
            }
            private bool CheckXLeft(IGhostWritableRandom ghost, IContextWritable context)
            {
                ghost.Direction = eDirection.LEFT;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionRandom(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateRandomPosition(nextPositon.x, nextPositon.y);
                    return true;
                }
                return false;
            }
            private bool CheckXRight(IGhostWritableRandom ghost, IContextWritable context)
            {
                ghost.Direction = eDirection.RIGHT;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionRandom(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateRandomPosition(nextPositon.x, nextPositon.y);
                    return true;
                }
                return false;
            }
            private bool CheckYUp(IGhostWritableRandom ghost, IContextWritable context)
            {

                ghost.Direction = eDirection.UP;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionRandom(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateRandomPosition(nextPositon.x, nextPositon.y);
                    return true;
                }
                return false;

            }
            private bool CheckYDown(IGhostWritableRandom ghost, IContextWritable context)
            {
                ghost.Direction = eDirection.DOWN;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionRandom(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateRandomPosition(nextPositon.x, nextPositon.y);
                    return true;
                }

                return false;
            }

            public void Exec(IContextWritable context)
            {
                IGhostWritableRandom ghost = context.CharactardsContainer.Get<IGhostWritableRandom>();
                if (_pacManPosition.x == ghost.X && _pacManPosition.y == ghost.Y)
                { 
                    pacModel.StopGame(); return;
                }

                if (_pacManPosition.x < ghost.X)
                {
                    if (CheckXRight(ghost, context)) return;
                    if (ghost.Previous != eDirection.DOWN && CheckYUp(ghost, context)) return;
                    if (CheckYDown(ghost, context)) return;
                    CheckXLeft(ghost, context);
                }
                if (_pacManPosition.x > ghost.X)
                {
                    if (ghost.Previous != eDirection.RIGHT && CheckXLeft(ghost, context)) return;
                    if (ghost.Previous != eDirection.DOWN && CheckYUp(ghost, context)) return;
                    if (CheckYDown(ghost, context)) return;
                    CheckXRight(ghost, context);
                }
                if (_pacManPosition.x == ghost.X)
                {
                    if (_pacManPosition.y < ghost.Y)
                    {
                        if (ghost.Previous != eDirection.DOWN && CheckYUp(ghost, context)) return;
                        if (ghost.Previous != eDirection.LEFT && CheckXRight(ghost, context)) return;
                        if (CheckXLeft(ghost, context)) return;
                        CheckYDown(ghost, context);
                    }
                    else
                    {
                        if (ghost.Previous != eDirection.UP && CheckYDown(ghost, context)) return;
                        if (ghost.Previous != eDirection.LEFT && CheckXRight(ghost, context)) return;
                        if (CheckXLeft(ghost, context)) return;
                        CheckYUp(ghost, context);
                    }
                }
            }
        }
    }
}
