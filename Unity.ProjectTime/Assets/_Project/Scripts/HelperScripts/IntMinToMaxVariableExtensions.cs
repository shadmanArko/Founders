using ASP.NET.ProjectTime.Services;
using UnityEngine;

namespace _Project.Scripts.HelperScripts
{
   public static class IntMinToMaxVariableExtensions 
   {
      public static int RandomValueInRange(this IntMinToMaxValue intMinToMaxValue)
      {
         return Random.Range(intMinToMaxValue.minValue, intMinToMaxValue.maxValue + 1);
      }
   }
}
