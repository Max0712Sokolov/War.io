using LernGame.Movement;
using LernGame.Shooting;

using UnityEngine;

namespace LernGame
{
	[RequireComponent(typeof(CharaterMovementController), typeof(ShootingController))]

	public class BaseCharater : MonoBehaviour
	{
		[SerializeField]
		private Weapon _baseWeaponPrefab;
		[SerializeField]
		private Transform _hand;
		private IMovementDirectionSourse _movementDirectionSourse;
		private CharaterMovementController _charaterMovementController;
		private ShootingController _shootingController;

		[SerializeField]
		private float _health = 2f;

		protected void Awake()
		{
			_charaterMovementController = GetComponent<CharaterMovementController>();
			_movementDirectionSourse = GetComponent<IMovementDirectionSourse>();
			_shootingController = GetComponent<ShootingController>();
		}

		protected void Start()
		{
			_shootingController.SetWeapon(_baseWeaponPrefab, _hand);
		}
		protected void Update()
		{
			var direction = _movementDirectionSourse.MovementDirection;
			var lookDirection = direction;
			if (_shootingController.HasTarget)
				lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
			_charaterMovementController.MovementDirection = direction;
			_charaterMovementController.LookDirection = lookDirection;
			if(_health <= 0)
				Destroy(gameObject);
		}
		protected void OnTriggerEnter(Collider other)
		{
			if (LayerUtils.IsBullet(other.gameObject))
			{
				var bullet = other.gameObject.GetComponent<Bullet>();
				_health -= bullet.Damage;

				Destroy(bullet.gameObject);
			}
		}
	}

}
