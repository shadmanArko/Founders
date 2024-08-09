using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.zzz_Testing.Recyclable_Scroll_VIew
{
    public class RecyclableScrollParent : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public GameObject content;
        public GameObject cardPrefab;
        
        public List<RecyclableScrollCard> cards;

        private void Awake()
        {
            cards = new List<RecyclableScrollCard>();
        }
    }
}
