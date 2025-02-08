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

		public void UpdateMovementDirection(Vector3 targetPosition)
		{
			var realDirection = targetPosition - transform.position;
			MovementDirection = new Vector3 (realDirection.x, 0, realDirection.z).normalized;
		}
	}
}
