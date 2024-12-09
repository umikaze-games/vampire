using UnityEngine;

public class Fireball : MonoBehaviour
{
	private int damage = 10;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}
	}
}