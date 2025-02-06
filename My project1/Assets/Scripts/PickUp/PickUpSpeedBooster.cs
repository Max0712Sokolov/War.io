using LearnGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame.PickUp
{
    public class PickUpSpeedBooster : PickUpItem
    {
		[SerializeField]
		private float _speedMultipiller;
		[SerializeField]
		private float _timeSec;
		public override void PickUp(BaseCharater charater)
		{
			base.PickUp(charater);
			charater.SpeedBoost(_speedMultipiller, _timeSec);
		}
	}

}
