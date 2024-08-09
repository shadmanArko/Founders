using System.Collections.Generic;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;

namespace ASP.NET.ProjectTime.Services.PopServices
{
    public class PopGenerator
    {
        public Pop CreatePop(Clan clan)
        {
            var pop = new Pop
            (
                NewIdGenerator.GenerateNewId(),
                ClanName(clan.Name),
                PopNumber(clan),
                Reference(clan),
                TileCoordinates(),
                BuildingId(),
                SpiritualKnowledge(),
                EducationalTradition(),
                PhysicalProwess(),
                PopSize(),
                Culture(),
                Religion(),
                Happiness(),
                Skills(),
                StoringTechnologies(),
                PopulationAppearance(),
                "",
                ""
            );
            
            return pop;
        }

        private PopAppearance PopulationAppearance()
        {
            var appearance = new PopAppearance
            {
                HeadShape = "Triangular",
                BodyShape = "Blob",
                HairColor = "greenish-pink",
                HairStyle = "afro",
                Id = NewIdGenerator.GenerateNewId(),
                SkinTone = "Snow-white like"
            };

            return appearance;
        }

        private List<Technology> StoringTechnologies()
        {
            var technologies = new List<Technology>
            {
                new Technology
                {
                    TechnologyType = TechnologyType.BronzeWorking
                }
            };

            return technologies;
        }

        private List<Skill> Skills()
        {
            var skills = new List<Skill>
            {
                new Skill
                {
                    SkillType = SkillType.Charioteering
                }
            };

            return skills;
        }

        private int Happiness()
        {
            return 1;
        }

        private string Religion()
        {
            return "Buddhism";
        }

        private string Culture()
        {
            return "Gothic";
        }

        private int PhysicalProwess()
        {
            return 1;
        }

        private int EducationalTradition()
        {
            return 0;
        }

        private int SpiritualKnowledge()
        {
            return 0;
        }

        private int PopSize()
        {
            return 10;
        }

        private string BuildingId()
        {
            return "Farm1";
        }

        private TileCoordinates TileCoordinates()
        {
            return new TileCoordinates
            {
                X = 0,
                Y = 1
            };
        }

        private int PopNumber(Clan clan) => ++clan.CreatedPopCounter;
        private string ClanName(string clanName) => clanName;
        private string Reference(Clan clan) => $"{clan.Name}_{clan.CreatedPopCounter.ToString()}";

    }
}