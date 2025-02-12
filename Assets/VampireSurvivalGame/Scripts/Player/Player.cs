using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,ICharacter
{
	public static Player Instance;

	[SerializeField]
	private Weapon activeWeapon;

	private float moveSpeed=3;
	private float moveX;
	private float moveY;
	private Animator playerAnimator;
	public CharacterStats playerStats;
	public GameObject deadParticle;

	public int coin = 0;
	public int maxHealth;
	public int currentExp;
	public int currentHealth;
	public int level=1;
	public int strength;
	public int vitality;
	public int dexterity;
	public int nextExp;

	private bool isGameOver;
	public bool IsGameOver { get=>isGameOver; }
	public List<Weapon> unassignedWeapons, assignedWeapons;
	
	private void Awake()
	{
		if (Instance==null)
		{
			Instance = this;
		}
		else Destroy(this.gameObject);

		initStats();

	}
	private void Start()
	{
		playerAnimator = GetComponent<Animator>();
		AddWeapon(0);
	}
	private void Update()
	{
		PlayerMove();
	}
	private void OnEnable()
	{
		EventHandler.GameOverEvent += OnGameOver;
		EventHandler.PlayerDieEvent += Die;
		EventHandler.LevelupEvent += OnlevelupEvent;
	}

	private void OnDisable()
	{
		EventHandler.GameOverEvent -= OnGameOver;
		EventHandler.PlayerDieEvent -= Die;
		EventHandler.LevelupEvent -= OnlevelupEvent;
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
		EventHandler.CallGameOverEvent();
		EventHandler.CallPlaySFXEvent(SFXName.PlayerDeath);
	}

	public void initStats()
	{
	    isGameOver=false;
		maxHealth = playerStats.initHealth;
		currentHealth = maxHealth;
		currentExp = 0;
		level = 1;
		strength=playerStats.initStrength;
		vitality = playerStats.initVitality;
		dexterity = playerStats.iniDexterity;
		nextExp=playerStats.intiLevelupExp;
	}

	private void OnlevelupEvent()
	{
		level++;
		currentExp -= nextExp;
		nextExp = (int)(nextExp * playerStats.levelUpExpMultiplier);
		float amount= (float)currentExp / (float)nextExp;
		UIManager.Instance.UpdateExpUI(amount, level);
		if (currentExp>=nextExp)
		{
			OnlevelupEvent();
		}
	}

	public void AddWeapon(int weaponIndex)
	{
		if (weaponIndex < unassignedWeapons.Count)
		{
			assignedWeapons.Add(unassignedWeapons[weaponIndex]);
			unassignedWeapons[weaponIndex].gameObject.SetActive(true);
			unassignedWeapons.RemoveAt(weaponIndex);
		}
	}

	private void OnGameOver()
	{
		Instantiate(deadParticle, transform.position, Quaternion.Euler(-90, 0, 0));
		this.gameObject.SetActive(false);
		isGameOver = true;
	}

}
