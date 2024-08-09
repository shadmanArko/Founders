using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.DataContext;
using ASP.NET.ProjectTime.Services;
using SandBox.Arko.Scripts;
using SandBox.Arko.Scripts.UnitOfWork;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.zzz_Testing.Tilemap_Testing__Depricated_
{
    [CreateAssetMenu(fileName = "TileMapInstaller", menuName = "Installers/TileMapInstaller")]
    public class TileMapInstaller : ScriptableObjectInstaller<TileMapInstaller>
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private ScriptableObject tileDataContainer;
        // [SerializeField] private TilesScriptableObject tilesScriptableObject;
        [FormerlySerializedAs("playerDataScriptableObject")] [SerializeField] private SaveDataScriptableObject saveDataScriptableObject;
    
        public override void InstallBindings()
        {
            Container.Bind<TileMapGenerator>().AsSingle();
            Container.BindInstance(tilePrefab).WhenInjectedInto<TileMapGenerator>();
        
        
            Container.Bind<TileDataContainer>().FromScriptableObject(tileDataContainer).AsSingle();
        
            //new
            Container.Bind<SaveDataScriptableObject>().FromInstance(saveDataScriptableObject).AsSingle();
            //Container.Bind<Tiles>().AsSingle();
            Container.Bind<DataContext>().To<JsonDataContext>().AsSingle();
            Container.Bind<UnitOfWork>().AsSingle();
        
        
            Container.Bind<TileFinder>().AsSingle();

       
        
            Container.BindInterfacesAndSelfTo<TileMapController>().AsSingle()/*.NonLazy()*/;
        
        }
    
    }
}