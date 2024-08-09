using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.zzz_Testing.Random_Name_Generator_Testing
{
    public class RandomNameGenerator : MonoBehaviour
    {
        public int numberOfNames = 3;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private List<string> _vowels = new List<string>()
        {
            "a","e","i","o","u", "ae", "ou", "ea", "ia", "ai", "uo", "ao", "au", "eu", "ei", "ie", "oo", "aa", "ee"
        };
        private List<string> _starters  = new List<string>()
        {
            "b","c","d","f","g","h","j","k","l","m","n","p","q","r","s","t","v","w","x","y","z","bh","ch","dh","gh","jh",
            "kh","ph","rh","sh","th","wh","zh","st","h","fl","cl","bl","gl", "wh", "vh", "qh", "pr", "kr", "br", "cr", "dr", "fr", "gr", "ar", "ir", "er", "or", "ur",
            "at", "it", "et", "ot", "ut", "al", "el", "il", "ol", "ul", "ah", "eh", "ih", "oh", "uh", "tr", "vr", "nb", "mb", "nd"
        };
        private List<string> _enders0  = new List<string>()
        {
            "th", "st", "rb", "b", "v", "x", "w", "d", "f", "g", "j", "k", "l", "m", "n", "r", "s", "t", "z", "h", "zh", "sh", "ph", "kh", "rt", "rp", "rs", "rk", "rq", "rc", "by", "cy", "dy", "gy"
            , "ly", "ny", "my", "py", "ry", "sy", "ty", "vy", "xy", "be", "ce", "de", "fe", "ge", "he", "je", "ke", "le", "me", "ne", "pe", "re", "se", "te", "ve", "ze"
            , "zy", "ba", "ca", "da", "fa", "ha", "ga", "ja", "ka", "ma", "na", "pa", "sa", "ra", "ta", "va", "za", "xa", "bo", "co", "do", "fo", "go", "ho", "jo", "ko"
            , "no", "mo", "po", "ro", "so", "to", "vo", "xo", "zo", "lb", "lc", "ld", "lf", "lg", "li", "lj", "lk", "lm", "ln", "lt", "lx", "lv", "lz", "nb", "nc", "nd"
            , "nf", "ng", "nm", "np", "ns", "nt", "nx"
        };
        private List<string> _enders1EastAsia  = new List<string>()
        {
            "li", "Wang", "zhang", "liu", "chen", "yang", "zhao", "huang", "zhou", "wu", "xu", "hu", "zhu", "sun", "gao", "ma", "luo", "liang", "song", "zheng", "xie",
            "han", "tang", "feng", "yu", "dong", "xiao", "cheng", "cao", "yuon", "deng", "fu", "shen", "peng", "su", "cai", "jia", "diang", "wei", "xue", "ye", "yan"
            , "pan", "du", "dai", "xia", "tian", "ren", "fan", "fang", "shi", "yao", "zou", "hao", "kong", "bai", "cui", "kang", "mao", "qiu", "qin" ,"shi","gu", "hou"
            ,"shao", "meng", "long", "wan", "duan", "qian", "yin", "he", "gong", "yen", "kim", "kwon", "park", "do", "yoon", "park", "ryu", "mee" 
        };
        private List<string> _enders1Germanic  = new List<string>()
        {
            "er", "ler", "mer", "der", "ter", "ner", "ger", "berg", "mann", "man", "rich", "adel", "ten", "recht", "gott", "stadt", "halt", "meier", "spach", "baum", "hans"
            , "dorf", "ling", "brust", "kamp", "hart", "hold", "holt", "stein", "hide", "side", "weg", "burg", "kert", "gall", "key", "ruth", "dum", "trup", "feld", "haus"
            , "wagen", "wirth", "horn", "hagen", "oven", "dow", "torf", "field", "rager", "den", "haupt", "enzer", "singer", "linger", "gen", "witz", "wald", "wick", "schuh"
            , "is", "speck", "bel", "liff", "weich", "hoff", "park", "sen", "dahl", "gren", "gaard", "strup", "holm", "horn", "by", "rup", "vest", "stedt", "vall", "lund", "quist"
            , "lof", "ric", "thil", "mund", "bert", "gier", "vin", "ard", "rand", "fort", "vier", "don", "illard"
        };
        private List<string> _enders1Grecoroman  = new List<string>()
        {
            "lia", "leus", "lianus", "tius", "canus", "ippina", "barbus", "bana", "binus", "tonin", "tonia", "tonius", "ulus", "sius", "linus", "ius", "calla", "sus", "nelia",
            "ima", "itia", "tilla", "neta", "gota", "ander", "thea", "nike", "dora", "idora", "genia", "lampos", "malis", "maris", "metra", "dokia", "melia", "nadius", "phone",
            "monia", "rian", "rianus", "rentia", "ginus", "retia", "cella", "minus", "tavia", "tavian", "nius", "tumus", "cus", "tus", "nus", "bina", "krates", "phalos", "petra",
            "tiades", "kissa", "ipus", "lema", "ilis", "ippos"
        };
        private List<string> _enders1Turcomongol  = new List<string>()
        {
            "oglu", "beg", "ci", "avci", "akci", "irci", "raci", "pasha", "reis", "drim", "maz", "diz", "man", "gan", "kemal", "lan", "turk", "bas", "soy", "gul", "rak", "bayar",
            "ormaa", "batar", "ulga", "orig", "suren", "bat", "gen", "jin", "sukh", "galag", "onder", "bata", "khan", "bajav", "bal", "dorj", "set", "seg", "bish", "tekin", "han",
            "vaz", "cak", "yat", "top", "vahir", "adin", "raman", "raz", "prak", "ozen", "heb", "kaya", "sal", "demir", "kiran", "gor", "zi", "kan", "dakul", "kolu", "vanc", 
            "ratay", "zoq", "mirz", "gal", "bish", "tun", "ambul"
        };
        private List<string> _enders1Celtic  = new List<string>()
        {
            "bruna", "landa", "rexta", "bula", "bodwos", "talus", "diaca", "dica", "mula", "antus", "turix", "rix", "do", "stria", "tuinda", "dubnus", "sonna", "nonia",
            "uca", "tus", "urus", "norix", "dex", "dulus", "viccus", "vix", "xus", "atae", "binn", "gharad", "mail", "git", "nech", "thal", "umban", "rien", "wendal", "reg",
            "wen", "wenus", "craic", "reig", "eben", "chad", "bail", "gall", "bales", "tilkos", "beles", "tacer", "sunin", "lacos", "vara", "moltus", "galos", "fael", "remius",
            "ois", "nowal", "dur", "dog", "wallon", "genos", "doi", "acan", "cann", "adri", "chu", "chobar", "bial"
        };
    
        [ContextMenu("Random Generation")]
        public void GenerateRandomName()
        {
            List<string> generatedNames = new List<string>();
            string selectedVowel = _vowels[Random.Range(0, _vowels.Count)];
            string selectedEnder = _enders0[Random.Range(0, _enders0.Count)];
            var enders1 = SelectEnders1();

            for (int i = 0; i < numberOfNames; i++)
            {
                string name =  _starters[Random.Range(0, _starters.Count)] + selectedVowel + selectedEnder + enders1[Random.Range(0, enders1.Count)];
                // name = name.FirstCharToUpper();
            
                if (generatedNames.Contains(name)) {i--; continue;}
                generatedNames.Add(name);
            
                Debug.Log(name +" Tribe");
            }
                      
        }

        private List<string> SelectEnders1()
        {
            List<string> enders1 = new List<string>();
            List<string> ender1Types = new List<string>()
            {
                "East Asia",
                "Germanic",
                "Grecoroman",
                "Turcomongol",
                "Celtic"
            };
            switch (ender1Types[Random.Range(0, ender1Types.Count)])
            {
                case "East Asia":
                    enders1 = _enders1EastAsia;
                    break;
                case "Germanic":
                    enders1 = _enders1Germanic;
                    break;
                case "Grecoroman":
                    enders1 = _enders1Grecoroman;
                    break;
                case "Turcomongol":
                    enders1 = _enders1Turcomongol;
                    break;
                case "Celtic":
                    enders1 = _enders1Celtic;
                    break;
            }

            return enders1;
        }
    }
}
