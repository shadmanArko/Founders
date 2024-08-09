using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts
{
    [CreateAssetMenu(fileName = "PlayableCultureDescriptiveDataSo", menuName = "ScriptableObjects/PlayableCultureDescriptiveDataSo", order = 0)]
    public class PlayableCultureDescriptiveDataSo : ScriptableObject
    {
        public List<PlayableCultureDescriptiveData> playableCultureDescriptiveDatas;
    }
}