using UnityEngine;
using DG.Tweening;
using TMPro;

namespace OleksiiStepanov.Game
{
	public class ComboCounter : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI comboText;
		[SerializeField] private RectTransform rectTransform;

		public void Show(int mergeCount, Vector2 targetPosition)
		{
			comboText.DOFade(1, 0f);

			comboText.text = $"x{mergeCount}";
			rectTransform.anchoredPosition = targetPosition;

			comboText.DOFade(0, 1f);
		}
	}
}
