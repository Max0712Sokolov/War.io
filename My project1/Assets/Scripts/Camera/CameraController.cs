using LearnGame;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private PlayerCharater _player;
	[SerializeField]
	private Vector3 followCameraOffset = Vector3.zero;
	[SerializeField]
	private Vector3 rotationOffset = Vector3.zero;
	[SerializeField]
	private Transform _defaultTransform;


	void LateUpdate()
	{
		if(_player != null)
		{
			Vector3 targetRotation = rotationOffset - followCameraOffset;
			transform.position = _player.transform.position + followCameraOffset;
			transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
		}
		else
		{
			_player = FindObjectOfType<PlayerCharater>();
			transform.position = _defaultTransform.position;
			transform.rotation = _defaultTransform.rotation;
		}
	}
}
