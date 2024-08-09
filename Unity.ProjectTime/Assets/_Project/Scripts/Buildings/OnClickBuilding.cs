using System;
using _Project.Scripts.Game_Actions;
using UnityEngine;

namespace _Project.Scripts.Buildings
{
    public class OnClickBuilding : MonoBehaviour
    {
        public string buildingId;

        private void OnMouseUpAsButton()
        {
            BuildingActions.OnSelectedBuilding?.Invoke(buildingId);
        }
    }
}