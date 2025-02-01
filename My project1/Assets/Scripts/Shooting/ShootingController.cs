using UnityEngine;

namespace LernGame.Shooting
{
	public class ShootingController : MonoBehaviour
	{
		private enum TargetType {Player, Enemy }
		public bool HasTarget => _target != null;
		private Weapon _weapon;
		private float _nextShotTimerSec;
		private GameObject _target;
		private Collider[] _colliders = new Collider[2];

		[SerializeField]
		private TargetType _targetType = TargetType.Enemy;
		private int _mask;
		public Vector3 TargetPosition => _target.transform.position;

		protected void Awake()
		{
			if (_targetType == TargetType.Player)
				_mask = LayerUtils.PlayerMask;
			else if (_targetType == TargetType.Enemy)
				_mask = LayerUtils.EnemyMask;
		}
		protected void Update()
		{
			_target = GetTarget();
			_nextShotTimerSec -= Time.deltaTime;
			if(_nextShotTimerSec < 0)
			{
				if(HasTarget)
					_weapon.Shoot(TargetPosition);
				_nextShotTimerSec = _weapon.ShootFrequenceSec;
			}
		}
		public void SetWeapon(Weapon weaponPrefab, Transform hand)
		{
			if(_weapon != null)			
				Destroy(_weapon.gameObject);			
			_weapon = Instantiate(weaponPrefab, hand);
			_weapon.transform.localPosition = Vector3.zero;
			_weapon.transform.localRotation = Quaternion.identity;
		}
		private GameObject GetTarget()
		{
			GameObject target = null;
			var position = _weapon.transform.position;
			var radius = _weapon.ShootRadius;
			//var mask = LayerUtils.EnemyMask;

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
