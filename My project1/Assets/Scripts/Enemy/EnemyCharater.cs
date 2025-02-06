using UnityEngine;

namespace LearnGame.Enemy
{
	[RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]

	public class EnemyCharater : BaseCharater
	{
		[SerializeField]
		private float RunChanseFrom1Befor100;
		[SerializeField]
		private float LowHpProzentFrom1Before100;
		public bool IsLowHp { get  => (_health / _MaxHealth) < (LowHpProzentFrom1Before100 / 100f);}
		private float _rand;

		private void Start()
		{
			_rand = Random.Range(0f, 100f);
		}
		public bool Run { get => _rand < RunChanseFrom1Befor100;}
	}

}
