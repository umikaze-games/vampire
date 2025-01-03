using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
	public static DamageManager instance;
	public GameObject damagePrefab;
	private void Awake()
	{
		if (instance == null)
		{ 
			instance=this;
		
		}
		else Destroy(this.gameObject);
	}

	private void OnEnable()
	{
		EventHandler.ShowDamageUIEvent += OnShowDamageUIEvent;
	}
	private void OnDisable()
	{
		EventHandler.ShowDamageUIEvent -= OnShowDamageUIEvent;
	}

	private void OnShowDamageUIEvent(Enemy enemy, int damage)
	{
		var damageGameobject=Instantiate(damagePrefab,enemy.transform.position,Quaternion.identity);
		if (damageGameobject!=null)
		{
			damageGameobject.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
		}
	}

}
