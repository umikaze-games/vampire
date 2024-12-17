using UnityEngine;

public class Pickup : MonoBehaviour
{
	private Player player;
	public float pickupRange=20;
	private float moveSpeed=5;
	//private float pickupInterval = 0.2f;
	//private float timer;
	private void Awake()
	{
		player = GetComponent<Player>();

	}

	private void Update()
	{
		//timer += Time.deltaTime;
		
			PickupItems();
			//timer = 0;
		
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		Items items=collision.gameObject.GetComponent<Items>();
		if (items != null&&Vector3.Distance(collision.transform.position,player.transform.position)<=0.1f)
		{
			switch (items.itemType)
			{
				case ItemType.Experience:
					player.currentExp += items.amount;
					break;
				case ItemType.Coin:
					player.coin += items.amount;
					break;
				case ItemType.Health:
					player.currentHealth += items.amount;
					break;
				default:
					break;
			}
			Destroy(collision.gameObject);

		}
	}
	public void PickupItems()
	{
		Collider2D[] colliers = Physics2D.OverlapCircleAll(transform.position, pickupRange);
		foreach (Collider2D collider in colliers)
		{
			Items items = collider.gameObject.GetComponent<Items>();
			if (items != null)
			{
				ItemsMoveToPlayer(items);
			}

		}
	}
	private void ItemsMoveToPlayer(Items item)
	{
		item.transform.position = Vector3.MoveTowards(item.transform.position, player.transform.position, moveSpeed*Time.deltaTime);
	}
}

