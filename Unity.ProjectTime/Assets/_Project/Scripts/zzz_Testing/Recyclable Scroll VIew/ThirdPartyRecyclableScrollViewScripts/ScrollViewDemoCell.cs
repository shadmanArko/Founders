using Third_Party.Recyclable_Scroll_Rect.Main.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewDemoCell : MonoBehaviour, ICell
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text popName;
    [SerializeField] private TMP_Text clanName;

    public void ConfigureCell(PopData popData)
    {
        image.sprite = popData.PopSprite;
        popName.text = popData.PopName;
        clanName.text = popData.ClanName;
    }
}
