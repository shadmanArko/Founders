using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.zzz_Testing.Random_Name_Generator_Testing
{
    public class RandomPlaceNameGenerator : MonoBehaviour
    {
        public int numberOfNames = 3;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private List<string> _prefixIndian = new List<string>()
        {
            "Kalo", "Kali", "Alo", "Neel", "Shada", "Gora", "Ujal", "Lal", "Sobuj", "Sona", "Ghora", "Rupa",
            "Char", "Ishwar", "Golap", "Kala", "Bir", "Ek", "Dui", "Teen", "Pach", "Panch", "Sapt", "Satya", 
            "Bhil", "Khal", "Shapla", "Mach", "Dholai", "Kathi", "Kanai", "Bagher", "Biral", "Kukur", "Bibir"
        };
        private List<string> _suffixIndian = new List<string>()
        {
            "ban", "sagor", "pahar", "ghat", "nadi", "math", "gram", "chal", "chail", "hat", "gaon", "arai", 
            "chari", "khar", "bari", "mura", "ganj", "tola", "toli", "para", "dha", "danga", "gura", "khali", 
            "shati", "nagar", "gacha", "kandi", "gachi", "wari", "mari", "daha", "duly", "dighi", "kund", "dwip", 
            "hat", "hut", "bazar", "bajar", "pur", "patia"
        };
        private List<string> _prefixEastAsian = new List<string>()
        {
            "Kai", "Kun", "Mei", "Lan", "Bei", "Tai", "Har", "Bao", "Mi", "Dao", "Liang", "Zhang", "Dong", "Xia", 
            "Qiu","Chun", "Liu", "Huang", "Shi", "He", "Hei", "Dan", "Yi", "Han", "Tang", "Gu", "Xin", "Lu", "Gou", 
            "Huo", "Jin", "Wei", "Er", "San", "Si", "Wu", "Qi"
        };
        private List<string> _suffixEastAsian = new List<string>()
        {
            "shi", "wu", "fang", "guan", "nong", "kun", "xiang", "zhuang", "lai", "xi", "pen", "hu", "huang", "sou", 
            "wa", "lin", "sen", "cong", "shan", "ling", "luan", "yan", "bu", "zhen", "cheng", "bao", "an", "bin", "wei", 
            "di", "de", "tian", "yu", "man", "zi", "kan", "gu", "shui"
        };
        private List<string> _prefixAfroasiatic = new List<string>()
        {
            "Um", "Nip", "Us", "Khay", "A", "Hurg", "Mar", "Al", "Jed", "Az", "Dam", "Wa", "Bat", "Mad", "Yar", "Dha", "Hai", 
            "Nar", "Chag", "Dang", "Dam", "Shad", "Shu", "Bor", "Sir", "Ur", "Tob", "Kor", "Haj", "Akh", "As", "Ab", "Qad", 
            "Kha", "Say", "Jaf", "Har", "Bar", "Waha", "Sab"
        };
        private List<string> _suffixAfroasiatic = new List<string>()
        {
            "id", "dar", "wad", "pur", "mad", "ma", "riyat", "qum", "pum", "bela", "ur", "sippa", "til", "qat", "far", "ni", 
            "hid", "nan", "warya", "qarya", "dina", "dum", "jar", "hil", "sahil", "nahr", "tala", "ayra", "tan", "jar", 
            "hisn", "suq", "hadba", "rabia", "rab", "wadi", "makan", "turba", "ba", "rit", "lad", "madi", "khib"
        };
        private List<string> _prefixTurcomongol  = new List<string>()
        {
            "Kan", "Ols", "Nog", "Tset", "Besh", "Kaz", "Ulaan", "Bayan", "Ut", "Har", "Tosont", "Nogoon", "Khar", "Tsag", 
            "Tsen", "Ovol", "Zun", "Khavar", "Namar", "Samar", "Gys", "Tomus", "Qis", "Qara", "Aq", "Kuris", "Buda", "Komor", 
            "Buga", "Neg", "Biri", "Ket", "Khoyor", "Gurav", "Tav", "Bes", "Doloo", "Tulaan", "Altan", "Altin", "Astiq", "Khuur", "Chiig"
        };
        private List<string> _suffixTurcomongol  = new List<string>()
        {
            "balik", "sarai", "chu", "rikh", "rar", "fan", "batr", "balsan", "chuluu", "khot", "mai", "kheer", "talbar", "khorsh", "gazar", 
            "ger", "gol", "gorkh", "gorkhi", "oi", "ursgal", "nuur", "namag", "mod", "baish", "tolgod", "uul", "khad", "khot", "zakh", "dood", 
            "ereg", "uur", "khai", "us", "els", "tsol", "ovs", "togt", "ayrag", "khand", "das", "agach", "nariq", "qala", "satir", "kol"
        };
    
        private List<string> _directionals = new List<string>()
        {
        
        };

        [ContextMenu("Random Generation")]
        public void GenerateRandomName()
        {
            List<string> generatedNames = new List<string>();
        
            // var enders1 = SelectEnders1();

            for (int i = 0; i < numberOfNames; i++)
            {
                string generatedName = GenerateTurcomongolName();
                // generatedName = generatedName.FirstCharToUpper();
            
                if (generatedNames.Contains(generatedName)) {i--; continue;}
                generatedNames.Add(generatedName);
            
                Debug.Log(generatedName);
            }
                      
        }

        private string GenerateIndianName()
        {
            return _prefixIndian[Random.Range(0, _prefixIndian.Count)] +
                   _suffixIndian[Random.Range(0, _suffixIndian.Count)];
        }
        private string GenerateEastAsianName()
        {
            return _prefixEastAsian[Random.Range(0, _prefixEastAsian.Count)] +
                   _suffixEastAsian[Random.Range(0, _suffixEastAsian.Count)];
        }
        private string GenerateAfroasiaticName()
        {
            return _prefixAfroasiatic[Random.Range(0, _prefixAfroasiatic.Count)] +
                   _suffixAfroasiatic[Random.Range(0, _suffixAfroasiatic.Count)];
        }
        private string GenerateTurcomongolName()
        {
            return _prefixTurcomongol[Random.Range(0, _prefixTurcomongol.Count)] +
                   _suffixTurcomongol[Random.Range(0, _suffixTurcomongol.Count)];
        }

        // private List<string> SelectEnders1()
        // {
        //     List<string> enders1 = new List<string>();
        //     List<string> ender1Types = new List<string>()
        //     {
        //         "East Asia",
        //         "Germanic",
        //         "French",
        //         "Turkic"
        //     };
        //     switch (ender1Types[Random.Range(0, ender1Types.Count)])
        //     {
        //         case "East Asia":
        //             enders1 = _enders1EastAsia;
        //             break;
        //         case "Germanic":
        //             enders1 = _enders1Germanic;
        //             break;
        //         case "French":
        //             enders1 = _enders1French;
        //             break;
        //         case "Turkic":
        //             enders1 = _enders1Turkic;
        //             break;
        //     }
        //
        //     return enders1;
        // }
    }
}
