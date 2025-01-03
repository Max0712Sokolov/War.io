using LernGame;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharater player;
	[SerializeField]
	private Vector3 followCameraOffset = Vector3.zero;
	[SerializeField]
	private Vector3 rotationOffset = Vector3.zero;


	protected void Awake()
	{
		if (player == null)
			throw new NullReferenceException($"Follow camera can't follow null player - {nameof(player)}");
	}

	void LateUpdate()
    {
		Vector3 targetRotation = rotationOffset - followCameraOffset;
		transform.position = player.transform.position + followCameraOffset;
		transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
    }
}
