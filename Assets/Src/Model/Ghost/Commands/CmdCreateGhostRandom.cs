
namespace Game.Model
{
    public partial class ModelGhost
    {
        class CmdCreateGhostRandom : ICommand
        {
            int _x;
            int _y;

            public CmdCreateGhostRandom(int x, int y)
            {
                _x = x;
                _y = y;
            }

            public void Exec(IContextWritable context)
            {
                context.CharactardsContainer.Add<IGhostWritableRandom>(new GhostRandom(_x, _y));
                context.EventManager.Get<IGhostEventsWritable>().CreateGhost(_x, _y);
            }
        }
    }
}

