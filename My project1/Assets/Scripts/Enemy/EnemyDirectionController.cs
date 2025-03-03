using UnityEngine;
using LearnGame.Movement;

namespace LearnGame.Enemy
{
	public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSourse
	{
		public Vector3 MovementDirection {  get; private set; }

		public bool IsRunning { get; private set; } = false;

		public void Run()
		{
			IsRunning = true;
		}
		public void DontRun()
		{
			IsRunning = false;
		}

		public void UpdateMovementDirection(Vector3 targetPosition, bool invert = false)
		{
			var realDirection = targetPosition - transform.position;
			Vector3 movementDirection;
			if (invert)
				movementDirection = - new Vector3(realDirection.x, 0, realDirection.z).normalized;
			else
				movementDirection = new Vector3(realDirection.x, 0, realDirection.z).normalized;
			MovementDirection = movementDirection;
		}
	}
}
