using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticsExample : MonoBehaviour
{
	[SerializeField]
	Button startLevelButton;
	[SerializeField]
	Button completeLevelButton;
	[SerializeField]
	Button failLevelButton;
	[SerializeField]
	InputField levelNameInput;

	void Start ()
	{
		levelNameInput.text = "level1";

		startLevelButton.onClick.AddListener(OnStartLevelClicked);
		completeLevelButton.onClick.AddListener(OnCompleteLevelClicked);
		failLevelButton.onClick.AddListener(OnFailLevelClicked);
	}

	private void OnStartLevelClicked()
	{
		Analytics.LevelStart(levelNameInput.text);
	}

	private void OnCompleteLevelClicked()
	{
		Analytics.LevelComplete(levelNameInput.text);
	}

	private void OnFailLevelClicked()
	{
		Analytics.LevelFail(levelNameInput.text);
	}
}
