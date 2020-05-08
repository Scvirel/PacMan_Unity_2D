using UnityEngine;

namespace Game.View
{ 
    public interface ICharactersFactory
    {
        IPacMan CreatePacMan(Transform parentTransform, Vector2 position);
        IGhost CreateGhostRandom(Transform parentTransform, Vector2 position);
        IGhost CreateGhostFollow(Transform parentTransform, Vector2 position);
    }

    // ##############################################

    public class CharactersFactory : MonoBehaviour, ICharactersFactory
    {
        [SerializeField]
        PacMan _pacManPrefab;

        [SerializeField]
        Ghost _ghostPrefabRandom;

        [SerializeField]
        Ghost _ghostPrefabFollow;

        // ========== ICharactersFactory ============

        IPacMan ICharactersFactory.CreatePacMan(Transform parentTransform, Vector2 position)
        { return _pacManPrefab.CloneMe(parentTransform, position); }

        IGhost ICharactersFactory.CreateGhostRandom(Transform parentTransform, Vector2 position)
        {
            return _ghostPrefabRandom.CloneMe(parentTransform,position);
        }
        IGhost ICharactersFactory.CreateGhostFollow(Transform parentTransform, Vector2 position)
        {
            return _ghostPrefabFollow.CloneMe(parentTransform, position);
        }
    }
}
