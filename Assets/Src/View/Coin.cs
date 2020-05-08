using UnityEngine;

namespace Game.View
{
    public interface ICoin
    {
        void Hide();
        void Show();
    }

    public class Coin : MonoBehaviour, ICoin
    {
        
        public ICoin CloneMe(Transform parent, Vector3 position)
        {
            GameObject newObj = Instantiate(gameObject, parent);
            Coin coin = newObj.GetComponent<Coin>();
            coin.transform.localPosition = position;
            return coin;
        }


        public void Hide()
        {
            SpriteRenderer tempSpriteRenderer = GetComponent<SpriteRenderer>();
            Color tempColor = tempSpriteRenderer.color;
            tempColor.a = 0f;
            tempSpriteRenderer.color = tempColor;
        }

        public void Show()
        {
            SpriteRenderer tempSpriteRenderer = GetComponent<SpriteRenderer>();
            Color tempColor = tempSpriteRenderer.color;
            tempColor.a = 1f;
            tempSpriteRenderer.color = tempColor;
        }
    }
}