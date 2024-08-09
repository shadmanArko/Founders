using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.zzz_Testing.Camera_Testing
{
    public class ArkoCameraSystem : MonoBehaviour
    { 
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private float followOffsetMinY = 10f;
        [SerializeField] private float followOffsetMaxY = 100f;
    
        private bool dragPanMoveActive;
        public Vector2 lastMousePosition;
        public Vector3 followOffset;

        public float xAmount = 0f;
        public float zAmount = -40f;
        public float zoomAmount = 3f;
        public float zoomSpeed = 10f;
        public float zoomLerpSpeed = 1f;
        public float moveSpeed;

        public float followOffsetYAmount = 0;
        void Update()
        {
            HandleCameraMovement();
            //HandleCameraMovementEdgeScrolling();
            HandleCameraMovementDragPan();
            HandleCameraZoom_LowerY(); //todo fix it

            // Vector3 inputDir = new Vector3(0, 0, 0);
            //
            // if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
            // if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
            // if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
            // if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;
            //
            // int edgeScrollSize = 20;
            //
            // if (Input.mousePosition.x < edgeScrollSize) {
            //     inputDir.x = -1f;
            // }
            // if (Input.mousePosition.y < edgeScrollSize) {
            //     inputDir.z = -1f;
            // }
            // if (Input.mousePosition.x > Screen.width - edgeScrollSize) {
            //     inputDir.x = +1f;
            // }
            // if (Input.mousePosition.y > Screen.height - edgeScrollSize) {
            //     inputDir.z = +1f;
            // }
            //
            // var transform1 = transform;
            // Vector3 moveDir = transform1.forward * inputDir.z + transform1.right * inputDir.x;
            //
            // float moveSpeed = 50f;
            // transform1.position += moveDir * (moveSpeed * Time.deltaTime);
        }
        private void HandleCameraMovement() {
            Vector3 inputDir = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
            if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
            if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
            if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    
        private void HandleCameraMovementEdgeScrolling() {
            Vector3 inputDir = new Vector3(0, 0, 0);

            int edgeScrollSize = 20;

            if (Input.mousePosition.x < edgeScrollSize) {
                inputDir.x = -1f;
            }
            if (Input.mousePosition.y < edgeScrollSize) {
                inputDir.z = -1f;
            }
            if (Input.mousePosition.x > Screen.width - edgeScrollSize) {
                inputDir.x = +1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollSize) {
                inputDir.z = +1f;
            }

            var transform1 = transform;
            Vector3 moveDir = transform1.forward * inputDir.z + transform1.right * inputDir.x;
        
            transform1.position += moveDir * moveSpeed * Time.deltaTime;
        }
    
        private void HandleCameraMovementDragPan() {
            Vector3 inputDir = new Vector3(0, 0, 0);

            if (Input.GetMouseButtonDown(1)) {
                dragPanMoveActive = true;
                lastMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(1)) {
                dragPanMoveActive = false;
            }

            if (dragPanMoveActive) {
                Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

                float dragPanSpeed = 1f;
                inputDir.x = mouseMovementDelta.x * dragPanSpeed;
                inputDir.z = mouseMovementDelta.y * dragPanSpeed;

                lastMousePosition = Input.mousePosition;
            }

            Vector3 moveDir = transform.forward * -inputDir.z + transform.right * -inputDir.x;

            // float moveSpeed = 0.5f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        private void HandleCameraZoom_LowerY() {
        
            if (Input.mouseScrollDelta.y > 0)
            {
                //followOffset.y = Mathf.Lerp(followOffset.y, followOffset.y - zoomAmount, 2f);
                //zoomAmount;

                followOffsetYAmount = followOffset.y - zoomAmount;
                //followOffset.y -= zoomAmount;
            }
       
            if (Input.mouseScrollDelta.y < 0) {
                //followOffset.y += zoomAmount;
                //followOffset.y = Mathf.Lerp(followOffset.y, followOffset.y + zoomAmount, 2f);
            
                followOffsetYAmount = followOffset.y + zoomAmount;
            }
            followOffset.y = Mathf.Lerp(followOffset.y, followOffsetYAmount, zoomLerpSpeed * Time.deltaTime);
            //followOffset.y = Mathf.Lerp(followOffset.y, followOffsetYAmount, 0.6f * Time.deltaTime);

            followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);
            followOffset.x = xAmount;
            followOffset.z = zAmount;

        
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
                Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);

        }
    }
}
