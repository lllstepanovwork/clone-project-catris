using Catris.Game;
using UnityEngine;
using Zenject;

namespace Catris.UI 
{
    public class CatViewer : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private CatViewerElement catFront;
        [SerializeField] private CatViewerElement catBack;

        private const string AnimMoveTrigger = "Move";

        private CatQueue _catQueue;
        
        [Inject]
        public void Construct(CatQueue catQueue)
        {
            _catQueue = catQueue;
        }
        
        public void Init()
        {
            catFront.Init(_catQueue.GetCatSOInQueue(0));
            catBack.Init(_catQueue.GetCatSOInQueue(1));

            CatColumn.OnPlace += MoveCats;
        }

        private void OnDestroy()
        {
            CatColumn.OnPlace -= MoveCats;
        }

        public void UpdateFirstCat() 
        {
            catFront.Init(_catQueue.GetCatSOInQueue(1));
            catFront.transform.SetAsFirstSibling();
        }

        public void UpdateSecondCat()
        {
            catBack.Init(_catQueue.GetCatSOInQueue(1));
            catBack.transform.SetAsFirstSibling();
        }

        public void MoveCats()
        {
            anim.SetTrigger(AnimMoveTrigger);
        }
    }
}
