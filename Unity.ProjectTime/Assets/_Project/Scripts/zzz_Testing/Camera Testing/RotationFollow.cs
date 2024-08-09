using UnityEngine;

namespace _Project.Scripts.zzz_Testing.Camera_Testing
{
    public class RotationFollow : MonoBehaviour
    {
        private GameObject _gameObjectToFollow;
        // Start is called before the first frame update
        void Start()
        {
            _gameObjectToFollow = GameObject.Find("Virtual Camera");
        }

        // Update is called once per frame
        void Update()
        {
            transform.eulerAngles = new Vector3(_gameObjectToFollow.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) ;
        }
    }
}
