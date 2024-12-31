using Unity.VisualScripting;
using UnityEngine;

public class TrowingWeapons : Weapon
{
	private int damage;
	private float timeBetweenSpawn;
	public GameObject axePrefab;

	[SerializeField]
	private float timer;
	[SerializeField]
	private int weaponAmount;

	private float amountMultiplier = 0.5f;
	private float damageMultiplier = 0.5f;
	private float rangeMultiplier = 0.1f;
	private float timeBetweenAttackMultiplier = 0.1f;
	private float durationMultiplier = 0.2f;
	private float speedMultiplier = 0.2f;

	[SerializeField]
	private float trowForce = 1;
	private void Awake()
	{

	}
	private void Start()
	{
		UpdateWeaponStats();
	}
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= timeBetweenSpawn)
		{
			GenerateTrowingWeapons();
			timer = 0;
		}
	}

	private void OnEnable()
	{
		EventHandler.LevelupEndEvent += UpdateWeaponStats;
	}

	private void OnDisable()
	{
		EventHandler.LevelupEndEvent -= UpdateWeaponStats;
	}


	public void UpdateWeaponStats()
	{
		damage = (int)(weapondata.damage * (1 + weaponLevel * damageMultiplier));
		transform.localScale = Vector3.one * (1 + weaponLevel * rangeMultiplier);
		timeBetweenSpawn = weapondata.timeBetweenAttack * (1 - weaponLevel * timeBetweenAttackMultiplier);
		timeBetweenSpawn = timeBetweenSpawn >= 1 ? timeBetweenSpawn : 1;

		//lifeTime = weapondata.duration * (1 + weaponLevel * durationMultiplier);
		//speed = weapondata.speed*(1+ weaponLevel * speedMultiplier);
		//attackRadius = weapondata.range * (1 + weaponLevel * rangeMultiplier);
		weaponAmount = Mathf.RoundToInt(1 + (weaponLevel * amountMultiplier));
		//transform.localPosition =new Vector3(1+weaponLevel * 0.2f,0,0);
	}
	private void GenerateTrowingWeapons()
	{

		for (int i = 0; i < weaponAmount; i++)
		{
			var instantiatedGameObject = Instantiate(axePrefab, new Vector3(transform.position.x,(float)(transform.position.y),transform.position.z), Quaternion.identity);
			Axe instantiatedAxe = instantiatedGameObject.GetComponent<Axe>();
			instantiatedAxe.damage = damage;
			Rigidbody2D axeRigbody2D=instantiatedAxe.GetComponent<Rigidbody2D>();
			float forceAngle=Random.Range(45, 135);
			float radians = forceAngle * Mathf.Deg2Rad;
			Vector2 forceDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
			axeRigbody2D.AddForce(forceDirection* trowForce, ForceMode2D.Impulse);
			//instantiatedAxe.transform.rotation = Quaternion.Euler(0, 0, 360 * i / weaponAmount);
			//float radians = (360 * i / weaponAmount) * Mathf.Deg2Rad;
			//instantiatedSword.transform.position = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
		}

	}
}
