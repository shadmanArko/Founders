using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "MapChunkAddress", menuName = "ProceduralMapChunkAddress/MapChunkAddress", order = 0)]
    public class MapChunkAddressScriptableObject : ScriptableObject
    {
        public List<Position2> chunkAddress;
    }
}
