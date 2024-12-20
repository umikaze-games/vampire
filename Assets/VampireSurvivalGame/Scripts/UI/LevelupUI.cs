using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelupUI : MonoBehaviour
{
	public TextMeshProUGUI weaponName;
	public TextMeshProUGUI weaponDescription;
	public Image weponIcon;

	public void UpgradeWeaponUI(Weapon weapon)
	{
		weaponName.text=$"{weapon.name} Level{weapon.weaponLevel}";
		weaponDescription.text = weapon.weapondata.description;
		weponIcon.sprite = weapon.weapondata.icon;
	}
}
