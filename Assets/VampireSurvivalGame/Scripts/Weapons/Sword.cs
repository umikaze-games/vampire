using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{

	public int damage;
	private float lifeTime = 1f;
	private float timer;
	private Vector3 targetScale;
	private float transformSpeed = 5;
	private void Awake()
	{
		Destroy(this.gameObject, lifeTime);
		targetScale = transform.localScale;
		transform.localScale = Vector3.zero;
	}
	private void Update()
	{
		timer += Time.deltaTime;
		transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, transformSpeed * Time.deltaTime);
		if (timer >= lifeTime)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}
	}
}
