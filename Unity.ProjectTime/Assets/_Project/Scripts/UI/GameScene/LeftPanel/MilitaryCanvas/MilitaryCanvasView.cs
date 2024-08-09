using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.GameScene.LeftPanel.MilitaryCanvas
{
    public class MilitaryCanvasView : MonoBehaviour
    {
        public TMP_Text regimentStatsText;
        public TMP_Text regimentName;
        public TMP_Dropdown regimentRoleSelectorDropDown;
        public List<RegimentPopSlot> regimentPopSlots;  //pops in each regiment

        public TMP_Dropdown weaponMaterialSelectorDropdown;

        public List<RegimentSlot> regimentSlots;    //Regiments locked or unlocked
        
    }
}