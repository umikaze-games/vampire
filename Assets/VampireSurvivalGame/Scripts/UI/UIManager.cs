using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	public Slider expSlider;
	public TextMeshProUGUI levelText;
	public GameObject weaponLevelupPanel;
	private void Awake()
	{
		if (Instance == null)Instance = this;
		else Destroy(this.gameObject);
		expSlider.value = 0;
	}

	private void OnEnable()
	{
		EventHandler.LevelupEvent += OnCallLevelupEvent;
		EventHandler.LevelupEndEvent += OnCallLevelupEndEvent;
	}

	private void OnDisable()
	{
		EventHandler.LevelupEvent -= OnCallLevelupEvent;
		EventHandler.LevelupEndEvent -= OnCallLevelupEndEvent;
	}

	private void OnCallLevelupEndEvent()
	{
		weaponLevelupPanel.SetActive(false);
		Time.timeScale = 1.0f;
	}

	private void OnCallLevelupEvent()
	{
		weaponLevelupPanel.SetActive(true);
		Time.timeScale= 0;
	}

	public void UpdateExpUI(float amount,int level)
	{ 
		expSlider.value = amount;
		levelText.text = $"level {level}";

	}


}
