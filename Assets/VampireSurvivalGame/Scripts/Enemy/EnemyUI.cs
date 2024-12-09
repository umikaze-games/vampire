using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Image hpBar;
	private void Awake()
	{
	}
	private void Start()
	{
		hpBar.fillAmount = 1;
	}
	private void OnEnable()
	{

	}

	private void OnDisable()
	{

	}

	public void UpdateEnemyHealthUI(float healthAmout)
	{	
		hpBar.fillAmount = healthAmout;
	}
}
