using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Units
{
    public interface IUnitController
    {
        string Id { get; }
        Vector3 Position { get; set; }
        Unit Unit { get; }
        bool IsMoving { get; set; }
        UnitMovementSystem UnitMovementSystem { get; set; }
        UnitAnimator UnitAnimator { get; set; }
        void Config();
        void Destroy();
    }
}