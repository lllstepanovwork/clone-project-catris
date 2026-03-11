using System;
using Catris.UI;
using UnityEngine;
using Zenject;

namespace Catris.Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private CatViewer catViewer;
        [SerializeField] private CatField catField;
        [SerializeField] private ScoreHolder scoreHolder;
        
        private UIPanelController _panelController;
        private CatQueue _catQueue;
        
        [Inject]
        public void Construct(UIPanelController panelController, CatQueue catQueue)
        {
            _panelController = panelController;
            _catQueue = catQueue;
        }
        
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

                gameResult.msgText = Constants.GameOverResultNewRecord;
            }
            else
            {
                gameResult.msgText = Constants.GameOverResultGreat;
            }

            _panelController.OpenPanel(_panelController.winPanel, true);
            _panelController.winPanel.Init(gameResult);
        }

        public void Restart() 
        {
            catViewer.Init();
            catField.ClearField();
            scoreHolder.ClearScore();

            _catQueue.ClearQueue();
            _catQueue.GenerateQueue();
        }
    }

    public class GameResult
    {
        public string msgText;
        public bool bestScore = false;
        public int score = 0;
    }
}
