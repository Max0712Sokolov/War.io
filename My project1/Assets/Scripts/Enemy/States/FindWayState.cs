using LearnGame.FSM;
using UnityEngine;

namespace LearnGame.Enemy.States
{
	public class FindWayState : BaseState
	{
		private const float MaxDistanceBetweeRealyAndCalculated = 3f;
		private readonly EnemyTarget _target;
		private readonly NavMesher _navMesher;
		private readonly EnemyDirectionController _enemyDirectionController;

		private Vector3 _currentPoint;

		public FindWayState(EnemyTarget target, NavMesher navMesher, EnemyDirectionController enemyDirectionController)
		{
			_target = target;
			_navMesher = navMesher;
			_enemyDirectionController = enemyDirectionController;
		}

		public override void Execute()
		{
			Debug.Log("find way");
			Vector3 targetPosition = _target.Closest.transform.position;
			if(!_navMesher.IsPathCalculated || _navMesher.DistanceToTargetPointFrom(targetPosition) >
				MaxDistanceBetweeRealyAndCalculated)			
				_navMesher.CalculatePath(targetPosition);
			var currentPoint = _navMesher.GetCurrentPoint();
			if(_currentPoint != currentPoint)
			{
				_currentPoint = currentPoint;
				_enemyDirectionController.UpdateMovementDirection(currentPoint);
			}
				_enemyDirectionController.DontRun();

		}
	}
}
