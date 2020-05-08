using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    public interface ICherry
    {
        int X { get; set; }
        int Y { get; set; }
        bool Power { get; set; }
        bool Active { get; set; }
        void Hide();
        void Show();
        void SetNewPosition((ICoin coin, byte hasCoin)[,] coinField);
    }

    public class Cherry : MonoBehaviour, ICherry
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Power { get; set; }
        public bool Active { get; set; }

        public ICherry CloneMe(Transform parent, Vector2 position)
        {
            GameObject newObj = Instantiate(gameObject, parent);
            Cherry obj = newObj.GetComponent<Cherry>();
            obj.transform.localPosition = position;
            return obj;
        }

        public void SetNewPosition((ICoin coin, byte hasCoin)[,] coinField)
        {
            List<(int x, int y)> freeCoinPositions = new List<(int x, int y)>();
            for (int xpos = 0; xpos < coinField.GetLength(0); xpos++)
            {
                for (int ypos = 0; ypos < coinField.GetLength(1); ypos++)
                {
                    if (coinField[xpos, ypos].hasCoin == 0)
                    {
                        freeCoinPositions.Add((xpos, ypos));
                    }
                }
            }
            int rnd  = Random.Range(0, freeCoinPositions.Count-1);
            X = freeCoinPositions[rnd].x;
            Y = freeCoinPositions[rnd].y;
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