using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts
{
    [CreateAssetMenu(fileName = "ElevationDescriptiveDataSo", menuName = "ScriptableObjects/ElevationDescriptiveDataSo", order = 0)]
    public class ElevationDescriptiveDataSo : ScriptableObject
    {
        public List<ElevationDescriptiveData> elevationDescriptiveDatas;

    }
}