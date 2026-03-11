using Catris.Game;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Catris.UI 
{
    public class CatViewerElement : MonoBehaviour
    {
        public int Number { get; private set; }

        [SerializeField] private Image catImage;
        [SerializeField] private TextMeshProUGUI numberText;

        public void Init(CatSO cat)
        {
            Number = cat.number;
            numberText.text = Number.ToString();
            
            catImage.color = cat.color;
        }
    }
}

