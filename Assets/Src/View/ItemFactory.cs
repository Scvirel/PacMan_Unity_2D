using UnityEngine;

namespace Game.View
{
    public interface IItemFactory
    {
        ICoin CreateCoin(Transform parentTransform, Vector2 position);
        ICherry CreateCherry(Transform parentTransform, Vector2 position);
    }

    public class ItemFactory : MonoBehaviour, IItemFactory
    {
        [SerializeField]
        Cherry _cherryPrefab;
        [SerializeField]
        Coin _coinPrefab;


        public ICoin CreateCoin(Transform parentTransform, Vector2 position)
        {
            return _coinPrefab.CloneMe(parentTransform, position);
        }

        public ICherry CreateCherry(Transform parentTransform, Vector2 position)
        {
            return _cherryPrefab.CloneMe(parentTransform, position);
        }
    }
}