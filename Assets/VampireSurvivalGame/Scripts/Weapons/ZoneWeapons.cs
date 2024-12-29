using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ProjectileWeapons : Weapon
{

	public int damage;
	private float timeBetweenSpawn;
	public float lifeTime;
	public GameObject rotationHolder;

	[SerializeField]
	private float timer;
	private float lifeTimer;

	public SpriteRenderer projectileImage;
	public Collider2D projectileCollider;

	private float damageMultiplier = 0.5f;
	private float rangeMultiplier = 0.05f;
	private float timeBetweenAttackMultiplier = 0.2f;
	private float durationMultiplier = 0.2f;


	private List<Enemy> enemiesInZone = new List<Enemy>(); // 存储区域内的敌人
	private void Start()
	{
		UpdateWeaponStats();
	}
	private void Update()
	{
		timer += Time.deltaTime;
		rotationHolder.transform.Rotate(0, 0, Time.deltaTime * 100f);
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
		transform.localScale = Vector3.one * weapondata.range * (1 + weaponLevel * rangeMultiplier);
		timeBetweenSpawn = weapondata.timeBetweenAttack * (1 - weaponLevel * timeBetweenAttackMultiplier);
		timeBetweenSpawn=timeBetweenSpawn >= 0 ? timeBetweenSpawn : 0.1f ;
		lifeTime = weapondata.duration * (1 + weaponLevel * durationMultiplier);
	}



	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemiesInZone.Add(enemy);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemiesInZone.Remove(enemy);
		}
	}

}
