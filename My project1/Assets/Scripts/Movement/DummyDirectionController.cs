using UnityEngine;

namespace LernGame.Movement
{
    public class DummyDirectionController : MonoBehaviour, IMovementDirectionSourse
    {
		public Vector3 MovementDirection {  get; private set; }
		protected void Awake()
		{
			MovementDirection = Vector3.zero;
		}
	}
}