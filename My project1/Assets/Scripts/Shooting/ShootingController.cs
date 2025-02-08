using UnityEngine;

namespace LearnGame.Shooting
{
	public class ShootingController : MonoBehaviour
	{
		public bool HasTarget => _target != null;
		public Weapon Weapon { get; private set; }
		private float _nextShotTimerSec = 0f;
		private GameObject _target;
		private Collider[] _colliders = new Collider[2];
		public bool IsWeaponDefault { get; private set; } = true;

		private int _mask;
		public Vector3 TargetPosition => _target.transform.position;

		protected void Awake()
		{
			_mask = LayerUtils.PlayerMask | LayerUtils.EnemyMask;		
		}
		protected void Update()
		{
			_target = GetTarget();
			if(_nextShotTimerSec > 0f)
				_nextShotTimerSec -= Time.deltaTime;
			if(_nextShotTimerSec <= 0)
			{
				if(HasTarget)
				{
					Weapon.Shoot(TargetPosition);
					_nextShotTimerSec = Weapon.ShootFrequenceSec;
				}
			}
		}
		public void SetWeapon(Weapon weaponPrefab, Transform hand)
		{
			if(Weapon != null)
			{
				Destroy(Weapon.gameObject);			
				IsWeaponDefault = false;
			}
			Weapon = Instantiate(weaponPrefab, hand);
			Weapon.transform.localPosition = Vector3.zero;
			Weapon.transform.localRotation = Quaternion.identity;
			_nextShotTimerSec = 0f;
		}
		private GameObject GetTarget()
		{
			GameObject target = null;
			var position = Weapon.transform.position;
			var radius = Weapon.ShootRadius;

			var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, _mask);
			if (size > 0)
			{
				for(int i = 0; i < size; i++)
				{
					if (_colliders[i].gameObject != gameObject)
					{
						target = _colliders[i].gameObject;
						break;
					}
				}
			}
			return target;
		}
	}
}
