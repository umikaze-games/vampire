using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	public Slider expSlider;
	public TextMeshProUGUI levelText;
	public GameObject weaponLevelupPanel;

	private int levelupBtnIndex;
	public List<LevelupUI> levelupBtns;
	private void Awake()
	{
		if (Instance == null)Instance = this;
		else Destroy(this.gameObject);
		expSlider.value = 0;

	}

	private void Start()
	{
		UpdateWeapons();
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
		UpdateWeapons();
		weaponLevelupPanel.SetActive(true);
		Time.timeScale= 0;
	}

	public void UpdateExpUI(float amount,int level)
	{ 
		expSlider.value = amount;
		levelText.text = $"level {level}";

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

}
