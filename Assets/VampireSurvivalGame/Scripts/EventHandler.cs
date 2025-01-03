using System;
using UnityEditor.VersionControl;
using UnityEngine;

public static class EventHandler
{
	public static event Action PlayerDieEvent;
	public static void CallPlayerDieEvent()
	{
		PlayerDieEvent?.Invoke();
	}

	public static event Action<Enemy> EnemyDieEvent;
	public static void CallEnemyDieEvent(Enemy enemy)
	{
		EnemyDieEvent?.Invoke(enemy);
	}

	public static event Action<float> UpdatePlayerUIEvent;
	public static void CallUpdatePlayerUIEvent(float healtAmount)
	{
		UpdatePlayerUIEvent?.Invoke(healtAmount);
	}

	public static event Action<Enemy,float> UpdateEnemyUIEvent;
	public static void CallUpdateEnemyUIEvent(Enemy enemy,float healtAmount)
	{
		UpdateEnemyUIEvent?.Invoke(enemy,healtAmount);
	}

	public static event Action<Enemy, int> ShowDamageUIEvent;
	public static void CallShowDamageUIEvent(Enemy enemy, int damage)
	{ 
		ShowDamageUIEvent?.Invoke(enemy, damage);
	}

	public static event Action LevelupEvent;
	public static void CallLevelupEvent()
	{ 
		LevelupEvent?.Invoke();
	}

	public static event Action LevelupEndEvent;
	public static void CallLevelupEndEvent()
	{
		LevelupEndEvent?.Invoke();
	}

	public static event Action<Projectiles> RemoveProjectileEvent;
	public static void CallRemoveProjectileEvent(Projectiles Projectile)
	{
		RemoveProjectileEvent?.Invoke(Projectile);
	}

	public static event Action<Enemy> RemoveEnemyEvent;
	public static void CallRemoveEnemyEvent(Enemy Enemy)
	{
		RemoveEnemyEvent?.Invoke(Enemy);
	}

	public static event Action GameOverEvent;
	public static void CallGameOverEvent()
	{ 
		GameOverEvent?.Invoke();
	}

	public static event Action<SFXName> PlaySFXEvent;
	public static void CallPlaySFXEvent(SFXName sfxName)
	{
		PlaySFXEvent?.Invoke(sfxName);
	}
}
