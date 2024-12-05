using System;
using UnityEngine;

public class Player : MonoBehaviour,ICharacter
{
	[SerializeField]
	private float moveSpeed;
	private float moveX;
	private float moveY;
	private Animator playerAnimator;

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
		
	}

	private void OnDisable()
	{
	
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
		PlayerStats.Instance.currentHP-=damage;

		EventHandler.CallUpdatePlayerUIEvent();
		if (PlayerStats.Instance.currentHP <= 0)
		{
			PlayerStats.Instance.currentHP = 0;

			EventHandler.CallDieEvent();
		}
	}

	public void Die()
	{
		Debug.Log("die");
	}
}
