using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Natural_Resources
{
    public interface INaturalResourceController
    {
        string Id { get; }
        Vector3 Position { get; set; }
        NaturalResource NaturalResource { get; }
        void Config();
        
    }
}