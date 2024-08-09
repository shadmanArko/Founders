
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Buildings
{
    public interface IBuildingController 
    {
        string Id { get; }
        Building Building { get; }
        
        Vector3 Position { get; set; }
        void Config();
    }
}
