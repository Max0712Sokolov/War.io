using LearnGame.FSM;
using System.Collections.Generic;

namespace LearnGame.Enemy.States
{
	internal class EnemyStateMachine:BaseStateMachine
	{
		private const float NavMeshTurnOffDistance = 5f;
		public EnemyStateMachine(EnemyDirectionController enemyDirectionController, NavMesher navMesher, EnemyTarget target) 
		{
			var idleState = new IdleState();
			var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
			var moveForwardState = new MoveForwardState(target, enemyDirectionController);
			var RunAwayState = new RunAwayState(target, enemyDirectionController);
			var enemy = enemyDirectionController.gameObject.GetComponent<EnemyCharater>();

			SetInitialState(idleState);

			AddState(state: idleState, transitions: new List<Transition>
			{
				new Transition(findWayState,() => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
				new Transition(moveForwardState, ()=> target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
				new Transition(RunAwayState, () => enemy.IsLowHp && enemy.Run && LayerUtils.IsChatater(target.Closest)),
			}
			);
			AddState(state: findWayState, transitions: new List<Transition>
			{
				new Transition(idleState,() => target.Closest == null),
				new Transition(moveForwardState, ()=> target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
			}
			);
			AddState(state: moveForwardState, transitions: new List<Transition>
			{
				new Transition(findWayState,() => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
				new Transition(idleState, () => target.Closest == null),
				new Transition(RunAwayState,() => enemy.IsLowHp && enemy.Run && LayerUtils.IsChatater(target.Closest)),

			}
			);AddState(state: RunAwayState, transitions: new List<Transition>
			{
				new Transition(idleState,() => !LayerUtils.IsChatater(target.Closest)),
			}
			);
		}
	}
}
