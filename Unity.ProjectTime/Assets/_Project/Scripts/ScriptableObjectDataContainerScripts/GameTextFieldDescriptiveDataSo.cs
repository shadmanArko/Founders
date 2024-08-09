using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "GameTextFieldDescriptiveDataSo", menuName = "ScriptableObjects/GameTextFieldDescriptiveDataSo", order = 0)]
    public class GameTextFieldDescriptiveDataSo : ScriptableObject
    {
        public List<GameTextFieldDescriptiveData> gameTextFieldDescriptiveDatas;
    }
}