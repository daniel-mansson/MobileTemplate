using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExampleMenu : MonoBehaviour
{
	[SerializeField]
	List<Button> buttons;

	void Start ()
	{
		foreach (var button in buttons)
		{
			var sceneName = button.GetComponentInChildren<Text>().text;
			button.onClick.AddListener(() => OnButtonClicked(sceneName));
		}
	}

	private void OnButtonClicked(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
