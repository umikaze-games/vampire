using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	public TextMeshProUGUI gameTimeText;
	public Slider expSlider;
	public TextMeshProUGUI coinsAmount;
	public TextMeshProUGUI levelText;
	public GameObject weaponLevelupPanel;
	public GameObject gameOverPanel;
	public GameObject pausePanel;

	public Button restartBtn;
	public Button returnMenuBtn;

	private float timer;
	private float gameTime;

	private int levelupBtnIndex;
	public List<LevelupUI> levelupBtns;
	private void Awake()
	{
		if (Instance == null)Instance = this;
		else Destroy(this.gameObject);
		expSlider.value = 0;

		restartBtn.onClick.AddListener(RestartGame);
		returnMenuBtn.onClick.AddListener(ReturnMenu);
	}

	private void Start()
	{
		UpdateWeapons();
		UpdateCoinsUI(Player.Instance.coin);
		UpdateGameTimeUI();
	}

	private void Update()
	{
		if (Player.Instance.IsGameOver == true) return;
		gameTime += Time.deltaTime;
		timer += Time.deltaTime;
		if (timer>=1)
		{
			UpdateGameTimeUI();
			timer= 0;
		}
	}
	private void OnEnable()
	{
		EventHandler.GameOverEvent += OnGameOverEvent;
		EventHandler.LevelupEvent += OnCallLevelupEvent;
		EventHandler.LevelupEndEvent += OnCallLevelupEndEvent;
	}

	private void OnDisable()
	{
		EventHandler.GameOverEvent -= OnGameOverEvent;
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
		UpdateWeapons();
		weaponLevelupPanel.SetActive(true);
		Time.timeScale= 0;
	}

	public void UpdateExpUI(float amount,int level)
	{ 
		expSlider.value = amount;
		levelText.text = $"level {level}";

	}

	public void UpdateCoinsUI(int amount)
	{
		coinsAmount.text =$"{amount}" ;
	}

	public void UpdateWeapons()
	{
		UpdateassignedWeapons();
		UpdatenUnassignedWeapons();
	}
	public void UpdateassignedWeapons()
	{
		levelupBtnIndex = 0;
		if (Player.Instance.assignedWeapons!=null)
		{
			foreach (var weapon in Player.Instance.assignedWeapons)
			{
				levelupBtns[levelupBtnIndex].UpgradeWeaponUI(weapon,levelupType.assignedWeapon,levelupBtnIndex);
				levelupBtnIndex++;
			}

		}
		

	}
	public void UpdatenUnassignedWeapons()
	{
		if (Player.Instance.unassignedWeapons != null)
		{
			foreach (var weapon in Player.Instance.unassignedWeapons)
			{
				levelupBtns[levelupBtnIndex].UpgradeWeaponUI(weapon, levelupType.unassignedWeapon,levelupBtnIndex);
				levelupBtnIndex++;
			}
		}
	
	}

	private void UpdateGameTimeUI()
	{
		float seconds = gameTime%60;
		float mintue = gameTime/60;
		string showedGameTime=$"Time {mintue:00}:{seconds:00}";
		gameTimeText.text= showedGameTime;
	}
	private void OnGameOverEvent()
	{
		StartCoroutine(ShowGameOverPanel());
	}

	private IEnumerator ShowGameOverPanel()
	{
		yield return new WaitForSeconds(1);
		gameOverPanel.SetActive(true);
	}

	public void PauseGame()
	{ 
			
	}

	public void RestartGame()
	{
		SceneManager.LoadScene("MainScene");
	}
	public void ReturnMenu()
	{
		SceneManager.LoadScene("TitleScene");
	}
}
