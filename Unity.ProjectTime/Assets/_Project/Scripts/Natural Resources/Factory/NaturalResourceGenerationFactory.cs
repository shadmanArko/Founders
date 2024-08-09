using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.Static_Classes;
using ASP.NET.ProjectTime.Models;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Natural_Resources.Factory
{
    public class NaturalResourceGenerationFactory : IFactory<string, NaturalResource, Vector3, INaturalResourceController>
    {
        private readonly DiContainer _container;
        private readonly ResourcePrefabsContainerSo _resourcePrefabsContainerSo;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public NaturalResourceGenerationFactory(DiContainer container, ResourcePrefabsContainerSo resourcePrefabsContainerSo, SaveDataScriptableObject saveDataScriptableObject)
        {
            _container = container;
            _resourcePrefabsContainerSo = resourcePrefabsContainerSo;
            _saveDataScriptableObject = saveDataScriptableObject;
        }
        public INaturalResourceController Create(string tileId, NaturalResource naturalResource, Vector3 spawnPosition)
        {
            var resourceObject = _container.InstantiatePrefab(_resourcePrefabsContainerSo.naturalResourceIcon);
            var naturalResourceView = _container.InstantiateComponent<NaturalResourceView>(resourceObject);
            naturalResourceView.naturalResource = naturalResource;
            naturalResourceView.resourceSprite.sprite = naturalResource.GetResourceIcon();
            naturalResourceView.Position = spawnPosition;
            return _container.Instantiate<NaturalResourceController>(new object[]
                {naturalResource, naturalResourceView, spawnPosition});
        }
    }
}