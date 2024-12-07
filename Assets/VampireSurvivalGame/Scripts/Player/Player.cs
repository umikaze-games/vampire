using System;
using UnityEngine;

public class Player : MonoBehaviour,ICharacter
{
	[SerializeField]
	private float moveSpeed;
	private float moveX;
	private float moveY;
	private Animator playerAnimator;
	public CharacterStats playerStats;

	public int maxHealth;
	public int currentExp;
	public int currentHealth;
	public int level;
	public int strength;
	public int vitality;
	public int dexterity;
	private void Awake()
	{
		initStats();
	}
	private void Start()
	{
		playerAnimator = GetComponent<Animator>();
	}
	private void Update()
	{
		PlayerMove();
	}
	private void OnEnable()
	{
		EventHandler.PlayerDieEvent += Die;
	}

	private void OnDisable()
	{
		EventHandler.PlayerDieEvent -= Die;
	}
	private void PlayerMove()
	{
		moveX=Input.GetAxisRaw("Horizontal");
		moveY=Input.GetAxisRaw("Vertical");
		Vector3 dir = new Vector3(moveX, moveY, 0).normalized;
		transform.position += dir * moveSpeed * Time.deltaTime;
		if (dir.magnitude>0.1)
		{
			playerAnimator.SetBool("IsMoving", true);
		}
		else playerAnimator.SetBool("IsMoving", false);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		float healthAmount = (float)currentHealth / (float)maxHealth;

		EventHandler.CallUpdatePlayerUIEvent(healthAmount);
		if (currentHealth <= 0)
		{
			currentHealth = 0;

			EventHandler.CallPlayerDieEvent();
		}
	}

	public void Die()
	{
		Debug.Log("die");
	}

	public void initStats()
	{
		maxHealth = playerStats.initHealth;
		currentHealth = maxHealth;
		currentExp = 0;
		level = 1;
		strength=playerStats.initStrength;
		vitality = playerStats.initVitality;
		dexterity = playerStats.iniDexterity;

	}

}
