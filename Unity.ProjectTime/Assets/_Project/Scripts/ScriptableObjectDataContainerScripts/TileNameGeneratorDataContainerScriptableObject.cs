using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "TileNameGeneratorDataContainer", menuName = "ScriptableObjects/TileNameGeneratorDataContainer", order = 0)]
    public class TileNameGeneratorDataContainerScriptableObject : ScriptableObject
    { 
        public List<string> prefixIndian = new List<string>()
        {
            "Kalo", "Kali", "Alo", "Neel", "Shada", "Gora", "Ujal", "Lal", "Sobuj", "Sona", "Ghora", "Rupa",
            "Char", "Ishwar", "Golap", "Kala", "Bir", "Ek", "Dui", "Teen", "Pach", "Panch", "Sapt", "Satya", 
            "Bhil", "Khal", "Shapla", "Mach", "Dholai", "Kathi", "Kanai", "Bagher", "Biral", "Kukur", "Bibir"
        };
        public List<string> suffixIndian = new List<string>()
        {
            "ban", "sagor", "pahar", "ghat", "nadi", "math", "gram", "chal", "chail", "hat", "gaon", "arai", 
            "chari", "khar", "bari", "mura", "ganj", "tola", "toli", "para", "dha", "danga", "gura", "khali", 
            "shati", "nagar", "gacha", "kandi", "gachi", "wari", "mari", "daha", "duly", "dighi", "kund", "dwip", 
            "hat", "hut", "bazar", "bajar", "pur", "patia"
        };
        public List<string> prefixEastAsian = new List<string>()
        {
            "Kai", "Kun", "Mei", "Lan", "Bei", "Tai", "Har", "Bao", "Mi", "Dao", "Liang", "Zhang", "Dong", "Xia", 
            "Qiu","Chun", "Liu", "Huang", "Shi", "He", "Hei", "Dan", "Yi", "Han", "Tang", "Gu", "Xin", "Lu", "Gou", 
            "Huo", "Jin", "Wei", "Er", "San", "Si", "Wu", "Qi"
        };
        public List<string> suffixEastAsian = new List<string>()
        {
            "shi", "wu", "fang", "guan", "nong", "kun", "xiang", "zhuang", "lai", "xi", "pen", "hu", "huang", "sou", 
            "wa", "lin", "sen", "cong", "shan", "ling", "luan", "yan", "bu", "zhen", "cheng", "bao", "an", "bin", "wei", 
            "di", "de", "tian", "yu", "man", "zi", "kan", "gu", "shui"
        };
        public List<string> prefixAfroasiatic = new List<string>()
        {
            "Um", "Nip", "Us", "Khay", "A", "Hurg", "Mar", "Al", "Jed", "Az", "Dam", "Wa", "Bat", "Mad", "Yar", "Dha", "Hai", 
            "Nar", "Chag", "Dang", "Dam", "Shad", "Shu", "Bor", "Sir", "Ur", "Tob", "Kor", "Haj", "Akh", "As", "Ab", "Qad", 
            "Kha", "Say", "Jaf", "Har", "Bar", "Waha", "Sab"
        };
        public List<string> suffixAfroasiatic = new List<string>()
        {
            "id", "dar", "wad", "pur", "mad", "ma", "riyat", "qum", "pum", "bela", "ur", "sippa", "til", "qat", "far", "ni", 
            "hid", "nan", "warya", "qarya", "dina", "dum", "jar", "hil", "sahil", "nahr", "tala", "ayra", "tan", "jar", 
            "hisn", "suq", "hadba", "rabia", "rab", "wadi", "makan", "turba", "ba", "rit", "lad", "madi", "khib"
        };
        public List<string> prefixTurcomongol  = new List<string>()
        {
            "Kan", "Ols", "Nog", "Tset", "Besh", "Kaz", "Ulaan", "Bayan", "Ut", "Har", "Tosont", "Nogoon", "Khar", "Tsag", 
            "Tsen", "Ovol", "Zun", "Khavar", "Namar", "Samar", "Gys", "Tomus", "Qis", "Qara", "Aq", "Kuris", "Buda", "Komor", 
            "Buga", "Neg", "Biri", "Ket", "Khoyor", "Gurav", "Tav", "Bes", "Doloo", "Tulaan", "Altan", "Altin", "Astiq", "Khuur", "Chiig"
        };
        public List<string> suffixTurcomongol  = new List<string>()
        {
            "balik", "sarai", "chu", "rikh", "rar", "fan", "batr", "balsan", "chuluu", "khot", "mai", "kheer", "talbar", "khorsh", "gazar", 
            "ger", "gol", "gorkh", "gorkhi", "oi", "ursgal", "nuur", "namag", "mod", "baish", "tolgod", "uul", "khad", "khot", "zakh", "dood", 
            "ereg", "uur", "khai", "us", "els", "tsol", "ovs", "togt", "ayrag", "khand", "das", "agach", "nariq", "qala", "satir", "kol"
        };
    }
}