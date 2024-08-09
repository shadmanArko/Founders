using System.Collections.Generic;
using Third_Party.Recyclable_Scroll_Rect.Main.Scripts;
using Third_Party.Recyclable_Scroll_Rect.Main.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScrollViewDataSource : MonoBehaviour, IRecyclableScrollRectDataSource
{
    private List<PopData> _popData;
    
    [SerializeField] RecyclableScrollRect recyclableScrollRect;
    [SerializeField] private int dataListCount;

    [SerializeField] private List<Sprite> spriteList;

    private void Awake()
    {
        _popData = new List<PopData>();
        InitData();
        recyclableScrollRect.DataSource = this;
    }

    [ContextMenu("Generate Data")]
    private void InitData()
    {
        _popData.Clear();
        for (var i = 0; i < dataListCount; i++)
        {
            var popData = new PopData($"Pop {Random.Range(1, 9999)}", "Clan" + i,
                spriteList[Random.Range(0, spriteList.Count)]);
            _popData.Add(popData);
        }
    }
    
    public int GetItemCount() => _popData.Count;

    public void SetCell(ICell cell, int index)
    {
        var item = cell as ScrollViewDemoCell;
        if (item != null) item.ConfigureCell(_popData[index]);
    }
    
    [ContextMenu("Reverse Data Reload")]
    public void ReloadData()
    {
        recyclableScrollRect.ReloadData(this);
    }
}

public struct PopData
{
    public readonly string PopName;
    public readonly string ClanName;
    public readonly Sprite PopSprite;

    public PopData(string popName, string clanName, Sprite popSprite)
    {
        PopName = popName;
        ClanName = clanName;
        PopSprite = popSprite;
    }
}


