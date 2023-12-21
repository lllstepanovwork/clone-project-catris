using OleksiiStepanov.UI;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformChangedEventTrigger : UIBehaviour
    {
        [SerializeField] private UIManager uiManager;
        protected override void OnRectTransformDimensionsChange()
        {
            uiManager.CalculateSafeAreaAndAspectRatio();
        }
    }
}
