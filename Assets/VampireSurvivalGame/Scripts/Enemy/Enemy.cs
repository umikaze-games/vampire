using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;
	private Rigidbody2D rb;
	public GameObject player;
	private Animator animator;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		player = GameObject.FindWithTag("Player");
	}

	private void Update()
	{
		Chase(player.transform.position);
	}

	private void Chase(Vector3 position)
	{
		if (Vector3.Distance(player.transform.position, transform.position) <= 0.1)
		{
			animator.SetBool("IsMoving", false);
			return;
		} 

		Vector3 dir = new Vector3(position.x-transform.position.x, position.y-transform.position.y,0).normalized;
		transform.position += dir * moveSpeed * Time.deltaTime;
		if (dir!=Vector3.zero)
		{
			animator.SetBool("IsMoving", true);
			if (dir.x>0)
			{
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
			}
			else
			{
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);
			}
		}
	}


}
