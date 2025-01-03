using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string loadScene;

    public Button startBtn;
    public Button exitBtn;
	private void Awake()
	{
        startBtn.onClick.AddListener(StartGame);
		exitBtn.onClick.AddListener(ExitGame);
	}
	public void StartGame()
    {
        SceneManager.LoadScene(loadScene);
    }
    public void ExitGame()
    { 
        Application.Quit();
    }
}
