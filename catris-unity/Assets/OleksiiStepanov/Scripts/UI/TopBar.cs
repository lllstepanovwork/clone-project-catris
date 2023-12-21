using UnityEngine;

namespace OleksiiStepanov.UI
{
    public class TopBar : MonoBehaviour
    {
        public void OnPauseButtonClick()
        {
            UIPanelController.Instance.OpenPanel(UIPanelController.Instance.winPanel, true);
        }
    }
}
