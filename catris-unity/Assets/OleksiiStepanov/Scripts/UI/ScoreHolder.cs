using UnityEngine;
using OleksiiStepanov.Game;
using TMPro;
using DG.Tweening;

namespace OleksiiStepanov.UI 
{
    public class ScoreHolder : MonoBehaviour
    {
        [SerializeField] private RectTransform currentScoreTextRectTransform;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI scoreMsgText;
        [SerializeField] private TextMeshProUGUI currentScoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        public int BestScoreValue { get; private set; }
        public int CurrentScore { get; private set; }

        private int sum = 0;

        public void Init()
        {
            CatColumn.OnMerge += AddScore;
        }

        private void OnDestroy()
        {
            CatColumn.OnMerge -= AddScore;
        }

        private void AddScore(int scoreToAdd, int multiplyer)
        {
            sum = scoreToAdd * 2 * multiplyer;

            CurrentScore += sum;

            SetScoreMsg(sum);
            SetCurrentScore(CurrentScore);
        }

        private void SetScoreMsg(int value)
        {
            scoreMsgText.text = $"+{value}";

            scoreMsgText.DOFade(1, 0f);
            scoreMsgText.DOFade(0, 1f);
        }

        private void SetCurrentScore(int value) 
        {
            currentScoreText.text = $"{value}";

            var sequence = DOTween.Sequence();

            sequence
                .Append(currentScoreTextRectTransform.DOScale(1.2f, 0.5f))
                .Append(currentScoreTextRectTransform.DOScale(1f, 0.5f));
        }

        public void SetBestScore(int value)
        {
            BestScoreValue = value;
            bestScoreText.text = $"{value} <sprite name=score_icon>";
        }

        public void ClearScore()
        {
            CurrentScore = 0;
        }
    }
}