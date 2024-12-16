using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private Player player;
	//private float spawnInterval=0.05f;
	private float spawnCount;
	//private int spawnMaxCount=100;
	public GameObject enemyPrefab;

	private float spawnXMax=10;
	private float spawnXMin=-10;
	private float spawnYMax=10;
	private float spawnYMin=-10;

	public List<Enemy> spawnEnemyList;
	public List<EnemyWave> waves;
	private int currentWave=0;
	private float waveCounter;

	private void Awake()
	{
		player = FindAnyObjectByType<Player>();
		waveCounter=waves[currentWave].waveLength;
		spawnCount = waves[currentWave].spawnTime;
	}
	private void Update()
	{
		spawnCount -= Time.deltaTime;
		SpawnEnemy();
	}

	private void SpawnEnemy()
	{
		//Vector3 spawnPosition = RandomSpawnPosition();
		//GameObject spawnedEnemy=Instantiate(enemyPrefab, spawnPosition, Quaternion.identity,transform);
		//Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();
		//if (enemyComponent != null) {
		//	spawnEnemyList.Add(enemyComponent);
		//	spawnTimer = 0;
		//}

		if (player.isActiveAndEnabled)
		{
			if (currentWave<waves.Count)
			{
				waveCounter-=Time.deltaTime;
				if (waveCounter<=0)
				{
					NextWave();
				}
			}
			if (spawnCount<=0)
			{
				Vector3 spawnPosition = RandomSpawnPosition();
				GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
				Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();
				if (enemyComponent != null)
				{
					spawnEnemyList.Add(enemyComponent);
				}
				//spawnCount=waves[currentWave].spawnTime;
			}
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

	private void NextWave()
	{ 
		currentWave++;
		if (currentWave>waves.Count-1)
		{
			currentWave = waves.Count - 1;
		}
		enemyPrefab=waves[currentWave].enemyToSpawn;
		waveCounter = waves[currentWave].waveLength;
		spawnCount = waves[currentWave].spawnTime;
	}
}

[System.Serializable]
public class EnemyWave
{
	public GameObject enemyToSpawn;
	public float spawnTime=0.2f;
	public float waveLength=5;
}