﻿using LearnGame.Movement;
using LearnGame.Shooting;
using UnityEngine;

namespace LearnGame.Enemy
{
	public class EnemyTarget
	{
		public GameObject Closest {  get; private set; }
		private readonly Transform _agentTransform;
		private readonly float _viewRadius;
		private readonly PlayerCharater _player;

		private readonly Collider[] _colliders = new Collider[10];
		public EnemyTarget(Transform agent, PlayerCharater player, float viewRadius)
		{
			_agentTransform = agent;
			_viewRadius = viewRadius;
			_player = player;
		}

		public void FindClosest()
		{
			float minDistance = float.MaxValue;
			var count = FindAllTargets(LayerUtils.PickUpMask| LayerUtils.CharaterMask);
			for (int i = 0; i < count; i++)
			{
				var go = _colliders[i].gameObject;
				if (go == _agentTransform.gameObject) continue;
				if(go.tag == TagUtils.PickUpWeaponTag && !_agentTransform.gameObject.GetComponent<ShootingController>().IsWeaponDefault) continue;
				if (go.tag == TagUtils.SpeedBoosterTag && _agentTransform.gameObject.GetComponent<CharaterMovementController>().IsSpeedBoosted) continue;
				var distance = DistanceFromAgetnTo(go);
				if (distance < minDistance)
				{
					minDistance = distance;
					Closest = go;
				}
			}
			if(_player != null && DistanceFromAgetnTo(_player.gameObject) < minDistance)
				Closest = _player.gameObject;
		}
		public float DistanceToClosestFromAgent()
		{
			if (Closest != null)
				return DistanceFromAgetnTo(Closest);
			return float.MaxValue;
		}
		private int FindAllTargets(int layerMask)
		{
			var size = Physics.OverlapSphereNonAlloc(_agentTransform.position, _viewRadius, _colliders, layerMask);
			return size;
		}
		private float DistanceFromAgetnTo(GameObject go) =>( _agentTransform.position - go.transform.position).magnitude;
	}
}
