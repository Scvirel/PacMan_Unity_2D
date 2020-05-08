using System.Collections.Generic;
using UnityEngine;
namespace Game.View
{
    public interface IVisualManager
    {
        void SpawnCherry((int width, int height) fieldSize);
        void SpawnCoins((int width, int height) fieldSize, ICherry cherry);
        void InitPacMan(Model.IEventManager eventsManager, float iterationTime);
        void InitGhostRandom(Model.IEventManager eventManager, float iterationTime);
        void InitGhostFollow(Model.IEventManager eventManager, float iterationTime);
    }

    // #################################################

    public class VisualManager : MonoBehaviour, IVisualManager
    {
        [SerializeField]
        CharactersFactory _charactersFactory;
        [SerializeField]
        ItemFactory _itemFactory;
        [SerializeField]
        PositionManager _positionManager;

        (ICoin coin, byte hasCoin)[,] CoinField;
        float _iterationTime;
        IPacMan _pacMan;
        (int Px, int Py) pacManPos;
        IGhost _ghostRandom;
        (int Rx, int Ry) ghostRandomPos;
        IGhost _ghostFollow;
        (int Fx, int Fy) ghostFollowPos;
        ICherry cherry;

        public bool Game { get; set; }
        public bool IsVin { get; set; }

        // =============================================
        IItemFactory ItemFactory => _itemFactory;
        ICharactersFactory CharactersFactory => _charactersFactory;
        IPositionManager PositionManager => _positionManager;

        // ============ IVisualManager =================
        void IVisualManager.SpawnCoins((int width, int height) fieldSize,ICherry cherry)
        {
            CoinField = new (ICoin, byte)[fieldSize.width, fieldSize.height];
            for (int y = 0; y < fieldSize.height; y++)
            {
                for (int x = 0; x < fieldSize.width; x++)
                {
                    CoinField[x, y].coin = ItemFactory.CreateCoin(null, PositionManager.GetPosition(x, y));
                    if (x == 0 && y == 0 || x == cherry.X && y == cherry.Y)
                    {
                        CoinField[x, y].coin.Hide();
                        CoinField[x, y].hasCoin = 0;
                        continue;
                    }
                    CoinField[x, y].hasCoin = 1;
                }
            }
        }
        void IVisualManager.SpawnCherry((int width,int height)fieldSize)
        {
            (int x, int y) cherryPosition;
            cherryPosition.x = Random.Range(1, fieldSize.width-1);
            cherryPosition.y = Random.Range(1, fieldSize.height-1);
            cherry = ItemFactory.CreateCherry(null,PositionManager.GetPosition(cherryPosition.x, cherryPosition.y));
            cherry.X = cherryPosition.x;
            cherry.Y = cherryPosition.y;
            cherry.Active = true;
        }
        void IVisualManager.InitPacMan(Model.IEventManager eventsManager, float iterationTime)
        {
            _iterationTime = iterationTime;
            eventsManager.Get<Model.IPacManEvents>().OnCreatePacMan += OnCreatePacMan;
            eventsManager.Get<Model.IPacManEvents>().OnUpdatePacManPosition += OnUpdatePacManPosition;
            eventsManager.Get<Model.IPacManEvents>().OnStopGame += OnStopGame;
            eventsManager.Get<Model.IPacManEvents>().OnVinGame += OnVinGame;
        }
        void IVisualManager.InitGhostRandom(Model.IEventManager eventManager, float iterationTime)
        {
            _iterationTime = iterationTime;
            eventManager.Get<Model.IGhostEvents>().OncreateGhost += OnCreateGhostRandom;
            eventManager.Get<Model.IGhostEvents>().OnUpdateRandomGhostPosition += OnUpdateRandomGhostPosition;
            eventManager.Get<Model.IGhostEvents>().OnUpdateRunAwayGhostPosition += OnUpdateRunAwayGhostPositionRandom;
            eventManager.Get<Model.IGhostEvents>().OnStopGame += OnStopGame;
        }
        void IVisualManager.InitGhostFollow(Model.IEventManager eventManager, float iterationTime)
        {
            _iterationTime = iterationTime;
            eventManager.Get<Model.IGhostEvents>().OncreateGhost += OnCreateGhostFollow;
            eventManager.Get<Model.IGhostEvents>().OnUpdateFollowGhostPosition += OnUpdateFollowGhostPosition;
            eventManager.Get<Model.IGhostEvents>().OnUpdateRunAwayGhostPosition += OnUpdateRunAwayGhostPositionFollow;
            eventManager.Get<Model.IGhostEvents>().OnStopGame += OnStopGame;
        }
        // =============================================
        public void SetCherryPosition(ref ICherry cherry)
        {
            Vector2 position = PositionManager.GetPosition(cherry.X,cherry.Y);
            (cherry as Cherry).transform.localPosition = position;
            this.cherry = cherry; 
        }
        public void SetPacManPosition()
        {
            Vector2 position = PositionManager.GetPosition(0,0);
            (_pacMan as PacMan).transform.localPosition = position;
        }
        public void SetGhostPositionRandom()
        {
            Vector2 position = PositionManager.GetPosition(Random.Range(5, 15), Random.Range(5, 11));
            (_ghostRandom as Ghost).transform.localPosition = position;
        }
        public void SetGhostPositionFollow()
        {
            Vector2 position = PositionManager.GetPosition(Random.Range(5, 15), Random.Range(5, 11));
            (_ghostFollow as Ghost).transform.localPosition = position;
        }

        public (int width, int height) GetFieldSize() 
        {
            return PositionManager.GetWidthHeight();
        }
        public (ICoin coin, byte hasCoin)[,] GetCoinField()
        {
            return CoinField;
        }
        public ICherry GetCherry()
        {
            return cherry;
        }
        public SpriteRenderer GetPacManSpriteRenderer()
        {
            return (_pacMan as PacMan).pacManSprite;
        }
        public (int Rx, int Ry) GetRandomPosition()
        {
            return ghostRandomPos;
        }
        public (int Fx, int Fy) GetFollowPosition()
        {
            return ghostFollowPos;
        }
        public (int Px, int Py) GetPacManPosition()
        {
            return pacManPos;
        }

        void OnCreatePacMan(int x, int y)
        {
            Vector2 position = PositionManager.GetPosition(x, y);
            _pacMan = CharactersFactory.CreatePacMan(null, position);
        }
        void OnUpdatePacManPosition(int x, int y)
        {
            pacManPos.Px = x;
            pacManPos.Py = y;
            Vector2 position = PositionManager.GetPosition(x, y);
            _pacMan.UpdatePosition(position, _iterationTime);
        }
        void OnStopGame()
        {
            Game = false;
        }
        void OnVinGame()
        {
            Game = false;
            IsVin = true;
        }

        void OnCreateGhostRandom(int x, int y)
        {
            Vector2 position = PositionManager.GetPosition(x, y);
            _ghostRandom = CharactersFactory.CreateGhostRandom(null, position);
        }
        void OnUpdateRandomGhostPosition(int x, int y)
        {
            ghostRandomPos.Rx = x;
            ghostRandomPos.Ry = y;
            Vector2 position = PositionManager.GetPosition(x, y);
            _ghostRandom.UpdatePosition(position, _iterationTime);
        }
        void OnUpdateRunAwayGhostPositionRandom(int x, int y)
        {
            ghostRandomPos.Rx = x;
            ghostRandomPos.Ry = y;
            Vector2 position = PositionManager.GetPosition(x, y);
            _ghostRandom.UpdatePosition(position, _iterationTime);
        }

        void OnCreateGhostFollow(int x, int y)
        {
            Vector2 position = PositionManager.GetPosition(x, y);
            _ghostFollow = CharactersFactory.CreateGhostFollow(null, position);
        }
        void OnUpdateFollowGhostPosition(int x, int y)
        {
            ghostFollowPos.Fx = x;
            ghostFollowPos.Fy = y;
            Vector2 position = PositionManager.GetPosition(x, y);
            _ghostFollow.UpdatePosition(position, _iterationTime);
        }
        void OnUpdateRunAwayGhostPositionFollow(int x, int y)
        {
            ghostFollowPos.Fx = x;
            ghostFollowPos.Fy = y;
            Vector2 position = PositionManager.GetPosition(x, y);
            _ghostFollow.UpdatePosition(position, _iterationTime);
        }
    }
}