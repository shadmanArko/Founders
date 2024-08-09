 using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;

namespace _Project.Scripts.Pop_Clan_Culture
{
    public class PopClanDynamics
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly ClanAndPopGeneratorController _clanAndPopGeneratorController;
        private readonly int _numberOfDynamicsPossible = 11;

        private List<Pop> _removedPops = new List<Pop>();
        private List<Pop> _addedPops = new List<Pop>();


        private List<Clan> _removedClans = new List<Clan>();
        private List<Clan> _addedClans = new List<Clan>();
        private List<PopSizeChange> _increasedPopSizeList = new List<PopSizeChange>();
        private List<PopSizeChange> _decreasedPopSizeList = new List<PopSizeChange>();

        public PopClanDynamics(SaveDataScriptableObject saveDataScriptableObject, ClanAndPopGeneratorController clanAndPopGeneratorController)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
            _clanAndPopGeneratorController = clanAndPopGeneratorController;
        }

        private void RevertLastPopClanDynamics()
        {
            foreach (var addedClan in _addedClans)
            {
                _saveDataScriptableObject.Save.Clans.Remove(addedClan);
            }
            foreach (var removedClan in _removedClans)
            {
                _saveDataScriptableObject.Save.Clans.Add(removedClan);
            }
            foreach (var addedPop in _addedPops)
            {
                _saveDataScriptableObject.Save.Pops.Remove(addedPop);
                var clan = _saveDataScriptableObject.Save.Clans.FirstOrDefault(clan1 => clan1.Name == addedPop.ClanName);
                if (clan != null) clan.CreatedPopCounter--;
            }
            foreach (var removedPop in _removedPops)
            {
                _saveDataScriptableObject.Save.Pops.Remove(removedPop);
            }

            foreach (var popSizeChange in _increasedPopSizeList)
            {
                var pop = _saveDataScriptableObject.Save.Pops.FirstOrDefault(pop1 => pop1.Id == popSizeChange.Pop.Id);
                if (pop != null) pop.PopSize -= popSizeChange.Size;
            }
            foreach (var popSizeChange in _decreasedPopSizeList)
            {
                var pop = _saveDataScriptableObject.Save.Pops.FirstOrDefault(pop1 => pop1.Id == popSizeChange.Pop.Id);
                if (pop != null) pop.PopSize += popSizeChange.Size;
            }
        }
        

        private void UpdatePopClanDynamics()
        {
            ClearPreviousPopClanDynamicsData();
            var pops = GetAllPops();
            var listOfPossibleDynamicsIndex = GenerateAllPossibleDynamicsIndexes();
            foreach (var pop in pops)      
            {
                var probabilityOfAction = UnityEngine.Random.Range(0, 100);
                if (probabilityOfAction > 50)
                {
                    foreach (var dynamicsIndex in listOfPossibleDynamicsIndex.Shuffle())
                    {
                        if(dynamicsIndex == 0 && DissolutionOfExistingPopIntoOtherExistingPopsOfExistingClans(pop)) break; 
                        if(dynamicsIndex == 1 && DissolutionOfExistingPopIntoTwoNewPopsOfExistingClans(pop)) break; 
                        if(dynamicsIndex == 2 && DissolutionOfExistingPopIntoTwoNewPopsOfTwoNewClans(pop)) break; 
                        if(dynamicsIndex == 3 && DissolutionOfExistingPopIntoTwoNewPopsOfOneNewClan(pop)) break; 
                        
                        if(dynamicsIndex == 4 && UnpromptedSplittingOfExistingPopToForm_OneNewPopOf_OneNewClan(pop)) break; 
                        if(dynamicsIndex == 5 && UnpromptedSplittingOfExistingPopToForm_OneNewPop_WithinTheSameClan(pop)) break; 
                        if(dynamicsIndex == 6 && UnpromptedSplittingOfExistingPopToForm_OneNewPopOf_AnotherExistingClan(pop)) break; 
                        
                        if(dynamicsIndex == 7 && EngorgedSplittingOfExistingPopToForm_OneNewPopOf_OneNewClan(pop)) break;
                        if(dynamicsIndex == 8 && EngorgedSplittingOfExistingPopToForm_OneNewPop_WithinTheSameClan(pop)) break;
                        if(dynamicsIndex == 9 && EngorgedSplittingOfExistingPopToForm_OneNewPopOf_AnotherExistingClan(pop)) break;
                        
                        if(dynamicsIndex == 10 && CoordinatedEngorgedSplittingOfExistingPopToForm_OneNewPopOf_OneNewClan(pop)) break;

                    }
                }
                
            }

        }

        private void ClearPreviousPopClanDynamicsData()
        {
            _removedClans.Clear();
            _addedClans.Clear();
            _removedPops.Clear();
            _addedPops.Clear();
        }

        private List<int> GenerateAllPossibleDynamicsIndexes()
        {
            var listOfPossibleDynamicsIndex = new List<int>();
            for (int i = 0; i < _numberOfDynamicsPossible; i++)
            {
                listOfPossibleDynamicsIndex.Add(i);
            }

            return listOfPossibleDynamicsIndex;
        }


        #region Dissolution

        private bool DissolutionOfExistingPopIntoOtherExistingPopsOfExistingClans(Pop pop)
        {
            var startPopSize = pop.PopSize;
            List<Pop> otherExistingPops = _saveDataScriptableObject.Save.Pops.FindAll(pop1 => pop1.Id != pop.Id);
            var totalRemainingSizeInOtherPop = 0;
            foreach (var otherPop in otherExistingPops)
            {
                totalRemainingSizeInOtherPop += 10 - otherPop.PopSize;
            }

            if (totalRemainingSizeInOtherPop < pop.PopSize) return false;
            while (pop.PopSize <= 0)
            {
                var randomPop = otherExistingPops.Shuffle()[0];
                if (randomPop.PopSize < 10)
                {
                    randomPop.PopSize++;
                    _increasedPopSizeList.Add(new PopSizeChange(){Pop = randomPop, Size = 1});
                    pop.PopSize--;
                    AssignSpiritualEducationalAndPhysicalStatsForNewPop(randomPop, randomPop.PopSize-1, pop, 1, randomPop);
                }
            }

            if (pop.PopSize == 0)
            {
                pop.PopSize = startPopSize;
                _removedPops.Add(pop);
                _saveDataScriptableObject.Save.Pops.Remove(pop);
            }
            return true;
        }
        private bool DissolutionOfExistingPopIntoTwoNewPopsOfExistingClans(Pop pop)
        {
            var startPopSize = pop.PopSize;
            List<Clan> otherExistingClans = _saveDataScriptableObject.Save.Clans.FindAll(clan => clan.Name != pop.ClanName);
            var totalRemainingSpaceInOtherClans = 0;
            foreach (var otherClan in otherExistingClans)
            {
                totalRemainingSpaceInOtherClans += 4 - otherClan.CreatedPopCounter;
            }

            if (totalRemainingSpaceInOtherClans <2) return false;
            var createdPopCount = 0;
            DividePopInRandomSize(pop.PopSize, out var newPopSize1, out var newPopSize2);
            List<Pop> newPopList = new List<Pop>();
            while (createdPopCount < 2)
            {
                var randomClan = otherExistingClans.Shuffle()[0];
                if (randomClan.CreatedPopCounter < 4)
                {
                    var newPop = CreatePopOfSize(randomClan, createdPopCount < 1 ? newPopSize1 : newPopSize2);
                    newPopList.Add(newPop);
                    createdPopCount++;
                }
            }

            _saveDataScriptableObject.Save.Pops.Remove(pop);
            pop.PopSize = startPopSize;
            _removedPops.Add(pop);
            SavePops(newPopList);
            return true;
        }
        private bool DissolutionOfExistingPopIntoTwoNewPopsOfTwoNewClans(Pop pop)
        {
            var clan1 = _clanAndPopGeneratorController.GenerateNewClan();
            var clan2 = _clanAndPopGeneratorController.GenerateNewClan();
            List<Clan> listOfNewClans = new List<Clan>() { clan1, clan2 };
            var createdPopCount = 0;
            DividePopInRandomSize(pop.PopSize, out var newPopSize1, out var newPopSize2);
            List<Pop> newPopList = new List<Pop>();
            while (createdPopCount < 2)
            {
                var selectedClan = listOfNewClans[createdPopCount];
                if (selectedClan.CreatedPopCounter < 4)
                {
                    var newPop = CreatePopOfSize(selectedClan, createdPopCount < 1 ? newPopSize1 : newPopSize2);
                    newPopList.Add(newPop);
                    createdPopCount++;
                }
            }

            _saveDataScriptableObject.Save.Pops.Remove(pop);
            SavePops(newPopList);
            SaveNewClans(listOfNewClans);
            return true;
        }
        private bool DissolutionOfExistingPopIntoTwoNewPopsOfOneNewClan(Pop pop)
        {
            var clan1 = _clanAndPopGeneratorController.GenerateNewClan();
            List<Clan> listOfNewClans = new List<Clan>() { clan1 };
            var createdPopCount = 0;
            DividePopInRandomSize(pop.PopSize, out var newPopSize1, out var newPopSize2);
            List<Pop> newPopList = new List<Pop>();
            while (createdPopCount < 2)
            {
                var selectedClan = listOfNewClans[0];
                if (selectedClan.CreatedPopCounter < 4)
                {
                    var newPop = CreatePopOfSize(selectedClan, createdPopCount < 1 ? newPopSize1 : newPopSize2);
                    newPopList.Add(newPop);
                    createdPopCount++;
                }
            }

            _saveDataScriptableObject.Save.Pops.Remove(pop);
            SavePops(newPopList);
            SaveNewClans(listOfNewClans);
            return true;
        }

        #endregion

        #region Unprompted Splitting
        private bool UnpromptedSplittingOfExistingPopToForm_OneNewPopOf_OneNewClan(Pop pop)
        {
            var clan1 = _clanAndPopGeneratorController.GenerateNewClan();
            List<Clan> listOfNewClans = new List<Clan>() { clan1 };
            DividePopInRandomSize(pop.PopSize, out var newPopSize1, out var newPopSize2);
            List<Pop> newPopList = new List<Pop>();

            pop.PopSize = newPopSize1;
            _decreasedPopSizeList.Add(new PopSizeChange(){Pop = pop, Size = newPopSize2});
            var newPop = CreatePopOfSize(listOfNewClans[0], newPopSize2);
            AssignSpiritualEducationalAndPhysicalStatsForNewPop(pop, newPopSize2, pop, 0, newPop);
            newPopList.Add(newPop);

            SavePops(newPopList);
            SaveNewClans(listOfNewClans);
            return true;
        }
        private bool UnpromptedSplittingOfExistingPopToForm_OneNewPopOf_AnotherExistingClan(Pop pop)
        {
            var clan = _saveDataScriptableObject.Save.Clans.ToList().Shuffle().FirstOrDefault(newClan =>
                newClan.Name != pop.ClanName && newClan.CreatedPopCounter < 4);
            if (clan == null) return false;
            DividePopInRandomSize(pop.PopSize, out var newPopSize1, out var newPopSize2);
            List<Pop> newPopList = new List<Pop>();

            pop.PopSize = newPopSize1;
            _decreasedPopSizeList.Add(new PopSizeChange(){Pop = pop, Size = newPopSize2});
            var newPop = CreatePopOfSize(clan, newPopSize2);
            AssignSpiritualEducationalAndPhysicalStatsForNewPop(pop, newPopSize2, pop, 0, newPop);
            newPopList.Add(newPop);

            _saveDataScriptableObject.Save.Clans.Remove(_saveDataScriptableObject.Save.Clans.GetById(clan1 => clan1.Id, clan.Id));
            _removedClans.Add(clan);
            SavePops(newPopList);
            SaveNewClans(new List<Clan>(){clan});
            return true;
        }
        private bool UnpromptedSplittingOfExistingPopToForm_OneNewPop_WithinTheSameClan(Pop pop)
        {
            var clan = _saveDataScriptableObject.Save.Clans.GetById(clan1 => clan1.Name, pop.ClanName);
            if (clan.CreatedPopCounter >= 4) return false;
            DividePopInRandomSize(pop.PopSize, out var newPopSize1, out var newPopSize2);
            List<Pop> newPopList = new List<Pop>();

            pop.PopSize = newPopSize1;
            _decreasedPopSizeList.Add(new PopSizeChange(){Pop = pop, Size = newPopSize2});
            var newPop = CreatePopOfSize(clan, newPopSize2);
            AssignSpiritualEducationalAndPhysicalStatsForNewPop(pop, newPopSize2, pop, 0, newPop);
            newPopList.Add(newPop);

            SavePops(newPopList);
            return true;
        }
        
        #endregion

        #region Engorged Splitting

        private bool EngorgedSplittingOfExistingPopToForm_OneNewPopOf_OneNewClan(Pop pop)
        {
            if (pop.PopSize < 10) return false;
            var clan1 = _clanAndPopGeneratorController.GenerateNewClan();
            List<Clan> listOfNewClans = new List<Clan>() { clan1 };
            DividePopSizeForEngorgedSplitting(pop.PopSize, out var newPopSize1, out var remainingPopSize);
            List<Pop> newPopList = new List<Pop>();

            pop.PopSize = remainingPopSize;
            _decreasedPopSizeList.Add(new PopSizeChange(){Pop = pop, Size = newPopSize1});
            var newPop = CreatePopOfSize(listOfNewClans[0], newPopSize1);
            AssignSpiritualEducationalAndPhysicalStatsForNewPop(pop, newPopSize1, pop, 0, newPop);
            newPopList.Add(newPop);

            SavePops(newPopList);
            SaveNewClans(listOfNewClans);
            return true;
        }
        private bool EngorgedSplittingOfExistingPopToForm_OneNewPopOf_AnotherExistingClan(Pop pop)
        {
            if (pop.PopSize < 10) return false;
            var clan = _saveDataScriptableObject.Save.Clans.ToList().Shuffle().FirstOrDefault(newClan =>
                newClan.Name != pop.ClanName && newClan.CreatedPopCounter < 4);
            if (clan == null) return false;
            DividePopSizeForEngorgedSplitting(pop.PopSize, out var newPopSize1, out var remainingPopSize);
            List<Pop> newPopList = new List<Pop>();

            pop.PopSize = remainingPopSize;
            _decreasedPopSizeList.Add(new PopSizeChange(){Pop = pop, Size = newPopSize1});
            var newPop = CreatePopOfSize(clan, newPopSize1);
            AssignSpiritualEducationalAndPhysicalStatsForNewPop(pop, newPopSize1, pop, 0, newPop);
            newPopList.Add(newPop);

            _saveDataScriptableObject.Save.Clans.Remove(_saveDataScriptableObject.Save.Clans.GetById(clan1 => clan1.Id, clan.Id));
            _removedClans.Add(clan);
            SavePops(newPopList);
            SaveNewClans(new List<Clan>(){clan});
            return true;
        }
        private bool EngorgedSplittingOfExistingPopToForm_OneNewPop_WithinTheSameClan(Pop pop)
        {
            if (pop.PopSize < 10) return false;
            var clan = _saveDataScriptableObject.Save.Clans.GetById(clan1 => clan1.Name, pop.ClanName);
            if (clan.CreatedPopCounter >= 4) return false;
            DividePopSizeForEngorgedSplitting(pop.PopSize, out var newPopSize1, out var remainingPopSize);
            List<Pop> newPopList = new List<Pop>();

            pop.PopSize = remainingPopSize;
            _decreasedPopSizeList.Add(new PopSizeChange(){Pop = pop, Size = newPopSize1});
            var newPop = CreatePopOfSize(clan, newPopSize1);
            AssignSpiritualEducationalAndPhysicalStatsForNewPop(pop, newPopSize1, pop, 0, newPop);
            newPopList.Add(newPop);

            SavePops(newPopList);
            return true;
        }

        #endregion

        #region Coordinated Engorged Splitting
        private bool CoordinatedEngorgedSplittingOfExistingPopToForm_OneNewPopOf_OneNewClan(Pop pop)
        {
            var anotherPop =
                _saveDataScriptableObject.Save.Pops.FirstOrDefault(
                    newPop => newPop.Id != pop.Id && newPop.PopSize >= 10);
            if (anotherPop == null || pop.PopSize < 10) return false;
            
            var newClan = _clanAndPopGeneratorController.GenerateNewClan();
            List<Clan> listOfNewClans = new List<Clan>() { newClan };
            DividePopSizeForEngorgedSplitting(pop.PopSize, out var newPopSize1, out var remainingPopSize);
            DividePopSizeForEngorgedSplitting(anotherPop.PopSize, out var anotherNewPopSize1, out var remainingAnotherPopSize);
            List<Pop> newPopList = new List<Pop>();

            pop.PopSize = remainingPopSize;
            _decreasedPopSizeList.Add(new PopSizeChange(){Pop = pop, Size = newPopSize1});
            anotherPop.PopSize = remainingAnotherPopSize;
            var newPop = CreatePopOfSize(listOfNewClans[0], newPopSize1+anotherNewPopSize1);
            AssignSpiritualEducationalAndPhysicalStatsForNewPop(pop, newPopSize1, anotherPop, anotherNewPopSize1, newPop);
            newPopList.Add(newPop);

            SavePops(newPopList);
            SaveNewClans(listOfNewClans);
            return true;
        }
        #endregion
        
        #region UtilityMethodes

        private Pop CreatePopOfSize(Clan clan, int popSize)
        {
            var pop = _clanAndPopGeneratorController.GeneratePop(clan);
            pop.PopSize = popSize;
            return pop;
        }

        private List<Pop> GetAllPops() 
        {   
            var pops = new List<Pop>();
            foreach (var pop in _saveDataScriptableObject.Save.Pops)
            {
                pops.Add(pop);
            }
            return pops;
        }
        private static void DividePopInRandomSize(int popSize, out int newPopSize1, out int newPopSize2)
        {
            newPopSize1 = UnityEngine.Random.Range(1, popSize);
            newPopSize2 = popSize - newPopSize1;
        }
        private static void DividePopSizeForEngorgedSplitting(int popSize, out int newPopSize, out int remainingPopSize)
        {
            newPopSize = UnityEngine.Random.Range(2, 6);
            remainingPopSize = popSize - newPopSize;
        }
        
        private void AssignSpiritualEducationalAndPhysicalStatsForNewPop(Pop pop1, int pop1SizeTaken, Pop pop2, int pop2SizeTaken, Pop createdPop)
        {
            createdPop.SpiritualKnowledge =
                ((pop1.SpiritualKnowledge * pop1SizeTaken) + (pop2.SpiritualKnowledge * pop2SizeTaken)) /
                (pop1SizeTaken + pop2SizeTaken);
            
            createdPop.EducationalTradition =
                ((pop1.EducationalTradition * pop1SizeTaken) + (pop2.EducationalTradition * pop2SizeTaken)) /
                (pop1SizeTaken + pop2SizeTaken);
            
            createdPop.PhysicalProwess =
                ((pop1.PhysicalProwess * pop1SizeTaken) + (pop2.PhysicalProwess * pop2SizeTaken)) /
                (pop1SizeTaken + pop2SizeTaken);
        }
        #endregion

        #region Saving

        private void SavePops(List<Pop> newPopList)
        {
            foreach (var newPop in newPopList)
            {
                _saveDataScriptableObject.Save.Pops.Add(newPop);
                _addedPops.Add(newPop);
            }
        }
        private void SaveNewClans(List<Clan> listOfNewClans)
        {
            foreach (var newClan in listOfNewClans)
            {
                _saveDataScriptableObject.Save.Clans.Add(newClan);
                _addedClans.Add(newClan);
            }
        }

        #endregion
        
        
        
    }
    [Serializable]
    public class PopSizeChange
    {
        public Pop Pop;
        public int Size;
    }
}
