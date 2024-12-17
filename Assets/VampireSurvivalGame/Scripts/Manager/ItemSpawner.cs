using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;
    public GameObject experiencePrefab;
	public GameObject coinePrefab;
	public GameObject healthPrefab;

	private void Awake()
	{
		if (Instance == null)Instance = this;
		else Destroy(this.gameObject);
	}
	public void SpawnExperience(Vector3 position,int value)
    { 
        var spawnedGameobject=Instantiate(experiencePrefab, position,Quaternion.identity);
       Items spawnedItem= spawnedGameobject.GetComponent<Items>();
        if (spawnedItem != null)
        {
			spawnedItem.amount=value;
        }
	}
}
