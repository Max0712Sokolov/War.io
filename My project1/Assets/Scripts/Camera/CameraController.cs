using LearnGame;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharater _player;
	[SerializeField]
	private Vector3 followCameraOffset = Vector3.zero;
	[SerializeField]
	private Vector3 rotationOffset = Vector3.zero;


	protected void Awake()
	{
		if (_player == null)
			throw new NullReferenceException($"Follow camera can't follow null player - {nameof(_player)}");
	}

	void LateUpdate()
    {
		if(_player != null)
		{
			Vector3 targetRotation = rotationOffset - followCameraOffset;
			transform.position = _player.transform.position + followCameraOffset;
			transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
		}
    }
}
