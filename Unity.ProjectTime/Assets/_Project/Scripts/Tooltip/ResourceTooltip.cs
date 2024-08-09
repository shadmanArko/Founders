using _Project.Scripts.Arko_s_Tooltip_System.Codes;
using _Project.Scripts.Natural_Resources;
using _Project.Scripts.Static_Classes;
using UnityEngine;

namespace _Project.Scripts.Tooltip
{
    public class ResourceTooltip : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

            GetComponent<TooltipTriggerInternal>().content = GetComponentInParent<NaturalResourceView>().naturalResource.GetResourceName();
        }

    
    }
}
