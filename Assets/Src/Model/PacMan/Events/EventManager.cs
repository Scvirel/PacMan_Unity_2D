namespace Game.Model
{
    public delegate void dCreatePacMan(int x, int y);
    public delegate void dUpdatePacManPosition(int x, int y);
    public delegate void dStopGame();
    public delegate void dVinGame();

    public interface IPacManEvents
    {
        event dCreatePacMan OnCreatePacMan;
        event dUpdatePacManPosition OnUpdatePacManPosition;
        event dStopGame OnStopGame;
        event dVinGame OnVinGame;
    }

    public interface IPacManEventsWritable
    {
        void CreatePacMan(int x, int y);
        void UpdatePacManPosition(int x, int y);
        void StopGame();
        void VinGame();
    }

    // #############################################

    class PacManEvents : IPacManEvents, IPacManEventsWritable
    {
        // ========= IPacManEvents ================

        public event dCreatePacMan OnCreatePacMan;
        public event dUpdatePacManPosition OnUpdatePacManPosition;
        public event dStopGame OnStopGame;
        public event dVinGame OnVinGame;
        // ========= IPacManEventsWritable =========

        void IPacManEventsWritable.CreatePacMan(int x, int y)
        { OnCreatePacMan?.Invoke(x, y); }

        void IPacManEventsWritable.UpdatePacManPosition(int x, int y)
        { OnUpdatePacManPosition?.Invoke(x, y); }

        void IPacManEventsWritable.StopGame()
        {
            OnStopGame?.Invoke();
        }
        void IPacManEventsWritable.VinGame()
        {
            OnVinGame?.Invoke();
        }
    }
}
