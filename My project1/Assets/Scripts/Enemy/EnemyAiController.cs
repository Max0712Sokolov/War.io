using UnityEngine;
using LearnGame.Enemy.States;

namespace LearnGame.Enemy
{
	public class EnemyAiController: MonoBehaviour
	{
		private EnemyStateMachine _stateMachine;
		private EnemyTarget _target;
		[SerializeField]
		private float _viewRadius = 20f;
		protected void Awake()
		{
			var player = FindObjectOfType<PlayerCharater>();
			var enemyDirectionController = GetComponent<EnemyDirectionController>();
			var navMesher = new NavMesher(transform);
			_target = new EnemyTarget(transform, player, _viewRadius);
			_stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target);
		}
		protected void Update()
		{
			_target.FindClosest();
			_stateMachine.Update();
		}
	}
}
