using UnityEngine;

public class Projectiles : MonoBehaviour
{
	public int damage;
	public float lifeTime=5f;
	public float speed;
	private float timer;
	private void Awake()
	{
		
	}
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= lifeTime) Destroy(this.gameObject);
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
