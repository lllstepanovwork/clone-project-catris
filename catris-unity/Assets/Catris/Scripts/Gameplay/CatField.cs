using UnityEngine;
using System.Collections.Generic;
using System;

namespace OleksiiStepanov.Game
{
    public class CatField : MonoBehaviour
    {
        [SerializeField]
        private List<CatColumn> catColumns = new List<CatColumn>();

        private Action OnFieldFull;

        public void Init(Action OnFieldFull)
        {
            foreach (CatColumn catColumn in catColumns)
            {
                catColumn.Init(CheckGameOver);
            }

            this.OnFieldFull = OnFieldFull;
        }

        public void CheckGameOver() 
        {
            int fullCounter = 0;

            for (int i = 0; i < catColumns.Count;i++) 
            {
                if (catColumns[i].IsFull)
                {
                    fullCounter++;
                }
            }

            if (fullCounter == catColumns.Count) 
            {
                OnFieldFull?.Invoke();
            }
        }

        public void ClearField() 
        {
            for (int i = 0; i < catColumns.Count; i++)
            {
                catColumns[i].Clear();
            }
        }
    }
}
