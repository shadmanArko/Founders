using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "ClanCoreNameGeneratorDataContainer", menuName = "ScriptableObjects/ClanCoreNameGeneratorDataContainer", order = 0)]
    public class ClanCoreNameGeneratorDataContainerScriptableObject : ScriptableObject
    {
        public List<string> starters= new List<string>()
        {
            "b","c","d","f","g","h","j","k","l","m","n","p","q","r","s","t","v","w","x","y","z","bh","ch","dh","gh","jh",
            "kh","ph","rh","sh","th","wh","zh","st","fl","cl","bl","gl", "vl", "sc","pl", "zl", "sl", "sn", "sp", "sk",
            "wh", "vh", "qh", "pr", "kr", "br", "dr", "fr", "gr", "sv"
            
        };
        public List<string> vowels= new List<string>()
        {
            "a","e","i","o","u", "ou", "ia", "ai", "au", "eu", "ei", "ie", "oo", "aa", "ee"
        };
        public List<string> enders0 = new List<string>()
        {
            "th", "st", "rb", "rd", "rg", "rj", "rk", "rn", "rm", "rz", "b", "mb", "lm","lp", "lf","lt", "ls", "v", "x", "w", "d", "f", "g", "j", "k", "l", "m", "n", "r", "s", "t", "z", "h", "zh", "y", "p", "q",
            "sh", "ph", "kh", "rt", "rp", "rs", "rl", "dh", "ty", "gy", "zy", "ht", "nc", "nd" , "ng", "nm", "np", "mp", "ng", "nz", "ns", "nt", "fy", "ry", "sp" , "sb", "sk"
        };
    }
}