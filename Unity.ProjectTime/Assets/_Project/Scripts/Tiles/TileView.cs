using System;
using UnityEngine;

namespace _Project.Scripts.Tiles
{
    public class TileView : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public MeshFilter meshFilter;
        public BoxCollider tileBoxCollider;


        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
            tileBoxCollider = GetComponent<BoxCollider>();
        }
    }
}
