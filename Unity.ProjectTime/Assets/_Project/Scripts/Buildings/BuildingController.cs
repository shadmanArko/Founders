using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Buildings
{
    public class BuildingController : IBuildingController
    {
        private Building _building;
        private readonly BuildingView _buildingView;
        private string _tileId;
        private string _BuildingSize;

        public BuildingController(Building building, BuildingView buildingView, string tileId, string BuildingSize)
        {
            _building = building;
            _buildingView = buildingView;
            _tileId = tileId;
            _BuildingSize = BuildingSize;
        }

        public string Id => _building.Id;
        public Building Building => _building;

        public Vector3 Position
        {
            get => _buildingView.Position;
            set => _buildingView.Position = value;
        }

        public void Config()
        {
            
        }
    }
}
