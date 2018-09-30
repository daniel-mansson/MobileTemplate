using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is a hack to make the Escape key / Android back button go back to the menu
/// regardless of which scene is loaded
/// </summary>
public class ExampleMenuBackBehaviour : MonoBehaviour
{
	static bool isInitialized = false;

	private void Start()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("ExampleMenu");
		}
	}
}
