using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.HelperScripts
{
    public static class ListExtensions
    {
        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> shuffledList = new List<T>(list); // Create a new list as a copy of the original list

            int n = shuffledList.Count;
            while (n > 1)
            {
                int k = Random.Range(0, n);
                n--;
                (shuffledList[n], shuffledList[k]) = (shuffledList[k], shuffledList[n]);
            }

            return shuffledList;
        }
    }
}