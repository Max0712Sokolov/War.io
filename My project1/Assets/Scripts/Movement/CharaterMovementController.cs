using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LearnGame.Movement
{
	[RequireComponent(typeof(CharacterController))]
	public class CharaterMovementController : MonoBehaviour
	{
		private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;
		[SerializeField]
		private float _speed = 1.0f;
		[SerializeField]
		private float _sprintMultipiler = 2.0f;
		[SerializeField]
		private float _maxRadiansDelta = 10f;
		public Vector3 MovementDirection { get; set; }
		public Vector3 LookDirection { get; set; }
		private CharacterController characterController;
		//private bool speedBoosted = false;
		//public float SpeedMultipiller { get; set; } = 1f;
		public float TimeSpeedBoostSec { private get; set; } = 0f;
		public float SpeedBoostMultipiller {private get; set; } = 1f;
		public bool IsRunning { get; set; }

		protected void Awake()
		{
			characterController = GetComponent<CharacterController>();
		}
		protected void Update()
		{
			Translite();
			if(_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
			{
				Rotate();
			}
		}
		private void Translite()
		{
			var delta = MovementDirection * _speed * Time.deltaTime;
			if (IsRunning)
				delta *= _sprintMultipiler;
			if(TimeSpeedBoostSec > 0f)
			{
				delta *= SpeedBoostMultipiller;
				TimeSpeedBoostSec -= Time.deltaTime;
			}	
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
					_maxRadiansDelta * Time.deltaTime);
				transform.rotation = newRotation;
			}
		}
		
	}
}
