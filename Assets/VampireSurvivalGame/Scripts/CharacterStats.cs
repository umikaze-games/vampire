using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [Header("Health")]
	public int initHealth=100;
	public int maxMana=100;

	[Header("Level")]
    public int intiLevelupExp=20;
	public float levelUpExpMultiplier = 1.5f;

	[Header("Stats")]
	public int initStrength=10;
    public int iniDexterity = 10;
    public int initVitality=10;

}
