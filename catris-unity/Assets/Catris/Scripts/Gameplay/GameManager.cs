using System;
using OleksiiStepanov.UI;
using OleksiiStepanov.Utils;
using UnityEngine;

namespace OleksiiStepanov.Game
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] private CatViewer catViewer;
        [SerializeField] private CatField catField;
        [SerializeField] private ScoreHolder scoreHolder;
        
        public void Init(Action onSuccess = null)
        {
            catField.Init(OnFieldFull);
            catViewer.Init();

            scoreHolder.Init();
            
            onSuccess?.Invoke();
        }

        public void OnFieldFull()
        {
            GameResult gameResult = new GameResult();

            gameResult.score = scoreHolder.CurrentScore;

            if (scoreHolder.BestScoreValue < scoreHolder.CurrentScore)
            {
                scoreHolder.SetBestScore(scoreHolder.CurrentScore);

                gameResult.bestScore = true;

                gameResult.msgText = Constants.GAME_OVER_RESULT_NEW_RECORD;
            }
            else
            {
                gameResult.msgText = Constants.GAME_OVER_RESULT_GREAT;
            }

            UIPanelController.Instance.OpenPanel(UIPanelController.Instance.winPanel, true);
            UIPanelController.Instance.winPanel.Init(gameResult);
        }

        public void Restart() 
        {
            catViewer.Init();
            catField.ClearField();
            scoreHolder.ClearScore();

            CatQueue.Instance.ClearQueue();
            CatQueue.Instance.GenerateQueue();
        }
    }

    public class GameResult
    {
        public string msgText;
        public bool bestScore = false;
        public int score = 0;
    }
}
