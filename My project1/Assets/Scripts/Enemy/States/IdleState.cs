using LearnGame.FSM;
using UnityEngine;


namespace LearnGame.Enemy.States
{
	public class IdleState : BaseState
	{
		public override void Execute()
		{
			Debug.Log("idle");
		}
	}
}
