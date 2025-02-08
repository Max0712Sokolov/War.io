using LearnGame;
using LearnGame.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharaterSpawner : MonoBehaviour
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
	private static int _maxEnemyCount = 5; //  общее колличесво врагов на сцене
	private static int _currentEnemyCount;
	private float _spawnIntervalSeconds;
	private static bool _isPlayerSpawned = false;


	private float _currentSpawnTimerSeconds;
	protected void Awake()
	{
		_spawnIntervalSeconds = Random.Range(_spawnIntervalSecondsMin, _spawnIntervalSecondsMax);
	}

	protected void Update()
	{
		if (_currentSpawnTimerSeconds < _spawnIntervalSeconds)
			_currentSpawnTimerSeconds += Time.deltaTime;
		else
		{
			if( _isPlayerSpawned ) SpawnChatater(_enemyChataterPrefab);
				else
				{
					if(Random.Range(0, 2) ==  0)
						SpawnChatater(_enemyChataterPrefab);
						else
							SpawnCharater(_playerChataterPrefab);
				}
			_currentSpawnTimerSeconds = 0f;			
		}
	}
	private void SpawnChatater(EnemyCharater enemyPrefab)
	{
		if (_currentEnemyCount < _maxEnemyCount)
		{			
			_currentEnemyCount++;

			var randomPointInsideRange = Random.insideUnitCircle * _range;
			var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

			var Enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity, transform);
			Enemy.Killed += OnEnemyKilled;
			_spawnIntervalSeconds = Random.Range(_spawnIntervalSecondsMin, _spawnIntervalSecondsMax);			
		}
	}
	private void SpawnCharater(PlayerCharater playerPrefab)
	{
		_isPlayerSpawned = true;
		var randomPointInsideRange = Random.insideUnitCircle * _range;
		var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

		var Player = Instantiate(playerPrefab, randomPosition, Quaternion.identity, transform);
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
		_isPlayerSpawned = false;
	}
	protected void OnDrawGizmos()
	{
		var cashedColor = Handles.color;
		Handles.color = Color.red;
		Handles.DrawWireDisc(transform.position, Vector3.up, _range);
		Handles.color = cashedColor;
	}
}
