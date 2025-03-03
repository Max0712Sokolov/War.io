using UnityEngine;

public class CharacterSpawnerManager : MonoBehaviour
{
	[field:SerializeField]
	public int MaxEnemyCount { get; private set; } = 5;
	public bool IsPlayerSpawned { get;  set; } = false;
	
	[SerializeField]
	CharacterSpawner[] playerSpawners;

	protected void Awake()
	{
		playerSpawners[Random.Range(0, playerSpawners.Length)].SetFirstPlayerSpawner();
	}
}

