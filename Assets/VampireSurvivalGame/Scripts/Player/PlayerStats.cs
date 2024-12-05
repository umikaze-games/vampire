using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance;
    public int maxHP=100;
    public int currentHP;
	public int currentExp =0;
	public int level =1;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else Destroy(Instance.gameObject);
		currentHP = maxHP;
	}
}
