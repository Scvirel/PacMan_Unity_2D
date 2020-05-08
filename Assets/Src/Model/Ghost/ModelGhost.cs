
using UnityEngine;

namespace Game.Model 
{
    public interface IModelGhost
    {
        IEventManager EventManager { get; }

        void InitRandom();
        void InitFollow();
        void UpdateRandom(IModelPacMan model);
        void UpdateFollow((int, int) pacManPosition, IModelPacMan model);
        void UpdateRunAwayFollow((int, int) pacManPosition, IModelPacMan model);
        void UpdateRunAwayRandom((int, int) pacManPosition, IModelPacMan model);
    }


    public partial class ModelGhost : ModelBase,IModelGhost
    {
        protected override void RegisterEvents(IEventManagerInternal eventManager)
        {
            eventManager.Register<IGhostEvents,IGhostEventsWritable>(new GhostEvents());
        }

        IEventManager IModelGhost.EventManager => EventManager;

        void IModelGhost.InitRandom()
        {
            CreateAndExecuteTurn(
                (ITurn turn) => 
                { 
                    turn.Push(new CmdCreateGhostRandom(Random.Range(5,15), Random.Range(5, 11))); 
                });
        }
        void IModelGhost.InitFollow()
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdCreateGhostFollow(Random.Range(5, 15), Random.Range(5, 11)));
                });
        }

        public void UpdateRandom(IModelPacMan model)
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdRandomMoveGhost(model));
                });
        }

        public void UpdateFollow((int,int)pacManPosition,IModelPacMan model)
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdFollowMoveGhost(pacManPosition, model));
                });
        }

        public void UpdateRunAwayFollow((int, int) pacManPosition, IModelPacMan model)
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                   turn.Push(new CmdRunAwayGhostFollow(pacManPosition, model));
                });
        }
        public void UpdateRunAwayRandom((int, int) pacManPosition, IModelPacMan model)
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdRunAwayGhostRandom(pacManPosition, model));
                });
        }
    }
}

