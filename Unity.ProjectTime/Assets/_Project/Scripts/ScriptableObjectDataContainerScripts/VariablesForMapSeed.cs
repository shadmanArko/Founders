using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "SeedVariables", menuName = "MapGeneration/SeedVariable", order = 0)]
    public class VariablesForMapSeed : ScriptableObject
    {
        public string inputSeed;
        public bool useRandomSeed;
        // public string currentSeed; 
        
    }
}