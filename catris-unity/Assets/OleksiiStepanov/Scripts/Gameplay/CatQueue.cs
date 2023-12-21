using UnityEngine;
using System;
using System.Collections.Generic;
using OleksiiStepanov.Utils;

namespace OleksiiStepanov.Game
{
    public class CatQueue : SingletonBehaviour<CatQueue>
    {
        [SerializeField] private List<CatSO> catsSO = new List<CatSO>();

        public List<int> catQueue = new List<int>();

        private int listSize = 100;

        public void Init(Action onSuccess = null)
        {
            GenerateQueue();

            onSuccess?.Invoke();
        }

        public void GenerateQueue() 
        {
            int randNum;

            for (int i = 0; i <= listSize; i++)
            {
                randNum = UnityEngine.Random.Range(0, 5);
                
                catQueue.Add(randNum);
            }
        }

        public CatSO DequeueCat()
        {
            CatSO catSO = catsSO[catQueue[0]];
            catQueue.RemoveAt(0);

            if (catQueue.Count < 10)
            {
                GenerateQueue();
            }

            return catSO; 
        }

        public CatSO GetCatSO(int index)
        {
            return catsSO[index];
        }

        public CatSO GetCatSOInQueue(int index)
        {
            return catsSO[catQueue[index]];
        }

        public void ClearQueue() 
        {
            catQueue.Clear();
        }
    }
}

