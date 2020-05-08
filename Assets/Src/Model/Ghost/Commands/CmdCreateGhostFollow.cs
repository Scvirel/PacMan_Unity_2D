namespace Game.Model
{
    public partial class ModelGhost
    {
        class CmdCreateGhostFollow : ICommand
        {
            int _x;
            int _y;

            public CmdCreateGhostFollow(int x, int y)
            {
                _x = x;
                _y = y;
            }

            public void Exec(IContextWritable context)
            {
                context.CharactardsContainer.Add<IGhostWritableFollow>(new GhostFollow(_x, _y));
                context.EventManager.Get<IGhostEventsWritable>().CreateGhost(_x, _y);
            }
        }
    }
}