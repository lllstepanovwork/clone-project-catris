using UnityEngine;
using OleksiiStepanov.Game;
using TMPro;

namespace OleksiiStepanov.UI 
{
    public class WinPanel : BaseUIPanel
    {
        [Header("Content")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI msgText;

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
            UIPanelController.Instance.ClosePanel(UIPanelController.Instance.winPanel, true);
        }
    }
}

