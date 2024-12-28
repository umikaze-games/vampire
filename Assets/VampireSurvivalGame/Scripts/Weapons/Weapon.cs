using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public WeaponData weapondata;

	public LevelupUI levelupUI;

	public int weaponLevel=1;

	public GameObject weaponLevelupPanel;
	public void WeaponLevelup()
	{
		weaponLevel++;
		//levelupUI.UpgradeWeaponUI(this,levelupType.assignedWeapon,);
		Time.timeScale = 1.0f;
	}
	
}
