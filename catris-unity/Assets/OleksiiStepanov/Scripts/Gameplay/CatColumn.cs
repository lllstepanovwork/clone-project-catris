using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using OleksiiStepanov.UI;

namespace OleksiiStepanov.Game
{
    public class CatColumn : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private Transform catContainerTransform;
        [SerializeField] private RectTransform spawningPointRectTransform;
        [SerializeField] private RectTransform dropPointRectTransform;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private RectTransform particlesRectTransform;
        [SerializeField] private ComboCounter comboCounter;

        [Header("Cats")]
        [SerializeField] private CatHolder catPrefab;

        [SerializeField] private List<CatHolder> availableCatHolders;
        private List<CatHolder> currentCatHolders = new List<CatHolder>();

        private Vector2 dropPointStartPosition;

        private int mergeCount = 0;
        private bool merging = false;

        public bool IsFull { get; private set; }

        public Action OnColumnFull;

        public static Action OnPlace;
        public static Action<int,int> OnMerge;

        public void Init(Action OnColumnFull = null)
        {
            dropPointStartPosition = dropPointRectTransform.anchoredPosition;

            this.OnColumnFull = OnColumnFull;
        }

        public void PlaceCat() 
        {
            if (!IsFull)
            {
                if (!merging)
                {
                    merging = true;

                    CatHolder catHolder = GetAvailableCat();
                    catHolder.gameObject.SetActive(true);
                    catHolder.Init(CatQueue.Instance.DequeueCat());
                    catHolder.rectTransform.anchoredPosition = spawningPointRectTransform.anchoredPosition;
                    catHolder.MoveToTarget(dropPointRectTransform.anchoredPosition, () =>
                    {
                        TryMerge();
                    });

                    currentCatHolders.Add(catHolder);

                    UpdateDropPointPosition();

                    OnPlace?.Invoke();
                }
            }
        }

        private CatHolder GetAvailableCat()
        {
            for (int i = 0; i < availableCatHolders.Count; i++)
            {
                if (availableCatHolders[i].available)
                {
                    return availableCatHolders[i];
                }
            }

            return null;
        }

        private void UpdateDropPointPosition()
        {
            dropPointRectTransform.anchoredPosition = new Vector2(dropPointRectTransform.anchoredPosition.x, dropPointStartPosition.y + ((dropPointRectTransform.sizeDelta.y - 10) * currentCatHolders.Count));
        }

        private void TryMerge()
        {
            if (currentCatHolders.Count > 1)
            {
                if (currentCatHolders[currentCatHolders.Count - 2].number == currentCatHolders[currentCatHolders.Count - 1].number)
                {
                    mergeCount++;

                    MergeCats();

                    OnMerge(currentCatHolders[currentCatHolders.Count - 2].number, mergeCount);
                }
                else
                {
                    merging = false;

                    mergeCount = 0;

                    if (currentCatHolders.Count == 10)
                    {
                        IsFull = true;

                        OnColumnFull?.Invoke();
                    }
                }
            }
            else
            {
                merging = false;

                mergeCount = 0;
            }
        }

        private void MergeCats()
        {
            currentCatHolders[currentCatHolders.Count - 2].MoveToMerge(currentCatHolders[currentCatHolders.Count - 1].rectTransform.anchoredPosition,
                () =>
                {
                    ShowComboCounter();
                    ShowParticles();

                    if (currentCatHolders[currentCatHolders.Count - 2].number > 10)
                    {
                        ConsumeCat(currentCatHolders.Count - 2);
                    }
                    else
                    {
                        currentCatHolders[currentCatHolders.Count - 2].Init(CatQueue.Instance.GetCatSO(currentCatHolders[currentCatHolders.Count - 2].number));
                    }

                    ConsumeCat(currentCatHolders.Count - 1);
                },
                () =>
                {
                    UpdateDropPointPosition();

                    TryMerge();
                });
        }

        private void ConsumeCat(int index) 
        {
            currentCatHolders[index].gameObject.SetActive(false);
            currentCatHolders[index].available = true;
            currentCatHolders.RemoveAt(index);
        }

        private void ShowComboCounter() 
        {
            if (mergeCount > 1)
            {
                comboCounter.Show(mergeCount, currentCatHolders[currentCatHolders.Count - 1].rectTransform.anchoredPosition);
            }
        }

        private void ShowParticles()
        {
            particles.gameObject.SetActive(true);
            particles.Play();
            particlesRectTransform.anchoredPosition = currentCatHolders[currentCatHolders.Count - 1].rectTransform.anchoredPosition;
        }

        public void Clear() 
        {
            foreach (CatHolder catHolder in availableCatHolders) 
            {
                catHolder.available = true;
                catHolder.gameObject.SetActive(false);
            }

            dropPointRectTransform.anchoredPosition = dropPointStartPosition;

            IsFull = false;

            currentCatHolders.Clear();
        }
    }
}

