using System;
using System.Collections.Generic;
using ASP.NET.ProjectTime.Helpers;

namespace ASP.NET.ProjectTime.Services.Tiles
{
    public class TileNameGenerator
    {
        public TileNameGenerator(string seed)
        {
            _random = new Random(seed.GetHashCode());
        }

        private readonly Random _random;
        public string GenerateRandomName(List<string> prefix, List<string> suffix)
        {
        
            var name = prefix[_random.Next(0, prefix.Count)] +
                   suffix[_random.Next(0, suffix.Count)];

            return name.FirstCharToUpper();


        }
                      
    }
    
}