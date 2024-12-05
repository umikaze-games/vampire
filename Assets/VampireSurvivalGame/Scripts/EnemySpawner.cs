using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private float spawnInterval=0.05f;
	private float spawnTimer = 0;
	private int spawnMaxCount=100;
	public GameObject enemyPrefab;

	private float spawnXMax=10;
	private float spawnXMin=-10;
	private float spawnYMax=10;
	private float spawnYMin=-10;

	public List<Enemy> spawnEnemyList;
	private void Update()
	{
		spawnTimer += Time.deltaTime;
		if (spawnEnemyList.Count >= spawnMaxCount||spawnTimer<=spawnInterval) return;
		SpawnEnemy();
	}

	private void SpawnEnemy()
	{
		Vector3 spawnPosition = RandomSpawnPosition();
		GameObject spawnedEnemy=Instantiate(enemyPrefab, spawnPosition, Quaternion.identity,transform);
		Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();
		if (enemyComponent != null) {
			spawnEnemyList.Add(enemyComponent);
			spawnTimer = 0;
		}
	
	}

	private Vector3 RandomSpawnPosition()
	{ 
		float spawnX= Random.Range(spawnXMin, spawnXMax);
		if (spawnX<=5&&spawnX>=0)
		{
			spawnX = 5;
		}
		else if (spawnX >= -5 && spawnX <= 0)
		{
			spawnX = -5;
		}
		float spawnY= Random.Range(spawnYMin, spawnYMax);
		if (spawnY <= 5 && spawnY >= 0)
		{
			spawnY = 5;
		}
		else if (spawnY >= -5 && spawnY <= 0)
		{
			spawnY = -5;
		}
		return new Vector3(spawnX, spawnY, 0);
	
	}
}
