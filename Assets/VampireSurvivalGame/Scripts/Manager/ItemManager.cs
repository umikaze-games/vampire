using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public GameObject experiencePrefab;
	public GameObject coinePrefab;
	public GameObject healthPrefab;
	private int coins;
	private void Awake()
	{
		if (Instance == null)Instance = this;
		else Destroy(this.gameObject);
		coins = 0;
	}
	public void SpawnExperience(Vector3 position,int amount)
    { 
        var spawnedGameobject=Instantiate(experiencePrefab, position,Quaternion.identity);
       Items spawnedItem= spawnedGameobject.GetComponent<Items>();
        if (spawnedItem != null)
        {
			spawnedItem.amount= amount;
        }
	}

	public void AddCoins(int amount)
	{
		coins += amount;
	}

	public int GetCoins()
	{
		return coins;
	}

	internal void SpawnCoins(Vector3 position, int amount)
	{
		var spawnedGameobject = Instantiate(coinePrefab, position, Quaternion.identity);
		Items spawnedItem = spawnedGameobject.GetComponent<Items>();
		if (spawnedItem != null)
		{
			spawnedItem.amount = amount;
		}
	}
}
