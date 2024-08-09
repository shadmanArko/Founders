using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class DraggableWindowScript : MonoBehaviour, IDragHandler
    {
        public Canvas canvas;
 
        private RectTransform rectTransform;
 
        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
 
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}