namespace Game.Model
{
    public delegate void dCreateChost(int x, int y);
    public delegate void dUpdateRandomPosition(int x, int y);
    public delegate void dUpdateFollowPosition(int x, int y);
    public delegate void dUpdateRunAwayPosition(int x, int y);
    public delegate void dStopgame();

    public interface IGhostEvents
    {
        event dCreateChost OncreateGhost;
        event dUpdateRandomPosition OnUpdateRandomGhostPosition;
        event dUpdateFollowPosition OnUpdateFollowGhostPosition;
        event dUpdateRunAwayPosition OnUpdateRunAwayGhostPosition;
        event dStopgame OnStopGame;
    }

    public interface IGhostEventsWritable
    {
        void CreateGhost(int x, int y);
        void UpdateRandomPosition(int x, int y);
        void UpdateFollowPosition(int x, int y);
        void UpdateRunAwayPosition(int x, int y);
        void StopGame();
    }

    class GhostEvents : IGhostEvents, IGhostEventsWritable
    {
        public event dCreateChost OncreateGhost;
        public event dUpdateRandomPosition OnUpdateRandomGhostPosition;
        public event dUpdateFollowPosition OnUpdateFollowGhostPosition;
        public event dUpdateRunAwayPosition OnUpdateRunAwayGhostPosition;
        public event dStopgame OnStopGame;

        public void CreateGhost(int x, int y)
        {
            OncreateGhost?.Invoke(x, y);
        }

        public void UpdateFollowPosition(int x, int y)
        {
            OnUpdateFollowGhostPosition?.Invoke(x, y);
        }

        public void UpdateRandomPosition(int x, int y)
        {
            OnUpdateRandomGhostPosition?.Invoke(x, y);
        }

        public void UpdateRunAwayPosition(int x, int y)
        {
            OnUpdateRunAwayGhostPosition?.Invoke(x, y);
        }
        public void StopGame()
        {
            OnStopGame?.Invoke();
        }
    }
}

