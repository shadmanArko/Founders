using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "StartingConditionsFunctionalDataSo", menuName = "ScriptableObjects/StartingConditionsFunctionalDataSo", order = 0)]
    public class StartingConditionsFunctionalDataSo : ScriptableObject
    {
        public int totalStaringClans;
        public int startingPopsInEachClan;
        public int excessPopInFirstClan;
        public int minimumTotalStatsPerPop;
        public int maximumTotalStatsPerPop;
        public int minimumStatInBest;
        public int minimumStartingHappiness;
        public int maximumStartingHappiness;
    }
}