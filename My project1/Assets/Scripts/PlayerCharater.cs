using LernGame.Movement;
using LernGame.Shooting;

using UnityEngine;

namespace LernGame
{
	[RequireComponent(typeof(CharaterMovementController), typeof(ShootingController))]

	public class PlayerCharater : MonoBehaviour
	{
		[SerializeField]
		private Weapon _baseWeaponPrefab;
		[SerializeField]
		private Transform _hand;
		private IMovementDirectionSourse _movementDirectionSourse;
		private CharaterMovementController _charaterMovementController;
		private ShootingController _shootingController;

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
			var ditection = _movementDirectionSourse.MovementDirection;
			_charaterMovementController.Direction = ditection;
		}
	}

}
