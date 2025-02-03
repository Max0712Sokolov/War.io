using LernGame;
using LernGame.Shooting;
using UnityEngine;

namespace LearnGame.PickUp
{
	public class PickUpWeapon : PickUpItem
	{
		[SerializeField]
		private Weapon _weaponPrefab;

		public override void PickUp(BaseCharater charater)
		{
			base.PickUp(charater);
			charater.SetWeapon(_weaponPrefab);
		}
	}
}
