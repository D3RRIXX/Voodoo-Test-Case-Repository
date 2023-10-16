using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SpaceRails.Game
{
	public class GroundCheck : MonoBehaviour
	{
		[SerializeField] private float _checkRadius = 0.15f;
		[SerializeField] private PlayerMovement _playerMovement;
		[SerializeField] private LayerMask _groundMask;

		private void Reset()
		{
			_playerMovement = GetComponent<PlayerMovement>();
			_groundMask = LayerMask.GetMask("Ground");
		}

		private void Awake()
		{
			this.FixedUpdateAsObservable()
			    .Where(_ => Physics.CheckSphere(transform.position, _checkRadius, _groundMask))
			    .Subscribe(_ => _playerMovement.ChangeMovementType(MovementType.OnFoot))
			    .AddTo(this);
		}
	}
}