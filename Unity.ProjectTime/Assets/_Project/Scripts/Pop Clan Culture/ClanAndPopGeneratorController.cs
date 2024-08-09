using System.Collections.Generic;
using _Project.Scripts.DataBase.Initializer;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services.ClanService;
using ASP.NET.ProjectTime.Services.PopServices;
using Cysharp.Threading.Tasks;
using SandBox.Arko.Scripts.UnitOfWork;
using UnityEngine;

namespace _Project.Scripts.Pop_Clan_Culture
{
    public class ClanAndPopGeneratorController
    {
        private NamingSetsContainer _namingSetsContainer;
        private ClanCoreNameGeneratorDataContainerScriptableObject clanCoreNameGenerator;
        private SaveDataScriptableObject saveDataScriptableObject;
        private StartingConditionsFunctionalDataSo _startingConditionsFunctionalDataSo;
        private readonly UnitOfWork _unitOfWork;
        private readonly Initializer _initializer;

        public ClanAndPopGeneratorController(Initializer initializer, UnitOfWork unitOfWork, NamingSetsContainer namingSetsContainer, ClanCoreNameGeneratorDataContainerScriptableObject clanCoreNameGenerator, SaveDataScriptableObject saveDataScriptableObject,
            StartingConditionsFunctionalDataSo startingConditionsFunctionalDataSo)
        {
            _initializer = initializer;
            _namingSetsContainer = namingSetsContainer;
            this.clanCoreNameGenerator = clanCoreNameGenerator;
            this.saveDataScriptableObject = saveDataScriptableObject;
            _startingConditionsFunctionalDataSo = startingConditionsFunctionalDataSo;
            _unitOfWork = unitOfWork;
        }

        #region Clan and Pop Generation

        public Clan GenerateNewClan()
        {
            var rand = Random.Range(0, 10000);
            var starters = clanCoreNameGenerator.starters;
            var vowels = clanCoreNameGenerator.vowels;
            var enders0 = clanCoreNameGenerator.enders0;
            var endersBasedOnCulture = _namingSetsContainer.namingSets[0].clanNameEnders;
            var clanGenerator = new ClanGenerator(rand.ToString());
            var clan = clanGenerator.GenerateNewClan(starters, vowels, enders0, endersBasedOnCulture);
            
            return clan;
        }
        
        public Pop GeneratePop(Clan clan)
        {
            var popGenerator = new PopGenerator();
            var pop = popGenerator.CreatePop(clan);
            
            return pop;
        }

        #endregion

        #region Clan and Pop Generation for New Game

        private Clan GenerateClans_NewGame(List<string> starters, string vowel, string ender0)
        {
            var rand = Random.Range(0, 10000);
            var clanGenerator = new ClanGenerator(rand.ToString());
            var clan = clanGenerator.GenerateFirstClan(starters, vowel, ender0);

            if (CheckIfClanExists(clan.Name))
                return GenerateClans_NewGame(starters, vowel, ender0);
            
            saveDataScriptableObject.Save.Clans.Add(clan);
            return clan;
        }

        private void SetPopStatsBasedOnNewGameStats(List<Pop> pops)
        {
            var maxStatPoints = _startingConditionsFunctionalDataSo.maximumTotalStatsPerPop;
            var minStatPoints = _startingConditionsFunctionalDataSo.minimumTotalStatsPerPop;
            var bestStatPoint = _startingConditionsFunctionalDataSo.minimumStatInBest;

            var minHappiness = _startingConditionsFunctionalDataSo.minimumStartingHappiness;
            var maxHappiness = _startingConditionsFunctionalDataSo.maximumStartingHappiness;

            foreach (var pop in pops)
            {
                var totalStatPoints = Random.Range(maxStatPoints, minStatPoints);
                var bestStat = Random.Range(0, 3);

                int statsForPhysicalProwess;
                int statsForEducationalTradition;
                int statsForSpiritualKnowledge;
                
                switch (bestStat)
                {
                    case 2:
                        statsForPhysicalProwess = Random.Range(bestStatPoint, 20);
                        pop.PhysicalProwess = statsForPhysicalProwess;
                        totalStatPoints -= statsForPhysicalProwess;
                        
                        statsForEducationalTradition = Random.Range(5, totalStatPoints);
                        pop.EducationalTradition = statsForEducationalTradition;
                        totalStatPoints -= statsForEducationalTradition;
                        
                        pop.SpiritualKnowledge = totalStatPoints;
                        
                        Debug.Log(pop.ClanName+pop.PopNumber+" case 2");
                        break;
                    case 1:
                        statsForEducationalTradition = Random.Range(bestStatPoint, 20);
                        pop.EducationalTradition = statsForEducationalTradition;
                        totalStatPoints -= statsForEducationalTradition;
                        
                        statsForSpiritualKnowledge = Random.Range(5, totalStatPoints);
                        pop.SpiritualKnowledge = statsForSpiritualKnowledge;
                        totalStatPoints -= statsForSpiritualKnowledge;
                        
                        pop.PhysicalProwess = totalStatPoints;
                        Debug.Log(pop.ClanName+pop.PopNumber+" case 1");
                        break;
                    case 0:
                        statsForSpiritualKnowledge = Random.Range(bestStatPoint, 20);
                        pop.SpiritualKnowledge = statsForSpiritualKnowledge;
                        totalStatPoints -= statsForSpiritualKnowledge;
                        
                        statsForPhysicalProwess = Random.Range(5, totalStatPoints);
                        pop.PhysicalProwess = statsForPhysicalProwess;
                        totalStatPoints -= statsForPhysicalProwess;
                        
                        pop.EducationalTradition = totalStatPoints;
                        Debug.Log(pop.ClanName+pop.PopNumber+" case 0");
                        break;
                }

                pop.Happiness = Random.Range(minHappiness, maxHappiness);
            }
        }
        
        #endregion
        
        public async UniTask<(List<Clan>, List<Pop>)> GenerateClanAndPopOnStart_NewGame()
        {
            var clanList = new List<Clan>();
            var popList = new List<Pop>();
            
            var starters = clanCoreNameGenerator.starters;
            var vowel = clanCoreNameGenerator.vowels[Random.Range(0, clanCoreNameGenerator.vowels.Count)];
            var ender0 = clanCoreNameGenerator.enders0[Random.Range(0, clanCoreNameGenerator.enders0.Count)];
            
            for (var i = 0; i < _startingConditionsFunctionalDataSo.totalStaringClans; i++)
            {
                var clan = GenerateClans_NewGame(starters, vowel, ender0);

                clanList.Add(clan);

                for (var j = 0; j < _startingConditionsFunctionalDataSo.startingPopsInEachClan; j++)
                {
                    var pop = GeneratePop(clan);
                    popList.Add(pop);
                }

                if (i != 0) continue;
                {
                    for (var j = 0; j < _startingConditionsFunctionalDataSo.excessPopInFirstClan; j++)
                    {
                        var pop = GeneratePop(clan);
                        popList.Add(pop);
                    }
                }
            }

            SetPopStatsBasedOnNewGameStats(popList);
            
            saveDataScriptableObject.Save.Clans = clanList;
            saveDataScriptableObject.Save.Pops = popList;

            await _initializer.CreateNewSaveFile("gg");
            _unitOfWork.Save();
            
            return (clanList, popList);
        }

        #region Utility Methods

        private bool CheckIfClanExists(string name)
        {
            foreach (var clan in saveDataScriptableObject.Save.Clans)
                if (clan.Name.Equals(name))
                {
                    Debug.Log(name+" already exists");
                    return true;
                }

            return false;
        }

        #endregion
        
    }
}