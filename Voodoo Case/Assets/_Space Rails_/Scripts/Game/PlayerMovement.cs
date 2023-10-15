using UnityEngine;

namespace SpaceRails.Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _strafeSpeed = 5f;
        [SerializeField] private float _moveSpeed = 1f;
    
        private Rigidbody rb;
        private Camera mainCamera;
        private Vector3 _touchStartScreenPos;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _touchStartScreenPos = Input.mousePosition;

            rb.velocity = new Vector3(GetVelocityX(), rb.velocity.y, _moveSpeed);
        }

        private float GetVelocityX()
        {
            if (!Input.GetMouseButton(0))
                return 0f;
        
            var touchStartPosWorld = GetMousePosInWorldSpace(_touchStartScreenPos);
            var touchPosWorld = GetMousePosInWorldSpace(Input.mousePosition);

            var targetPosition = new Vector3(touchPosWorld.x, transform.position.y, transform.position.z);

            var moveDirection = targetPosition - touchStartPosWorld;
            return moveDirection.x * _strafeSpeed;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPickup pickup))
                pickup.OnPickup(gameObject);
        }

        private Vector3 GetMousePosInWorldSpace(Vector3 mousePosition) => mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, 0, mainCamera.transform.position.y));
    }
}
