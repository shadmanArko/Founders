using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Natural_Resources
{
    public class NaturalResourceController : INaturalResourceController
    {
        private NaturalResource _naturalResource;
        private readonly NaturalResourceView _naturalResourceView;
        private Vector3 _naturalResourceSpawnPosition;

        public NaturalResourceController(NaturalResource naturalResource, NaturalResourceView naturalResourceView, Vector3 naturalResourceSpawnPosition)
        {
            _naturalResource = naturalResource;
            _naturalResourceView = naturalResourceView;
            _naturalResourceSpawnPosition = naturalResourceSpawnPosition;
        }


        public string Id => _naturalResource.Id;
        
        public Vector3 Position {
            get => _naturalResourceView.Position;
            set => _naturalResourceView.Position = value;
        }

        public NaturalResource NaturalResource => _naturalResource;
        public void Config()
        {
        }
    }
}