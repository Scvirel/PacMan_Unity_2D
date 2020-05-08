using UnityEngine;
using WCTools;

namespace Game.View
{ 
    public interface IPacMan
    {
        void UpdatePosition(Vector2 position, float time);
    }

    // #########################################

    public class PacMan : MonoBehaviour, IPacMan
    {
        public SpriteRenderer pacManSprite;

        public IPacMan CloneMe(Transform parent, Vector2 position)
        {
            GameObject newObj = Instantiate(gameObject, parent);
            PacMan pacMan = newObj.GetComponent<PacMan>();
            pacMan.transform.localPosition = position;
            return pacMan;
        }

        // ===================================

        CoroutineInterpolator _positionInterp;

        void Awake()
        {
            _positionInterp = new CoroutineInterpolator(this);
        }

        // ========== IPacMan ================

        void IPacMan.UpdatePosition(Vector2 position, float time)
        {
            _positionInterp.Interpolate(transform.localPosition, position, time,
                (Vector2 pos) =>
                {
                    transform.localPosition = pos;
                });
        }
       
    }
}
