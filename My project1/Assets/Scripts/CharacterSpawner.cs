using LearnGame;
using LearnGame.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
	[SerializeField]
	private PlayerCharater _playerChataterPrefab;
	[SerializeField]
	private EnemyCharater _enemyChataterPrefab;
	[SerializeField]
	private float _range = 5f;
	[SerializeField]
	private float _spawnIntervalSecondsMin = 2f;
	[SerializeField]
	private float _spawnIntervalSecondsMax = 10f;
	[SerializeField]
	CharacterSpawnerManager _spawnerManager;
	//private static int _maxEnemyCount = 5; //  общее колличесво врагов на сцене
	private static int _currentEnemyCount;
	private float _spawnIntervalSeconds;
	//private static bool _isPlayerSpawned = false;
	private bool _isFirstPlayerSpawner = false;



	private float _currentSpawnTimerSeconds;
	protected void Awake()
	{
		_spawnIntervalSeconds = Random.Range(_spawnIntervalSecondsMin, _spawnIntervalSecondsMax);
	}

	protected void Start()
	{
		if(_isFirstPlayerSpawner)
			SpawnPlayer();
	}

	protected void Update()
	{
		if (_currentSpawnTimerSeconds < _spawnIntervalSeconds)
			_currentSpawnTimerSeconds += Time.deltaTime;
		else
		{
			if( _spawnerManager.IsPlayerSpawned ) SpawnEnemy();
				else
				{
					if(Random.Range(0, 2) ==  0)
						SpawnEnemy();
						else
							SpawnPlayer();
				}
			_currentSpawnTimerSeconds = 0f;			
		}
	}
	private void SpawnEnemy()
	{
		if (_currentEnemyCount < _spawnerManager.MaxEnemyCount)
		{			
			_currentEnemyCount++;

			var randomPointInsideRange = Random.insideUnitCircle * _range;
			var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

			var Enemy = Instantiate(_enemyChataterPrefab, randomPosition, Quaternion.identity, transform);
			Enemy.Killed += OnEnemyKilled;
			_spawnIntervalSeconds = Random.Range(_spawnIntervalSecondsMin, _spawnIntervalSecondsMax);			
		}
	}
	public void SpawnPlayer()
	{
		_spawnerManager.IsPlayerSpawned = true;
		var randomPointInsideRange = Random.insideUnitCircle * _range;
		var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

		var Player = Instantiate(_playerChataterPrefab, randomPosition, Quaternion.identity, transform);
		Player.Killed += OnPlayerKilled;
		_spawnIntervalSeconds = Random.Range(_spawnIntervalSecondsMin, _spawnIntervalSecondsMax);
	}
	private void OnEnemyKilled(BaseCharater enemy)
	{
		enemy.Killed -= OnEnemyKilled;
		_currentEnemyCount--;
	}
	private void OnPlayerKilled(BaseCharater palyer)
	{
		palyer.Killed -= OnPlayerKilled;
		_spawnerManager.IsPlayerSpawned = false;
	}

	public void SetFirstPlayerSpawner()
	{
		_isFirstPlayerSpawner = true;
	}
	protected void OnDrawGizmos()
	{
		var cashedColor = Handles.color;
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position, Vector3.up, _range);
		Handles.color = cashedColor;
	}
}
