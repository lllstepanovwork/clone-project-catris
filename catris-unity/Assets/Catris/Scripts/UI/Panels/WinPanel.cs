using Catris.Game;
using UnityEngine;
using TMPro;
using Zenject;

namespace Catris.UI 
{
    public class WinPanel : BaseUIPanel
    {
        [Header("Content")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI msgText;

        private UIPanelController _panelController;
        
        [Inject]
        public void Construct(UIPanelController panelController)
        {
            _panelController = panelController;
        }
        
        public void Init(GameResult gameResult)
        {
            if (gameResult.bestScore)
            {
                scoreText.text = $"{gameResult.score} <sprite name=best_score_icon>";
            }
            else
            {
                scoreText.text = $"{gameResult.score}";
            }
            
            msgText.text = gameResult.msgText;
        }

        public void OnRestartButtonClick()
        {
            _panelController.ClosePanel(_panelController.winPanel, true);
        }
    }
}

