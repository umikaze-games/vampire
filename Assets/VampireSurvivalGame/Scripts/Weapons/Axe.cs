using UnityEngine;

public class Axe : MonoBehaviour
{
	public int damage;
	private float lifeTime = 3f;
	private float timer;
	private Vector3 targetScale;
	private float transformSpeed = 5;
	private float rotateSpeed=100;
	private float targetAngle;

	private void Awake()
	{
		Destroy(this.gameObject, lifeTime);
		targetScale = transform.localScale;
		transform.localScale = Vector3.zero;
	}
	private void Update()
	{
		timer += Time.deltaTime;
		RotateAxe();
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

	private void RotateAxe()
	{
		float currentAngle = transform.eulerAngles.z;
		if (transform.position.x > Player.Instance.transform.position.x)
		{
			targetAngle = -135;

		}
		else targetAngle = 135; 
		float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotateSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Euler(0, 0, newAngle);
		//transform.Rotate(0, 0, rotateSpeed * rotateDir* Time.deltaTime);
	}
}
