using System;
using System.Collections;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace SpaceRails.Game.Player
{
    public class MovementTypeReactiveProperty : ReactiveProperty<MovementType> {}
    
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _strafeSpeed = 5f;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _speedModifierGain = 0.1f;
        [ShowInInspector, ReadOnly] private MovementTypeReactiveProperty _movementType = new();
    
        private Rigidbody _rb;
        private Camera _mainCamera;
        private Vector3 _touchStartScreenPos;

        private Coroutine _activeMovementRoutine;

        public IReadOnlyReactiveProperty<MovementType> MovementType => _movementType;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _activeMovementRoutine = StartCoroutine(MoveForward());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _touchStartScreenPos = Input.mousePosition;
        }

        public void ChangeMovementType(MovementType movementType)
        {
            if (_movementType.Value == movementType)
                return;
            
            _movementType.Value = movementType;
            StopCoroutine(_activeMovementRoutine);

            _activeMovementRoutine = movementType switch
            {
                Player.MovementType.OnFoot => StartCoroutine(MoveForward()),
                Player.MovementType.Rails => StartCoroutine(GrindOnRails()),
                Player.MovementType.FreeFall => null,
                _ => throw new ArgumentOutOfRangeException(nameof(movementType), movementType, null)
            };
        }

        private IEnumerator MoveForward()
        {
            while (true)
            {
                _rb.velocity = new Vector3(GetVelocityX(), _rb.velocity.y, _moveSpeed);
                yield return null;
            }
        }

        private IEnumerator GrindOnRails()
        {
            float speedModifier = 1f;
            while (true)
            {
                _rb.velocity = new Vector3(GetVelocityX(), _rb.velocity.y, _moveSpeed * speedModifier);
                speedModifier += _speedModifierGain * Time.deltaTime;

                yield return null;
            }
        }

        private float GetVelocityX()
        {
            if (!Input.GetMouseButton(0))
                return 0f;
        
            Vector3 touchStartPosWorld = GetMousePosInWorldSpace(_touchStartScreenPos);
            Vector3 touchPosWorld = GetMousePosInWorldSpace(Input.mousePosition);

            var targetPosition = new Vector3(touchPosWorld.x, transform.position.y, transform.position.z);

            Vector3 moveDirection = targetPosition - touchStartPosWorld;

            return moveDirection.x * _strafeSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPickup pickup))
                pickup.OnPickup(gameObject);
        }

        private Vector3 GetMousePosInWorldSpace(Vector3 mousePosition) => _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, 0, _mainCamera.transform.position.y));
    }
}
