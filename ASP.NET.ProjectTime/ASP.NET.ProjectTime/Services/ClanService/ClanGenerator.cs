using System.Collections.Generic;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;

namespace ASP.NET.ProjectTime.Services.ClanService
{
    public class ClanGenerator
    {
        private string _seed;
        public ClanGenerator(string seed)
        {
            _seed = seed;
        }
        public Clan GenerateNewClan(List<string> starters, List<string> vowels, List<string> enders0, List<string> endersBasedOnContinent)
        {
            var clanNameGenerator = new ClanNameGenerator(_seed);
            var clan = new Clan(
                NewIdGenerator.GenerateNewId(),
                clanNameGenerator.GenerateRandomName(starters, vowels, enders0, endersBasedOnContinent),
                0
            );
            
            return clan;
        }
        
        public Clan GenerateFirstClan(List<string> starters, string vowel, string ender0)
        {
            var clanNameGenerator = new ClanNameGenerator(_seed);
            var clan = new Clan(
                NewIdGenerator.GenerateNewId(),
                clanNameGenerator.GenerateFirstClanName(starters, vowel, ender0),
                0
            );
            
            return clan;
        }
    }
}