using Game.Misc;
using Game.View;


namespace Game.Model
{
    public interface IModelPacMan
    {
        IEventManager EventManager { get; }

        void Init();
        void Update(eDirection direction, (ICoin coin, byte hasCoin)[,] coinFields, ICherry cherry, params (int, int)[] positionghosts);
        void StopGame();
    }

    // ##################################################

    public partial class ModelPacMan : ModelBase, IModelPacMan
    {
        protected override void RegisterEvents(IEventManagerInternal eventManager)
        { eventManager.Register<IPacManEvents, IPacManEventsWritable>(new PacManEvents()); }

        // ============== IModelPacMan =================

        IEventManager IModelPacMan.EventManager => EventManager;

        void IModelPacMan.Init()
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdCreatePacMan(0, 0));
                });
        }

        void IModelPacMan.Update(eDirection direction, (ICoin coin, byte hasCoin)[,] coinFields,ICherry cherry,params (int,int)[] positionGhosts)
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdMovePacMan(direction, coinFields,cherry, positionGhosts));
                });
        }

        void IModelPacMan.StopGame()
        {
            CreateAndExecuteTurn(
                (ITurn turn) =>
                {
                    turn.Push(new CmdStopGame());
                });
        }
    }
}