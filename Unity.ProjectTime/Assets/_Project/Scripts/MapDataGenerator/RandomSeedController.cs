using _Project.Scripts.ScriptableObjectDataContainerScripts;
using UnityEngine;

namespace _Project.Scripts.MapDataGenerator
{
    public class RandomSeedController 
    {
        private string _currentSeed;    //Current Loaded Seed
        public VariablesForMapSeed variablesForMapSeed;
        public string GetCurrentSeed() { return  _currentSeed; }

        public RandomSeedController( VariablesForMapSeed variablesForMapSeed)
        {
            this.variablesForMapSeed = variablesForMapSeed;
        }

        public void InitiateSeed()
        {
            if (variablesForMapSeed.useRandomSeed)
            {
                GenerateRandomSeed();
            }
            else
            {
                UseUserGivenSeed(variablesForMapSeed.inputSeed);
            }

        }

        //Generate Random seed for the system
        public void GenerateRandomSeed()
        {
            int tempSeed = (int)System.DateTime.Now.Ticks;
            _currentSeed = tempSeed.ToString();

            Random.InitState(tempSeed);
        }

        //Select the Seed for the System
        public void UseUserGivenSeed(string seed = "")
        {
            _currentSeed = seed;

            int tempSeed = 0;

            if (IsNumeric(seed))
                tempSeed = System.Int32.Parse(seed);
            else
                tempSeed = seed.GetHashCode();

            Random.InitState(tempSeed);
        }
        public void UseUserGivenSeed(int seed)
        {
            _currentSeed = seed.ToString();
            int tempSeed = seed;
            Random.InitState(tempSeed);
        }

        //Copy Seed to Clipboard
        public void CopySeedToClipboard() => GUIUtility.systemCopyBuffer = _currentSeed;

        //Check if Seed is All numbers
        private static bool IsNumeric(string s)
        {
            return int.TryParse(s, out int n);
        }
    }
}