using System;
using System.Collections.Generic;
using Catris.Gameplay;
using Zenject;

namespace Catris.Game
{
    public class CatQueue
    {
        private readonly List<int> catQueue = new List<int>();
        private Cats _cats;
        
        private int listSize = 100;

        [Inject]
        public void Construct(Cats cats)
        {
            _cats = cats;
        }

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
            CatSO catSo = _cats.catList[catQueue[0]];
            catQueue.RemoveAt(0);

            if (catQueue.Count < 10)
            {
                GenerateQueue();
            }

            return catSo; 
        }

        public CatSO GetCatSO(int index)
        {
            return _cats.catList[index];
        }

        public CatSO GetCatSOInQueue(int index)
        {
            return _cats.catList[catQueue[index]];
        }

        public void ClearQueue() 
        {
            catQueue.Clear();
        }
    }
}

