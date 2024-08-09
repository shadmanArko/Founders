using _Project.Scripts.Arko_s_Tooltip_System.Codes;
using _Project.Scripts.Static_Classes;
using _Project.Scripts.Tiles;
using UnityEngine;

namespace _Project.Scripts.Tooltip
{
    public class TileTooltip : MonoBehaviour
    {
        void Start()
        {
            var tile = GetComponent<TileInfo>().tile;
            TooltipTriggerInternal tooltipTriggerInternal = GetComponent<TooltipTriggerInternal>();
            tooltipTriggerInternal.content = $"{gameObject.name}\n" +
                                             $"{tile.Terrain.GetTerrainName()}\n" +
                                             $"{tile.ElevationType.GetElevationName()}\n{tile.Feature.GetFeatureName()}";

        }
    
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
