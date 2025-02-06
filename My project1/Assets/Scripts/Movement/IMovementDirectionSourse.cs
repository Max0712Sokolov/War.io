using UnityEngine;

namespace LearnGame.Movement
{
	public interface IMovementDirectionSourse
	{
		Vector3 MovementDirection { get; }
		bool IsRunning {  get; }
	}
}
