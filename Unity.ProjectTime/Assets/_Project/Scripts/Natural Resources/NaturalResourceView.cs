using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Natural_Resources
{
    public class NaturalResourceView : MonoBehaviour
    {
        public NaturalResource naturalResource;
        public SpriteRenderer resourceSprite;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
            resourceSprite = _transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        }
        public Vector3 Position
        {
            get => _transform.position;
            set
            {   
                if(transform!= null) _transform.position = value;
            }
        }
        // public void Init(NaturalResource naturalResource)
        // {
        //     // //todo make it fix, Make View and Controller
        //     // resourceSprite.sprite = naturalResource.GetResourceIcon();
        //     // resourceName = naturalResource.GetResourceName();
        // }
    }
}
