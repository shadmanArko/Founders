using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts
{
    [CreateAssetMenu(fileName = "FeatureDescriptiveDataSo", menuName = "ScriptableObjects/FeatureDescriptiveDataSo", order = 0)]
    public class FeatureDescriptiveDataSo : ScriptableObject
    {
        public List<FeatureDescriptiveData> featureDescriptiveDatas;
    }
}