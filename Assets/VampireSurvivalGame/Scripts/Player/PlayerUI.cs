using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	public static PlayerUI Instance;
    public Image hpBar;
	private void Awake()
	{
		if (Instance == null)
		{ 
			Instance = this;
		}

		else Destroy(this.gameObject);
	}
	private void Start()
	{
		hpBar.fillAmount = 1;
	}
	private void OnEnable()
	{
		EventHandler.UpdatePlayerUIEvent += OnUpdatePlayerUIEvent;
	}

	private void OnDisable()
	{
		EventHandler.UpdatePlayerUIEvent -= OnUpdatePlayerUIEvent;
	}

	private void OnUpdatePlayerUIEvent()
	{
		float hpBarAmout=(float)PlayerStats.Instance.currentHP / (float)PlayerStats.Instance.maxHP; 
		hpBar.fillAmount = hpBarAmout;
	}
}