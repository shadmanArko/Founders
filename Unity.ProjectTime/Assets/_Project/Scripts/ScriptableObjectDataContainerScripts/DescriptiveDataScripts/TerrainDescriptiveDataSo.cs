using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts
{
    [CreateAssetMenu(fileName = "TerrainDescriptiveDataSo", menuName = "ScriptableObjects/TerrainDescriptiveDataSo", order = 0)]
    public class TerrainDescriptiveDataSo : ScriptableObject
    {
        public List<TerrainDescriptiveData> terrainDescriptiveDatas;
    }
}