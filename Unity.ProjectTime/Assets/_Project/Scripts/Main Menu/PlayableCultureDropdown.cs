using _Project.Scripts.ScriptableObjectDataContainerScripts.Culture;
using _Project.Scripts.Static_Classes;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Main_Menu
{
    public class PlayableCultureDropdown : MonoBehaviour
    {
        public TMP_Dropdown playableCultureDropdown;

        private void Start()
        {
            // playableCultureContainerSo =
            //     Resources.Load<PlayableCultureContainerSo>("ScriptableObjects/PlayableCulture/playableCultureContainerSo");
            // PopulateDropDown();
        }

        public void PopulateDropDown(PlayableCultureContainerSo playableCultureContainerSo)
        {
            playableCultureDropdown.options.Clear();
            foreach (var playableCulture in playableCultureContainerSo.playableCultures)           
            {
                playableCultureDropdown.options.Add(new TMP_Dropdown.OptionData(playableCulture.GetPlayableCultureName()));
            }
            playableCultureDropdown.captionText.text = playableCultureDropdown.options[0].text;
        }
    }
}
