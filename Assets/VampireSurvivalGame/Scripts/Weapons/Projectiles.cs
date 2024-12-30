using UnityEngine;

public class Projectiles : MonoBehaviour
{
	public int damage;
	private float lifeTime=5f;
	public float movespeed;
	private float timer;
	private void Awake()
	{
		
	}
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= lifeTime)
		{
			EventHandler.CallRemoveProjectileEvent(this);
			Destroy(this.gameObject);
		} 
		transform.Translate(Vector3.up * movespeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
			EventHandler.CallRemoveProjectileEvent(this);
			Destroy(this.gameObject);
		}
	}
}
