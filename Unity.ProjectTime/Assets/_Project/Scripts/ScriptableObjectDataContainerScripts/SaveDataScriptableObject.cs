using ASP.NET.ProjectTime.DataContext;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject", order = 0)]
    public class SaveDataScriptableObject : ScriptableObject
    {
        [SerializeField] private Save save;

        public Save Save
        {
            get => save;
            set => save = value;
        }
    }
}