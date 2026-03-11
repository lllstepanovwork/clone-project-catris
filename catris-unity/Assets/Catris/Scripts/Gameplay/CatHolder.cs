using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

namespace OleksiiStepanov.Game
{
    public class CatHolder : MonoBehaviour
    {
        [Header("Content")]
        public int number;
        public bool available = true;
        public RectTransform rectTransform;

        [SerializeField] private Image catImage;
        [SerializeField] private TextMeshProUGUI numberText;

        [Header("Movement")]
        [SerializeField] private float moveDuration;
        [SerializeField] private float mergeDuration;

        [Header("Eyes Animation")]
        [SerializeField] private RectTransform leftEyeRectTransform;
        [SerializeField] private RectTransform rightEyeRectTransform;

        private bool eyeAnimationEnabled = false;

        public void Init(CatSO cat)
        {
            available = false;
            
            number = cat.number;
            numberText.text = number.ToString();
            catImage.color = cat.color;

            Sequence sequence = DOTween.Sequence();

            if (!eyeAnimationEnabled)
            {
                eyeAnimationEnabled = true;

                sequence
                .AppendInterval(6f)
                .Append(leftEyeRectTransform.DOScale(new Vector3(0.4f, 0, 0), 0.2f))
                .Join(rightEyeRectTransform.DOScale(new Vector3(0.4f, 0, 0), 0.2f))
                .Append(leftEyeRectTransform.DOScale(new Vector3(1, 1, 1), 0.2f))
                .Join(rightEyeRectTransform.DOScale(new Vector3(1, 1, 1), 0.2f))
                .SetLoops(-1);
            }
        }

        public void MoveToTarget(Vector3 target, Action onComplete = null) 
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(rectTransform.DOAnchorPos(target, moveDuration))
                .AppendCallback(() => {
                    onComplete?.Invoke();
                });
        }

        public void MoveToMerge(Vector3 target, Action onMerge = null, Action onComplete = null)
        {
            Vector2 startPosition = rectTransform.anchoredPosition;

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(rectTransform.DOAnchorPos(target, mergeDuration))
                .AppendCallback(() => {
                    onMerge?.Invoke();
                })
                .Append(rectTransform.DOAnchorPos(startPosition, mergeDuration))
                .AppendCallback(()=> {
                    onComplete?.Invoke();
                });
        }
    }
}
