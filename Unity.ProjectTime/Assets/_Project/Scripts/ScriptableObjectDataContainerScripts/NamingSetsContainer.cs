using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "NamingSetsContainer", menuName = "ScriptableObjects/NamingSetsContainer", order = 0)]
    public class NamingSetsContainer : ScriptableObject
    {
        public List<NamingSet> namingSets;
    }
}