using Unity.Cinemachine;
using UnityEngine;

public class SpinWeapons : Weapon
{
    private float rotateSpeed;
	public int damage;
	private float timeBetweenSpawn;
	private float knockBackForce = 10;
	public float lifeTime;
	public GameObject fireballHolder;


	private float damageMultiplier = 0.5f;
	private float rangeMultiplier = 0.2f;
	private float timeBetweenAttackMultiplier = 0.2f;
	private float durationMultiplier = 0.2f;
	private float speedMultiplier = 0.2f;
	private void Start()
	{
		UpdateWeaponStats();
	}
	private void Update()
	{
		RotateFireball();
	}

	private void OnEnable()
	{
		EventHandler.LevelupEndEvent += UpdateWeaponStats;
	}

	private void OnDisable()
	{
		EventHandler.LevelupEndEvent -= UpdateWeaponStats;
	}
	private void RotateFireball()
	{
		//float time = 0;
		//time += Time.deltaTime;
		fireballHolder.transform.Rotate(0, 0, Time.deltaTime*rotateSpeed);
	}
	public void UpdateWeaponStats()
	{
		damage =(int) (weapondata.damage*(1+weaponLevel*damageMultiplier));
		transform.localScale = Vector3.one * weapondata.range*(1+ weaponLevel*rangeMultiplier);
		timeBetweenSpawn = weapondata.timeBetweenAttack * (1 + weaponLevel * timeBetweenAttackMultiplier);
		lifeTime = weapondata.duration * (1 + weaponLevel * durationMultiplier);
		rotateSpeed = weapondata.speed*(1+ weaponLevel * speedMultiplier);
		transform.localPosition =new Vector3(1+weaponLevel * 0.2f,0,0);
	}



	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			KnockBack(enemy);
			enemy.TakeDamage(damage);

		}
	}

	private void KnockBack(Enemy enemy)
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float dirX = (enemy.transform.position - player.transform.position).x;
		float dirY = (enemy.transform.position - player.transform.position).y;

		Vector2 dir = new Vector2(dirX, dirY).normalized;
		Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
		enemyRb.AddForce(dir * knockBackForce, ForceMode2D.Impulse);
	}

}
