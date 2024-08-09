using System;
using _Project.Scripts.Buildings;

namespace _Project.Scripts.Game_Actions
{
    public static class BuildingActions
    {
        public static Action<string> OnBuildingCreated;
        public static Action<string> OnSelectedBuilding;
    }
}