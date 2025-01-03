using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,ICharacter
{
	protected GameObject player;
	protected Animator animator;
	private Rigidbody2D rb;
	public CharacterStats enemyStats;
	private EnemyUI enemyUI;
	private SpriteRenderer spriteRenderer;

	private float atkCD;
	private int atk;
	protected float moveSpeed;

	private float timer = 0;

	public int maxHealth;
	public int currentHealth;
	public int strength;
	public int vitality;
	public int dexterity;
	protected virtual void Awake()
	{
		spriteRenderer=GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		player = GameObject.FindWithTag("Player");
		initStats();
		enemyUI=GetComponent<EnemyUI>();
	}
	private void Start()
	{
		moveSpeed = dexterity/5;
		atk = strength;
		atkCD=dexterity/5;
	}
	private void Update()
	{
		if (Player.Instance.IsGameOver==true) return;
		Chase(player.transform.position);
		timer -= Time.deltaTime;
	}

	protected virtual void Chase(Vector3 position)
	{
		if (Vector3.Distance(player.transform.position, transform.position) <= 0.1)
		{
			animator.SetBool("IsMoving", false);

			return;
		}

		Vector3 dir = new Vector3(position.x - transform.position.x, position.y - transform.position.y, 0).normalized;
		transform.position += dir * moveSpeed * Time.deltaTime;
		if (dir != Vector3.zero)
		{
			animator.SetBool("IsMoving", true);
			if (dir.x > 0)
			{
				spriteRenderer.flipX = true;
			}
			else
			{
				spriteRenderer.flipX = false;
			}
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		float healtAmount = (float)currentHealth / (float)maxHealth;
		enemyUI.UpdateEnemyHealthUI(healtAmount);
		EventHandler.CallShowDamageUIEvent(this, damage);

		EventHandler.CallPlaySFXEvent(SFXName.EnemyHit);

		if (currentHealth <= 0)
		{
			currentHealth = 0;

			Die();
			SpawnExperience();
			SpawnCoins();
		}
	
	}

	public void initStats()
	{
		maxHealth=enemyStats.initHealth;
		currentHealth =maxHealth;
		strength = enemyStats.initStrength;
		vitality = enemyStats.initVitality;
		dexterity = enemyStats.iniDexterity;
	}
	public void Die()
	{
		EventHandler.CallRemoveEnemyEvent(this);
		Destroy(gameObject);
		EventHandler.CallPlaySFXEvent(SFXName.EnemyDeath);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (timer >0) return;
		Player player= collision.GetComponent<Player>();
		if (player!=null)
		{
			player.TakeDamage(atk);
			timer=atkCD;
		}
	}

	public void SpawnExperience()
	{
		ItemManager.Instance.SpawnExperience(transform.position, enemyStats.dropExp);
	}

	public void SpawnCoins()
	{
		int randomRate=Random.Range(0,100);
		if (randomRate>=50)
		{
			ItemManager.Instance.SpawnCoins(transform.position, enemyStats.dropCoin);
		}
	}

	public bool IsAlive()
	{
		return currentHealth > 0;
	}

}
