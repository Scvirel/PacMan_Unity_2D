using System.Collections;
using UnityEngine;
using Game.Model;
using Game.Misc;
using Game.View;
using UnityEngine.SceneManagement;
using System;
using TMPro;

namespace Game
{
    public class GameMediator : MonoBehaviour
    {
        const float ITERATION_TIME = 0.1f;

        [SerializeField]
        VisualManager _visualManager;
        [SerializeField]
        GameObject _winPanel;
        [SerializeField]
        GameObject _losePanel;
        [SerializeField]
        GameObject timeField;

        int coinCount = 0;
        (ICoin coin, byte hasCoin)[,] coinFields;
        ICherry cherry;
        (int width, int height) fieldSize;
        (int Px, int Py) pacManPos;
        (int Rx, int Ry) ghostRandomPos;
        (int Fx, int Fy) ghostFollowPos;
        IModelPacMan _modelPacMan = new ModelPacMan();
        IModelGhost ghostsRandom = new ModelGhost();
        IModelGhost ghostsFollow = new ModelGhost();
        eDirection currentDirection;
        SpriteRenderer pacManSpriteRenderer;
        bool CR_Runing = false;
        DateTime start = DateTime.Now;
        // ====================================

        IVisualManager VisualManager => _visualManager;
        // ====================================
        IEnumerator Start()
        {
            fieldSize = _visualManager.GetFieldSize();
            coinCount = fieldSize.width * fieldSize.height - 2;
            _visualManager.Game = true;
            VisualManager.SpawnCherry(fieldSize);
            cherry = _visualManager.GetCherry();
            VisualManager.SpawnCoins(fieldSize, cherry);
            coinFields = _visualManager.GetCoinField();
            StartInit();

            pacManSpriteRenderer = _visualManager.GetPacManSpriteRenderer();

            while (_visualManager.Game)
            {
                ghostRandomPos = _visualManager.GetRandomPosition();
                ghostFollowPos = _visualManager.GetFollowPosition();
                pacManPos = _visualManager.GetPacManPosition();
                calcCoins(coinFields);
                _modelPacMan.Update(currentDirection, coinFields, cherry, ghostRandomPos, ghostFollowPos);

                if (coinCount > 0)
                {
                    if (!cherry.Power)
                    {
                        ghostsFollow.UpdateFollow(pacManPos, _modelPacMan);
                        ghostsRandom.UpdateRandom(_modelPacMan);
                    }
                    else
                    {
                        if (!CR_Runing)
                        {
                            StartCoroutine(PowerTime());
                        }
                        ghostsFollow.UpdateRunAwayFollow(pacManPos, _modelPacMan);
                        ghostsRandom.UpdateRunAwayRandom(pacManPos, _modelPacMan);
                    }
                }
                else
                {
                    _visualManager.Game = false;
                }
                

                yield return new WaitForSeconds(ITERATION_TIME);
            }

            if (coinCount > 0)
            {
                _losePanel.SetActive(true);
            }
            else
            {
                TextMeshProUGUI levelTime = timeField.GetComponent<TextMeshProUGUI>();
                TimeSpan lvlTime = DateTime.Now - start;
                levelTime.text = $"{lvlTime.Minutes} min {lvlTime.Seconds} sec";
                _winPanel.SetActive(true);
                _visualManager.Game = false;
            } 
        }
        private void calcCoins((ICoin coin, byte hasCoin)[,] coinFields)
        {
            int counter=0;
            foreach (var item in coinFields)
            {
                if (item.hasCoin == 1)
                {
                    counter++;
                }
            }
            coinCount = counter;
        }
        IEnumerator PowerTime()
        {
            CR_Runing = true;
            cherry.Active = false;
            cherry.SetNewPosition(coinFields);
            yield return new WaitForSeconds(5f);
            cherry.Active = true;
            cherry.Show();
            cherry.Power = false;
            _visualManager.SetCherryPosition(ref cherry);
            CR_Runing = false;
        }
        private void OnGUI()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                SetAction(Event.current.keyCode);
            }
            if (!_visualManager.Game && Event.current.type == EventType.MouseDown)
            {
                SceneManager.LoadScene("Level");
            }
        }
        private void StartInit()
        {
            VisualManager.InitGhostRandom(ghostsRandom.EventManager, ITERATION_TIME);
            VisualManager.InitGhostFollow(ghostsFollow.EventManager, ITERATION_TIME);
            VisualManager.InitPacMan(_modelPacMan.EventManager, ITERATION_TIME);
            _modelPacMan.Init();
            ghostsRandom.InitRandom();
            ghostsFollow.InitFollow();

        }
        private void SetAction(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.LeftArrow:
                    {
                        pacManSpriteRenderer.flipX = true;
                        currentDirection = eDirection.LEFT; 
                    } break;
                case KeyCode.RightArrow: 
                    {
                        pacManSpriteRenderer.flipX = false;
                        currentDirection = eDirection.RIGHT;
                    } break;
                case KeyCode.UpArrow: { currentDirection = eDirection.UP; } break;
                case KeyCode.DownArrow: { currentDirection = eDirection.DOWN; } break;
                case KeyCode.L: { _visualManager.Game = false; coinCount = 0; }break;
            }
        } 
    }
}
