using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts.Culture
{
    [CreateAssetMenu(fileName = "PlayableCultureContainerSo", menuName = "ScriptableObjects/PlayableCultureContainerSo", order = 0)]
    public class PlayableCultureContainerSo : ScriptableObject
    {
        public List<PlayableCulture> playableCultures;
    }
}