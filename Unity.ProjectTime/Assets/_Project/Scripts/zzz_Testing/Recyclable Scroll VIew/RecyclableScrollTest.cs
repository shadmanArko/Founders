using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.zzz_Testing.Recyclable_Scroll_VIew
{
    public class RecyclableScrollTest : MonoBehaviour
    {
        public List<int> data;
        
        [SerializeField] private RecyclableScrollParent parentRecyclableScroll;

        private int _topCardCounter;
        private int _bottomCardCounter;

        private void Start()
        {
            parentRecyclableScroll.scrollRect.onValueChanged.AddListener(OnValueChange);
            //parentRecyclableScroll.scrollRect.onValueChanged.AddListener();
            previousContentPos = parentRecyclableScroll.scrollRect.content.anchoredPosition;
            var thisContent = parentRecyclableScroll.scrollRect.content;
            Debug.LogWarning($"{GetContentBounds(thisContent).max.x},{GetContentBounds(thisContent).max.y} {GetContentBounds(thisContent).min.x},{GetContentBounds(thisContent).min.y}");
            
            GenerateData();
            GenerateCards();
        }

        [SerializeField] private Vector2 manualAnchorPosition;
        [ContextMenu("Set Anchor")]
        public void SetAnchor()
        {
            parentRecyclableScroll.scrollRect.content.anchoredPosition = manualAnchorPosition;
            Debug.Log("Current Anchor Position: "+parentRecyclableScroll.scrollRect.content.anchoredPosition);
            Debug.Log(
                $" Anchor Points {parentRecyclableScroll.scrollRect.content.anchorMax} , {parentRecyclableScroll.scrollRect.content.anchorMin}");
        }
        
        public void OnValueChange(Vector2 pos)
        {
            var thisContent = parentRecyclableScroll.scrollRect.content;
            Debug.LogWarning($"{GetContentBounds(thisContent).max.x},{GetContentBounds(thisContent).max.y} {GetContentBounds(thisContent).min.x},{GetContentBounds(thisContent).min.y}");
            var content = parentRecyclableScroll.scrollRect.content;
            Debug.Log("OnValueChanged "+pos);
            Debug.LogWarning($"Anchored Position: {content.anchoredPosition}");
            var mouseDelta = Input.mouseScrollDelta.y;
            // if (mouseDelta > 0)
            //     ScrollUp();
            // else if(mouseDelta < 0)
            //     ScrollDown();
            
            //if(content.anchoredPosition < 0)
        }

        [SerializeField] private Vector2 previousContentPos;
        private void ChangeContentPosition(Vector2 pos)
        {
            var content = parentRecyclableScroll.scrollRect.content;

            content.anchoredPosition = previousContentPos + pos;
        }

        #region Generate Methods

        private void GenerateData()
        {
            for (var i = 0; i < 100; i++)
                data.Add(i);
        }
        
        private void GenerateCards()
        {
            var content = parentRecyclableScroll.scrollRect.content;
            var cardPrefab = parentRecyclableScroll.cardPrefab;
            for (var i = 0; i < 15; i++)
            {
                var card = Instantiate(cardPrefab, content.transform, true);
                var cardInfo = card.GetComponent<RecyclableScrollCard>();
                CreateNewCard(cardInfo, data[i], i);
                parentRecyclableScroll.cards.Add(cardInfo);
            }

            _topCardCounter = 0;
            _bottomCardCounter = parentRecyclableScroll.cards.Count;
        }
        
        void CreateNewCard(RecyclableScrollCard cardInfo,int tempData, int count)
        {
            cardInfo.index = count;
            cardInfo.text.text = tempData.ToString();
        }

        #endregion

        private void ScrollUp()
        {
            var scrollRect = parentRecyclableScroll.scrollRect;
        
            if(_topCardCounter <= 0) return;
            var content = scrollRect.content;
            var bottomMost = content.GetChild(content.childCount -1);
            Debug.Log("Scrolling Up");
            if(bottomMost == null) return;
            _topCardCounter--;
            _bottomCardCounter--;
            MoveCardToTop(bottomMost);
        }

        private void MoveCardToTop(Transform bottomMost)
        {
            bottomMost.SetAsFirstSibling();

            var card = bottomMost.GetComponent<RecyclableScrollCard>();
            card.text.text = data[_topCardCounter].ToString();
        }
        
        private void ScrollDown()
        {
            var scrollRect = parentRecyclableScroll.scrollRect;
            
            if(_bottomCardCounter >= data.Count) return;
            var content = scrollRect.content;
            var topMost = content.GetChild(0);
            Debug.Log("Scrolling Down");
            if (topMost == null)
            {
                Debug.LogError("Scroll Down came out null");
                return;
            }
            MoveCardToBottom(topMost);
        }

        private void MoveCardToBottom(Transform topMost)
        {
            topMost.SetAsLastSibling();
            
            var card = topMost.GetComponent<RecyclableScrollCard>();
            card.text.text = data[_bottomCardCounter].ToString();
            _topCardCounter+=1;
            _bottomCardCounter+=1;
        }
        

        #region Utilities

        private Rect GetContentBounds(RectTransform rectTransform)
        {
            //var scrollRect = parentRecyclableScroll.scrollRect;
            //var rectTransform = scrollRect.viewport;
            var contentCorners = new Vector3[4];
            rectTransform.GetWorldCorners(contentCorners);
        
            var min = contentCorners[0];
            var max = contentCorners[2];

            return new Rect(min, max - min);
        }

        #endregion
    }
}
