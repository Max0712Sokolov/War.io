using Unity.VisualScripting;
using UnityEngine;

namespace LernGame.Movement
{
	public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSourse
	{
		private new Camera camera;
		public Vector3 MovementDirection {  get; private set; }

		protected void Awake()
		{
			camera = Camera.main;
		}
		protected void Update()
		{
			var horizontal = Input.GetAxis("Horizontal");
			var vertical = Input.GetAxis("Vertical");
			//MovementDirection = new Vector3(horizontal, 0, vertical);
			var direction = new Vector3(horizontal, 0, vertical);
			direction = camera.transform.rotation * direction;
			direction.y = 0;
			MovementDirection = direction.normalized;
		}
	}

}
