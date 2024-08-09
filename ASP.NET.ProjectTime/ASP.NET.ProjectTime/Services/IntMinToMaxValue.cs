using System;

namespace ASP.NET.ProjectTime.Services
{
    [Serializable]
    public class IntMinToMaxValue
    {
        public int minValue;
        public int maxValue;

        public IntMinToMaxValue(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}