using _Project.Scripts.Game_Actions;
using UnityEngine;

namespace _Project.Scripts.Units
{
    public class UnitView : MonoBehaviour
    {
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [SerializeField] private string buildingType;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            spriteRenderer = _transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        
        public Vector3 Position
        {
            get => _transform.position;
            set
            {   
                if(transform!= null) _transform.position = value;
            }
        }
    }
}
