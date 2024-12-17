using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	public Slider expSlider;
	public TextMeshProUGUI levelText;
	private void Awake()
	{
		if (Instance == null)Instance = this;
		else Destroy(this.gameObject);
		expSlider.value = 0;
	}
	public void UpdateExpUI(float amount,int level)
	{ 
		expSlider.value = amount;
		levelText.text = $"level {level}";

	}
}
