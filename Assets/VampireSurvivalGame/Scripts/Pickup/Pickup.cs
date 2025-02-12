using UnityEngine;

public class Pickup : MonoBehaviour
{
	private Player player;
	public float pickupRange=20;
	private float moveSpeed=5;
	private float pickupInterval = 0.2f;
	private float timer;
	private void Awake()
	{
		player = GetComponent<Player>();

	}
	private void Update()
	{

		PickupItems();

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
					float amount = (float)player.currentExp / (float)player.nextExp;
					UIManager.Instance.UpdateExpUI(amount,player.level);
					if (player.currentExp>=player.nextExp)
					{
						EventHandler.CallLevelupEvent();
					}
					break;
				case ItemType.Coin:
					player.coin += items.amount;
					UIManager.Instance.UpdateCoinsUI(player.coin);
					break;
				case ItemType.Health:
					player.currentHealth += items.amount;
					break;
				default:
					break;
			}
			EventHandler.CallPlaySFXEvent(SFXName.Pickup);
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

