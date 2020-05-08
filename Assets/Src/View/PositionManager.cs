using UnityEngine;

namespace Game.View
{
    public interface IPositionManager
    {
        Vector2 GetPosition(int x, int y);
        (int width, int height) GetWidthHeight();
    }

    // #########################################

    public class PositionManager : MonoBehaviour, IPositionManager
    {
        [SerializeField]
        Transform _leftBottomMarker;
        [SerializeField]
        Transform _rightTopMarker;

        [Space]
        [SerializeField]
        int _width;
        [SerializeField]
        int _height;

        // =========== IPositionManager ========

        Vector2 IPositionManager.GetPosition(int x, int y)
        {
            float resultX = Mathf.Lerp(_leftBottomMarker.localPosition.x, _rightTopMarker.localPosition.x, x / (float)(_width - 1));
            float resultY = Mathf.Lerp(_leftBottomMarker.localPosition.y, _rightTopMarker.localPosition.y, y / (float)(_height - 1));
            return new Vector2(resultX, resultY);
        }
        (int width, int height) IPositionManager.GetWidthHeight()
        {
            return (_width,_height);
        }
    }
}