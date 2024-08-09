using ASP.NET.ProjectTime.Models.PathFinding;
using UnityEngine;

namespace _Project.Scripts.Pathfinding
{
    [CreateAssetMenu(fileName = "PathfinderGridDataContainer", menuName = "PathFinder/PathfinderGridDataContainer", order = 0)]
    [System.Serializable]
    public class PathfinderGridDataContainer : ScriptableObject
    {
        public Node[,] Grid;
    }
}