
using Game.Misc;

namespace Game.Model
{
    public partial class ModelGhost
    {
        class CmdRunAwayGhostFollow : ICommand
        {
            (int x, int y) _pacManPosition;
            IModelPacMan pacModel;
            public CmdRunAwayGhostFollow((int, int) pacManPosition, IModelPacMan model)
            {
                _pacManPosition = pacManPosition;
                pacModel = model;
            }

            private bool CheckXLeft(IGhostWritableFollow ghost, IContextWritable context)
            {
                ghost.Direction = eDirection.LEFT;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionFollow(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateFollowPosition(nextPositon.x, nextPositon.y);
                    return true;
                }
                return false;
            }
            private bool CheckXRight(IGhostWritableFollow ghost, IContextWritable context)
            {
                ghost.Direction = eDirection.RIGHT;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionFollow(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateFollowPosition(nextPositon.x, nextPositon.y);
                    return true;
                }
                return false;
            }
            private bool CheckYUp(IGhostWritableFollow ghost, IContextWritable context)
            {

                ghost.Direction = eDirection.UP;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionFollow(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateFollowPosition(nextPositon.x, nextPositon.y);
                    return true;
                }
                return false;

            }
            private bool CheckYDown(IGhostWritableFollow ghost, IContextWritable context)
            {
                ghost.Direction = eDirection.DOWN;
                ghost.CanMove = context.Field.IsCanMove(ghost.X, ghost.Y, ghost.Direction);
                if (ghost.CanMove)
                {
                    ghost.Previous = ghost.Direction;
                    (int x, int y) nextPositon = Direction.GetNextPosition(ghost.X, ghost.Y, ghost.Direction);
                    ghost.UpdatePositionFollow(nextPositon.x, nextPositon.y);
                    context.EventManager.Get<IGhostEventsWritable>().UpdateFollowPosition(nextPositon.x, nextPositon.y);
                    return true;
                }

                return false;
            }

            public void Exec(IContextWritable context)
            {
                IGhostWritableFollow ghost = context.CharactardsContainer.Get<IGhostWritableFollow>();
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
