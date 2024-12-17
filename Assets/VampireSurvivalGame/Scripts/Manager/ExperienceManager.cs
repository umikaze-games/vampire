using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
	public CharacterStats characterStats;
	public static ExperienceManager instance;
	private void Awake()
	{
		if (instance == null)
		{ instance = this; 
		}
		else Destroy(this.gameObject);
	}

}
