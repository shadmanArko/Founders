using System;
using UnityEngine;

namespace _Project.Scripts.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        public  BuildingSpriteRendererContainer buildingSpriteRendererContainer;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            buildingSpriteRendererContainer = GetComponent<BuildingSpriteRendererContainer>();
        }
        

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;   
        }
    }
}
