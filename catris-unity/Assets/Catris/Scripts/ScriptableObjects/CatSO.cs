using UnityEngine;

namespace Catris.Game
{
    [CreateAssetMenu(fileName = "Cat", menuName = "Cat")]
    public class CatSO : ScriptableObject
    { 
        public int number;
        public Color color;
    }
}
