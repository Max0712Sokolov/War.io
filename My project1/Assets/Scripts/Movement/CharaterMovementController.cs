using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LernGame.Movement
{
	[RequireComponent(typeof(CharacterController))]
	public class CharaterMovementController : MonoBehaviour
	{
		private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;
		[SerializeField]
		private float speed = 1.0f;
		[SerializeField]
		private float maxRadiansDelta = 10f;
		public Vector3 MovementDirection { get; set; }
		public Vector3 LookDirection { get; set; }
		private CharacterController characterController;
		protected void Awake()
		{
			characterController = GetComponent<CharacterController>();
		}
		protected void Update()
		{
			Translite();
			if(maxRadiansDelta > 0f && LookDirection != Vector3.zero)
			{
				Rotate();
			}
		}

		private void Translite()
		{
			var delta = MovementDirection * speed * Time.deltaTime;
			characterController.Move(delta);
		}
		private void Rotate()
		{
			var currentLookDirection = transform.rotation * Vector3.forward;
			float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;
			if (sqrMagnitude > SqrEpsilon)
			{
				var newRotation = Quaternion.Slerp(
					transform.rotation, 
					Quaternion.LookRotation(LookDirection, Vector3.up), 
					maxRadiansDelta * Time.deltaTime);
				transform.rotation = newRotation;
			}
		}
	}
}
