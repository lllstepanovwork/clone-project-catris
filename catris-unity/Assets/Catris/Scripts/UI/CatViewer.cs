using UnityEngine;
using OleksiiStepanov.Game;

namespace OleksiiStepanov.UI 
{
    public class CatViewer : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private CatViewerElement catFront;
        [SerializeField] private CatViewerElement catBack;

        public const string ANIM_MOVE_TRIGGER = "Move";

        public void Init()
        {
            catFront.Init(CatQueue.Instance.GetCatSOInQueue(0));
            catBack.Init(CatQueue.Instance.GetCatSOInQueue(1));

            CatColumn.OnPlace += MoveCats;
        }

        private void OnDestroy()
        {
            CatColumn.OnPlace -= MoveCats;
        }

        public void UpdateFirstCat() 
        {
            catFront.Init(CatQueue.Instance.GetCatSOInQueue(1));
            catFront.transform.SetAsFirstSibling();
        }

        public void UpdateSecondCat()
        {
            catBack.Init(CatQueue.Instance.GetCatSOInQueue(1));
            catBack.transform.SetAsFirstSibling();
        }

        public void MoveCats()
        {
            anim.SetTrigger(ANIM_MOVE_TRIGGER);
        }
    }
}
