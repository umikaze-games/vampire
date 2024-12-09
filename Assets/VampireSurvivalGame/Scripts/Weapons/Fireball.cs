 using UnityEngine;

public class Fireball : MonoBehaviour
{
	private int damage = 10;
	private float knockBackForce = 10;
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
		GameObject player =GameObject.FindGameObjectWithTag("Player");
		float dirX = (enemy.transform.position - player.transform.position).x;
		float dirY = (enemy.transform.position - player.transform.position).y;

		Vector2 dir = new Vector2(dirX, dirY).normalized;
		Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
		enemyRb.AddForce(dir*knockBackForce,ForceMode2D.Impulse);
	}
}