using System;
using UnityEngine;

public static class EventHandler
{
	public static event Action DieEvent;
	public static void CallDieEvent()
	{ 
		DieEvent?.Invoke();
	}

	public static event Action UpdatePlayerUIEvent;
	public static void CallUpdatePlayerUIEvent()
	{
		UpdatePlayerUIEvent?.Invoke();
	}
}
