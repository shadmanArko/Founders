using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.DataContext;
using Zenject;

namespace SandBox.Arko.Scripts.UnitOfWork
{
    public class UnitOfWork
    {
        [Inject]
        private DataContext _dataContext;
        // [SerializeField] private TilesScriptableObject tilesScriptableObject;
        
        [Inject]
        private SaveDataScriptableObject _saveDataScriptableObject;

        public UnitOfWork(DataContext dataContext, SaveDataScriptableObject saveDataScriptableObject)
        {
            _dataContext = dataContext;
            _saveDataScriptableObject = saveDataScriptableObject;
        }
        
        public SaveDataScriptableObject SaveData => _saveDataScriptableObject;

        public async void Save()
        {
            await _dataContext.Save();
        }
        public async void Load()
        {
            await _dataContext.Load();
        }
    }
}