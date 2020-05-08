using UnityEngine;
using WCTools;

namespace Game.View
{
    public interface IGhost
    {
        void UpdatePosition(Vector2 position, float time);
    }

    public class Ghost : MonoBehaviour, IGhost
    {
        public SpriteRenderer ghostSpriteRenderer;

        public IGhost CloneMe(Transform parent, Vector2 position)
        {
            GameObject newObj = Instantiate(gameObject, parent);
            Ghost ghost = newObj.GetComponent<Ghost>();
            ghost.transform.localPosition = position;
            return ghost;
        }

        CoroutineInterpolator _positionInterp;

        private void Awake()
        {
            _positionInterp = new CoroutineInterpolator(this);
        }

        void IGhost.UpdatePosition(Vector2 position, float time)
        {
            _positionInterp.Interpolate(transform.localPosition, position, time,
                (Vector2 pos) => { transform.localPosition = pos; }
                );
        }
    }
}


