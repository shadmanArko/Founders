using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "SubcontinentsContainer", menuName = "ScriptableObjects/SubcontinentsContainer", order = 0)]
    public class SubcontinentsContainer : ScriptableObject
    {
        public List<Subcontinent> subcontinents;
    }
}