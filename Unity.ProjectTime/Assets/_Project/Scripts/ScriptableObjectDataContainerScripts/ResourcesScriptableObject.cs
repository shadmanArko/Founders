using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "ResourcesScriptableObject", menuName = "ScriptableObjects/ResourcesScriptableObject", order = 0)]
    public class ResourcesScriptableObject : ScriptableObject
    {
        public List<NaturalResource> resources;
    }
}