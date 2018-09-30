using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressExample : MonoBehaviour
{
	[SerializeField]
	Button addGoldButton;
	[SerializeField]
	Button addLevelScoreButton;
	[SerializeField]
	Button saveButton;
	[SerializeField]
	Button loadButton;
	[SerializeField]
	Button clearButton;
	[SerializeField]
	Text rawDataText;
	[SerializeField]
	Text levelScoreText;
	[SerializeField]
	Text goldText;

	void Start ()
	{
		addGoldButton.onClick.AddListener(OnAddGoldClicked);
		addLevelScoreButton.onClick.AddListener(OnAddLevelScoreClicked);

		saveButton.onClick.AddListener(OnSaveClicked);
		loadButton.onClick.AddListener(OnLoadClicked);
		clearButton.onClick.AddListener(OnClearClicked);
	}

	private void OnSaveClicked()
	{
		PlayerProgress.Instance.Save();
	}

	private void OnLoadClicked()
	{
		PlayerProgress.Instance.Load();
	}

	private void OnClearClicked()
	{
		PlayerProgress.Instance.Clear();
	}

	private void OnAddGoldClicked()
	{
		PlayerProgress.Instance.Data.gold += Random.Range(3,10);
	}

	private void OnAddLevelScoreClicked()
	{
		var data = PlayerProgress.Instance.Data;

		data.levelScores.Add(new LevelScore()
		{
			levelId = data.currentLevel,
			score = Random.Range(300, 2000)
		});

		data.currentLevel++;
	}

	private void Update()
	{
		//This should not be called every frame in a real scenario
		var data = PlayerProgress.Instance.Data;

		rawDataText.text = JsonUtility.ToJson(data);

		levelScoreText.text = "";
		foreach (var levelScore in data.levelScores)
		{
			levelScoreText.text += "Level: " + levelScore.levelId + " - " + levelScore.score + "\n";
		}

		goldText.text = "Gold: " + data.gold;
	}
}
