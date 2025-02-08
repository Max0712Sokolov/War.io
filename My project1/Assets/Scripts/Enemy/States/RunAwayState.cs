using LearnGame.FSM;
using UnityEngine;

namespace LearnGame.Enemy.States
{
	public class RunAwayState : BaseState
	{
		private readonly EnemyTarget _danger;
		private readonly EnemyDirectionController _enemyDirectionController;

		private Vector3 _currentPoint;
		public RunAwayState(EnemyTarget target, EnemyDirectionController enemyDirectionController)
		{
			_danger = target;
			_enemyDirectionController = enemyDirectionController;
		}
		public override void Execute()
		{
			Vector3 targetPosition = _danger.Closest.transform.position;

			if (_currentPoint != targetPosition)
			{
				_currentPoint = -targetPosition;
				_enemyDirectionController.UpdateMovementDirection(targetPosition);
				_enemyDirectionController.Run();
			}
		}
	}
}
