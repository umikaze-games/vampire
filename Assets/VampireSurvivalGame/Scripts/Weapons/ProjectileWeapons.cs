using System.Collections.Generic;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public class ProjectileWeaponsWeapons : Weapon
{
    private float speed;
	private int damage;
	[SerializeField]
	private float timeBetweenSpawn;
	//private float lifeTime;

	private List<Projectiles> projectiles;
	public GameObject projectile;

	[SerializeField]
	private float timer;
	private float attackRadius;
	[SerializeField]
	private int weaponAmount;
	public LayerMask enemyLayer;

	private float amountMultiplier = 0.2f;
	private float damageMultiplier = 0.5f;
	private float rangeMultiplier = 0.05f;
	private float timeBetweenAttackMultiplier = 0.1f;
	private float durationMultiplier = 0.2f;
	private float speedMultiplier = 0.2f;

	private void Awake()
	{
		projectiles = new List<Projectiles>();
	}
	private void Start()
	{
		UpdateWeaponStats();
	}
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer>= timeBetweenSpawn)
		{
			InstantiateProjectiles(weaponAmount);
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
		damage =(int) (weapondata.damage*(1+weaponLevel*damageMultiplier));
		transform.localScale = Vector3.one * weapondata.range*(1+ weaponLevel*rangeMultiplier);
		timeBetweenSpawn = weapondata.timeBetweenAttack * (1 - weaponLevel * timeBetweenAttackMultiplier);
		timeBetweenSpawn = timeBetweenSpawn >= 0.2 ? timeBetweenSpawn : 0.2f;

		//lifeTime = weapondata.duration * (1 + weaponLevel * durationMultiplier);
		speed = weapondata.speed*(1+ weaponLevel * speedMultiplier);
		attackRadius=weapondata.range*(1+ weaponLevel * rangeMultiplier);
		weaponAmount= Mathf.RoundToInt(1 + (weaponLevel* amountMultiplier));
		//transform.localPosition =new Vector3(1+weaponLevel * 0.2f,0,0);
	}

	public void InstantiateProjectiles(int weaponAmount)
	{
		for (int i = 0; i < weaponAmount; i++)
		{
			float angle = 360 / (weaponAmount * (1+i));
			var instantiatedGameObject = Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward), transform);
			Projectiles instantiatedProjectile = instantiatedGameObject.GetComponent<Projectiles>();
			instantiatedProjectile.damage = damage;
			//instantiatedProjectile.lifeTime = lifeTime;
			//instantiatedProjectile.speed = speed;
			projectiles.Add(instantiatedProjectile);
		}
		AimEnemy();
	}

	private void AimEnemy()
	{
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius,enemyLayer);
		if (enemies==null) return;
		for (int i = 0; i < weaponAmount; i++)
		{
			if (i >= projectiles.Count) break; // 检查索引是否有效
			if (projectiles[i] == null) continue; // 检查对象是否已被销毁
			Vector3 dir = enemies[Random.Range(0, enemies.Length)].transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			angle -= 90;
			projectiles[i].transform.rotation = Quaternion.Euler(0, 0, angle);
			projectiles[i].speed = speed;
			projectiles[i].GetComponent<Collider2D>().enabled = true;
			projectiles.RemoveAt(i);
		}
		
	}


}
