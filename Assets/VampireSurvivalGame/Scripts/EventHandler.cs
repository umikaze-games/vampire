using System;
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
}
