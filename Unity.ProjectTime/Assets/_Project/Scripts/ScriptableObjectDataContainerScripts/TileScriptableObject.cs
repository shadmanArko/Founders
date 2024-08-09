using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Map
{
    [CreateAssetMenu(fileName = "TileScriptableObject", menuName = "ModelScriptableObjects/TileScriptableObject", order = 0)]
    public class TileScriptableObject : ScriptableObject
    {
        public List<Tile> tiles;
    }
}