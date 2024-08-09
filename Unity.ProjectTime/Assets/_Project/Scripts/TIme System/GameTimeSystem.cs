using System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;

namespace _Project.Scripts.TIme_System
{
    public class GameTimeSystem
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public GameTimeSystem(SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
        }
        public void IncreaseTime()
        {
            _saveDataScriptableObject.Save.GameTime.Week++;
            if (_saveDataScriptableObject.Save.GameTime.Week > 50)
            {
                _saveDataScriptableObject.Save.GameTime.Week = 1;
                _saveDataScriptableObject.Save.GameTime.Year++;
            }

            var gameTimeInWords =
                $"Week {_saveDataScriptableObject.Save.GameTime.Week}, {ConvertToBC_AC(_saveDataScriptableObject.Save.GameTime.Year)}";

            _saveDataScriptableObject.Save.GameTime.InWords = gameTimeInWords;
        }
        
        private string ConvertToBC_AC(int year)
        {
            if (year < 0)
            {
                return $"{Math.Abs(year)} BC";
            }
            else
            {
                return $"{year} AC";
            }
        }
    }
}
