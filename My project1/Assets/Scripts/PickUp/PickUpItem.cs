using UnityEngine;
using System;
using LernGame;

namespace LearnGame.PickUp
{
	public abstract class PickUpItem : MonoBehaviour
	{
		public event Action<PickUpItem> OnPickedUp;

		public virtual void PickUp(BaseCharater charater)
		{
			OnPickedUp?.Invoke(this);
		}
	}
}
