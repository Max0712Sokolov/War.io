﻿using UnityEngine;

namespace LernGame
{
	public static class LayerUtils
	{
		public const string BulletLayerName = "Bullet";
		public const string EnemyLayerName = "Enemy";
		public const string PlayerLayerName = "Player";

		public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);

		public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName);

		public static bool IsBullet(GameObject other) => other.layer == BulletLayer;

	}
}