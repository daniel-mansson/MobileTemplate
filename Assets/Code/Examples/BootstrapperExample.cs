using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootstrapperExample : MonoBehaviour
{
	[SerializeField]
	Button sceneChangeButton;
	[SerializeField]
	string nextSceneName = "BootstrapperExample2";

	void Start()
	{
		sceneChangeButton.onClick.AddListener(OnSceneChange);
	}

	private void OnSceneChange()
	{
		SceneManager.LoadScene(nextSceneName);
	}
}
