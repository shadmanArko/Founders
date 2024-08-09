using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Pop : Base
    {
        public string ClanName;
        public int PopNumber;
        public string Reference;
        public TileCoordinates TileCoordinates;
        public string WorkingBuildingId;
        public int SpiritualKnowledge;
        public int EducationalTradition;
        public int PhysicalProwess;
        public int PopSize;
        public string Culture;
        public string Religion;
        public int Happiness;
        public List<Skill> Skills;
        public List<Technology> StoringTechnologies;
        public PopAppearance PopAppearance;
        public string BuildingId;
        public string UnitId;

        public Pop(string id, string clanName, int popNumber, string reference, TileCoordinates tileCoordinates, string workingBuildingId, int spiritualKnowledge, int educationalTradition, int physicalProwess, int popSize, string culture, string religion, int happiness, List<Skill> skills, List<Technology> storingTechnologies, PopAppearance popAppearance, string buildingId, string unitId)
        {
            Id = id;
            ClanName = clanName;
            PopNumber = popNumber;
            Reference = reference;
            TileCoordinates = tileCoordinates;
            WorkingBuildingId = workingBuildingId;
            SpiritualKnowledge = spiritualKnowledge;
            EducationalTradition = educationalTradition;
            PhysicalProwess = physicalProwess;
            PopSize = popSize;
            Culture = culture;
            Religion = religion;
            Happiness = happiness;
            Skills = skills;
            StoringTechnologies = storingTechnologies;
            PopAppearance = popAppearance;
            BuildingId = buildingId;
            UnitId = unitId;
        }
    }
    
}