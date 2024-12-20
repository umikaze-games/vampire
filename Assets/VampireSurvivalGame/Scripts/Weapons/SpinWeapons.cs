using Unity.Cinemachine;
using UnityEngine;

public class SpinWeapons : Weapon
{
    private float rotateSpeed;
	public Damager damager;
	private float timeBetweenSpawn;

	private float damageMultiplier = 0.5f;
	private float rangeMultiplier = 0.2f;
	private float timeBetweenAttackMultiplier = 0.2f;
	private float durationMultiplier = 0.2f;
	private float speedMultiplier = 0.2f;
	private void Start()
	{
		UpdateWeaponStats();
	}
	private void Update()
	{
		RotateFireball();
		UpdateWeaponStats();
	}

	private void RotateFireball()
	{
		//float time = 0;
		//time += Time.deltaTime;
		transform.Rotate(0, 0, Time.deltaTime*rotateSpeed);
	}
	public void UpdateWeaponStats()
	{
		damager.damage =(int) (weapondata.damage*(1+weaponLevel*damageMultiplier));
		transform.localScale = Vector3.one * weapondata.range*(1+ weaponLevel*rangeMultiplier);
		timeBetweenSpawn = weapondata.timeBetweenAttack * (1 + weaponLevel * timeBetweenAttackMultiplier);
		damager.lifeTime = weapondata.duration * (1 + weaponLevel * durationMultiplier);
		rotateSpeed = weapondata.speed*(1+ weaponLevel * speedMultiplier);
	}
}
