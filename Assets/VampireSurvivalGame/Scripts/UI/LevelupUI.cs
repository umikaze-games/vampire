using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelupUI : MonoBehaviour
{
	public TextMeshProUGUI weaponName;
	public TextMeshProUGUI weaponDescription;
	public Image weponIcon;
	public int index;
	public levelupType levelupType;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(OnClickEvent);
	}
	public void UpgradeWeaponUI(Weapon weapon,levelupType levelupType,int index)
	{
		if (levelupType == levelupType.assignedWeapon)
		{
			weaponName.text = $"{weapon.name} Level{weapon.weaponLevel}";
			weaponDescription.text = weapon.weapondata.description;
			weponIcon.sprite = weapon.weapondata.icon;
		}

		else if (levelupType == levelupType.unassignedWeapon)
		{
			weaponName.text = $"{weapon.name} Unlock";
			weaponDescription.text = weapon.weapondata.description;
			weponIcon.sprite = weapon.weapondata.icon;
		}
		this.levelupType = levelupType;
		this.index = index;
	}

	public void OnClickEvent()
	{
		if (levelupType==levelupType.assignedWeapon)
		{
			Player.Instance.assignedWeapons[index].GetComponent<Weapon>().WeaponLevelup();
			EventHandler.CallLevelupEndEvent();
		}

		else if (levelupType == levelupType.unassignedWeapon)
		{
			int dex = index - Player.Instance.assignedWeapons.Count;
			Player.Instance.AddWeapon(dex);
			EventHandler.CallLevelupEndEvent();
		}
	}
}

public enum levelupType
{
	unassignedWeapon,
	assignedWeapon
}