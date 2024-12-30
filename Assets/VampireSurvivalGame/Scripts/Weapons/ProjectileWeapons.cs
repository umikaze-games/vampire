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

	[SerializeField]
	private List<Projectiles> projectiles;

	[SerializeField]
	private List<Enemy> aimedEnemies;
	public GameObject projectile;

	[SerializeField]
	private float timer;
	private float attackRadius;
	[SerializeField]
	private int weaponAmount;
	public LayerMask enemyLayer;
	//public int projectileCountsInScene;

	private float amountMultiplier = 0.2f;
	private float damageMultiplier = 0.5f;
	private float rangeMultiplier = 0.05f;
	private float timeBetweenAttackMultiplier = 0.02f;
	private float durationMultiplier = 0.2f;
	private float speedMultiplier = 0.2f;

	private void Awake()
	{
		projectiles = new List<Projectiles>();
		aimedEnemies = new List<Enemy>();
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
			GenerateProjectile();
			timer = 0;
		}
		AimEnemy();
	}

	private void OnEnable()
	{
		EventHandler.LevelupEndEvent += UpdateWeaponStats;
		EventHandler.RemoveProjectileEvent += OnRemoveProjectileEvent;
		EventHandler.RemoveEnemyEvent += OnRemoveEnemyEvent;
	}

	private void OnDisable()
	{
		EventHandler.LevelupEndEvent -= UpdateWeaponStats;
		EventHandler.RemoveProjectileEvent -= OnRemoveProjectileEvent;
		EventHandler.RemoveEnemyEvent -= OnRemoveEnemyEvent;
	}


	public void UpdateWeaponStats()
	{
		damage =(int) (weapondata.damage*(1+weaponLevel*damageMultiplier));
		transform.localScale = Vector3.one *(1+ weaponLevel*rangeMultiplier);
		timeBetweenSpawn = weapondata.timeBetweenAttack * (1 - weaponLevel * timeBetweenAttackMultiplier);
		timeBetweenSpawn = timeBetweenSpawn >= 0.5 ? timeBetweenSpawn : 0.5f;

		//lifeTime = weapondata.duration * (1 + weaponLevel * durationMultiplier);
		//speed = weapondata.speed*(1+ weaponLevel * speedMultiplier);
		attackRadius=weapondata.range*(1+ weaponLevel * rangeMultiplier);
		weaponAmount= Mathf.RoundToInt(1 + (weaponLevel* amountMultiplier));
		//transform.localPosition =new Vector3(1+weaponLevel * 0.2f,0,0);
	}

	private void AimEnemy()
	{
		if (projectiles.Count <= 0) return;
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 50,enemyLayer);
		foreach (var enemy in enemies)
		{
			Enemy enemyInRange=enemy.GetComponent<Enemy>();
			aimedEnemies.Add(enemyInRange);
		}
		for (int i = projectiles.Count - 1; i >= 0; i--)
		{
			Vector3 dir = aimedEnemies[Random.Range(0, aimedEnemies.Count)].transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			angle -= 90;
			projectiles[i].transform.rotation = Quaternion.Euler(0, 0, angle);
			projectiles[i].movespeed = 3;
			projectiles[i].GetComponent<Collider2D>().enabled = true;
			projectiles.RemoveAt(i);
			Debug.Log("fire");
		}
		aimedEnemies.Clear();
	}

	private void OnRemoveProjectileEvent(Projectiles destroyedProjectile)
	{
		if (projectiles.Contains(destroyedProjectile))
		{
			projectiles.Remove(destroyedProjectile);// 从列表中移除被销毁的飞刀
		}
	}

	private void OnRemoveEnemyEvent(Enemy enemy)
	{
		if (aimedEnemies.Contains(enemy))
		{
			aimedEnemies.Remove(enemy);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}

	private void GenerateProjectile()
	{
	
		while(GetChildProjectileCount() < weaponAmount)
		{
			var instantiatedGameObject = Instantiate(projectile, transform.position, Quaternion.identity, transform);
			Projectiles instantiatedProjectile = instantiatedGameObject.GetComponent<Projectiles>();
			instantiatedProjectile.damage = damage;
			projectiles.Add(instantiatedProjectile);
			//projectileCountsInScene++;
			SetProjectileRotation();
		}

	}
	private void SetProjectileRotation()
	{
		if (projectiles.Count <= 0) return;
		for (int i = 0; i < projectiles.Count; i++)
		{
			if (projectiles[i] == null) continue;
			projectiles[i].transform.rotation = Quaternion.Euler(0, 0, 360*i / projectiles.Count);
		}
	}

	private void RotateProjectiles()
	{
		transform.Rotate(0, 0, 1);
	
	}

	private int GetChildProjectileCount()
	{
		int childCount = transform.childCount;
		return childCount;
	}
}
