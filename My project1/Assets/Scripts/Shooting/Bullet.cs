

using UnityEngine;

namespace LernGame.Shooting
{
	public class Bullet : MonoBehaviour
	{
		private Vector3 _direction;
		private float _flySpeed;
		private float _maxFlyDistance;
		private float _currentFlyDistance;
		public float Damage { get; private set; }

		public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed, float damage)
		{
			_direction = direction;
			_flySpeed = flySpeed;
			_maxFlyDistance = maxFlyDistance;
			Damage = damage;
		}
		protected void Update()
		{
			var delta = _flySpeed * Time.deltaTime;
			_currentFlyDistance += delta;
			transform.Translate(_direction * delta);
			if (_currentFlyDistance >= _maxFlyDistance)
				Destroy(gameObject);
		}
	}
}
