using System;

namespace ASP.NET.ProjectTime.Helpers
{
    public static class NewIdGenerator
    {
        public static string GenerateNewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}