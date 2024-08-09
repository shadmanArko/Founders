using System;
using System.Collections.Generic;
using ASP.NET.ProjectTime.Helpers;

namespace ASP.NET.ProjectTime.Services.ClanService
{
    public class ClanNameGenerator 
    {
        
        public ClanNameGenerator(string seed)
        {
            _random = new Random(seed.GetHashCode());
        }

        private Random _random;

        public string GenerateRandomName(List<string> starters, List<string> vowels, List<string> enders0, List<string> enders1BasedOnContinent)
        {
            string selectedVowel = vowels[_random.Next(0, vowels.Count)];
            string selectedStarter = starters[_random.Next(0, starters.Count)];
            string selectedEnder0 = enders0[_random.Next(0, enders0.Count)];
            string selectedEnder1BasedOnContinent = enders1BasedOnContinent[_random.Next(0, enders1BasedOnContinent.Count)];
            
            string name =  selectedStarter + selectedVowel + selectedEnder0 + selectedEnder1BasedOnContinent;
            name = name.FirstCharToUpper();

            return name;
        }

        public string GenerateFirstClanName(List<string> starters, string vowel, string ender0)
        {
            var starter = starters[_random.Next(0, starters.Count)];
            var name = starter + vowel + ender0;
            
            name = name.FirstCharToUpper();
            return name;
        }

        
    }
}
