using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts
{
    [CreateAssetMenu(fileName = "ResourcesDescriptiveDataScriptableObject", menuName = "ScriptableObjects/ResourcesDescriptiveDataScriptableObject", order = 0)]
    public class ResourcesDescriptiveDataObject : ScriptableObject
    {
        public List<NaturalResourceDescriptiveData> naturalResourceDescriptiveDatas;
    }
}