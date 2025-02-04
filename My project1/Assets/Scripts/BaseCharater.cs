using LearnGame.PickUp;
using LearnGame.Movement;
using LearnGame.Shooting;

using UnityEngine;

namespace LearnGame
{
	[RequireComponent(typeof(CharaterMovementController), typeof(ShootingController))]

	public abstract class BaseCharater : MonoBehaviour
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
			SetWeapon(_baseWeaponPrefab);
		}
		protected void Update()
		{
			var direction = _movementDirectionSourse.MovementDirection;
			var lookDirection = direction;
			if (_shootingController.HasTarget)
				lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
			_charaterMovementController.MovementDirection = direction;
			_charaterMovementController.LookDirection = lookDirection;
			_charaterMovementController.IsRunning = _movementDirectionSourse.IsRunning;
			if (_health <= 0)
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
			else if (LayerUtils.IsPickUp(other.gameObject))
			{
				var pickUp = other.gameObject.GetComponent<PickUpItem>();
				pickUp.PickUp(this);
				Destroy(pickUp.gameObject);
			}
		}
		public void SetWeapon(Weapon weapon)
		{
			_shootingController.SetWeapon(weapon, _hand);
		}
		public void SpeedBoost(float multipiller, float timeSec)
		{
			_charaterMovementController.TimeSpeedBoostSec = timeSec;
			_charaterMovementController.SpeedBoostMultipiller = multipiller;
		}
	}

		
}


