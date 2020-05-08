using UnityEngine;

namespace Game.Model
{
    public partial class ModelPacMan
    {
        class CmdStopGame : ICommand
        {
            // ============== ICommand ================

            void ICommand.Exec(IContextWritable context)
            {
                context.EventManager.Get<IPacManEventsWritable>().StopGame();
            }
        }
    }
}
