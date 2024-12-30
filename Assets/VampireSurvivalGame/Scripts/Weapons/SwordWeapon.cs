using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon
{
	private int damage;
	private float timeBetweenSpawn;
	public GameObject swordPrefab;

	[SerializeField]
	private float timer;
	[SerializeField]
	private int weaponAmount;

	private float amountMultiplier = 0.5f;
	private float damageMultiplier = 0.5f;
	private float rangeMultiplier = 0.05f;
	private float timeBetweenAttackMultiplier = 0.1f;
	private float durationMultiplier = 0.2f;
	private float speedMultiplier = 0.2f;

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
			GenerateSword();
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
		//transform.localScale = Vector3.one * (1 + weaponLevel * rangeMultiplier);
		timeBetweenSpawn = weapondata.timeBetweenAttack * (1 - weaponLevel * timeBetweenAttackMultiplier);
		timeBetweenSpawn = timeBetweenSpawn >= 1 ? timeBetweenSpawn : 1;

		//lifeTime = weapondata.duration * (1 + weaponLevel * durationMultiplier);
		//speed = weapondata.speed*(1+ weaponLevel * speedMultiplier);
		//attackRadius = weapondata.range * (1 + weaponLevel * rangeMultiplier);
		weaponAmount = Mathf.RoundToInt(1 + (weaponLevel * amountMultiplier));
		//transform.localPosition =new Vector3(1+weaponLevel * 0.2f,0,0);
	}
	private void GenerateSword()
	{

		for(int i=0; i<weaponAmount; i++)
		{
			var instantiatedGameObject = Instantiate(swordPrefab, transform.position, Quaternion.identity, transform);
			Sword instantiatedSword = instantiatedGameObject.GetComponent<Sword>();
			instantiatedSword.damage = damage;
			instantiatedSword.transform.rotation = Quaternion.Euler(0, 0, 360 * i / weaponAmount);
			//float radians = (360 * i / weaponAmount) * Mathf.Deg2Rad;
			//instantiatedSword.transform.position = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
		}

	}
}

