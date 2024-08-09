using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class TooltipUI : MonoBehaviour
{
   public TextMeshProUGUI headerField;
   public TextMeshProUGUI contentField;
   public LayoutElement layoutElement;
   public int characterWrapLimit;
   public float yPositionModifier;

   private RectTransform _rectTransform;

   private void Awake()
   {
      _rectTransform = GetComponent<RectTransform>();
   }

   private void Update()
   {
      Vector2 position = Input.mousePosition;

      float pivotX = position.x / Screen.width;
      float pivotY = position.y / Screen.height;
      float yAddition;
      pivotX = pivotX > 0.5 ? 1 : 0;

      if (pivotY > 0.5)
      {
         pivotY = 1;
         yAddition = yPositionModifier;
      }
      else
      {
         pivotY = 0;
         yAddition = 0;
      }
      
      _rectTransform.pivot = new Vector2(pivotX, pivotY);
      
      transform.position = new Vector3(position.x, (position.y + yAddition), 0);
   }

   public void ChangeTooltipUiSize()
   {
      int headerLength = headerField.text.Length;
      int contentLength = contentField.text.Length;

      layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
   }

   public void SetText(string content)
   {
      contentField.text = content;
      
      ChangeTooltipUiSize();
   }

   private void OnEnable()
   {
      ChangeTooltipUiSize();
   }

   public void Show(string content)
   {
      SetText(content);
      gameObject.SetActive(true);
   }

   public void Hide()
   {
      gameObject.SetActive(false);
   }
}
