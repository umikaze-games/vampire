using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public WeaponData weapondata;

	public int weaponLevel=1;

	public void WeaponLevelup()
	{
		weaponLevel++;
	}
}
