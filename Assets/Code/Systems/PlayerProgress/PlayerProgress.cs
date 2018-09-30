using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerProgress : MonoBehaviour
{
	public static PlayerProgress Instance { get; private set; }

	const string PlayerProgressFilename = "PlayerProgressData.json";

	[SerializeField]
	PlayerProgressData data;
	[Header("Debug options")]
	[SerializeField]
	bool saveInEditor = true;
	[SerializeField]
	bool usePrettyPrintJson = false;

	public PlayerProgressData Data { get { return data; } }

	private void Awake()
	{
		if (Instance == null)
		{
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this;
			Load();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			Save();
		}
	}

	private void OnApplicationQuit()
	{
		Save();
	}

	[ContextMenu("Load")]
	public void Load()
	{
		var path = System.IO.Path.Combine(Application.persistentDataPath, PlayerProgressFilename);

		if (System.IO.File.Exists(path))
		{
			Debug.Log("Loading player progress from: " + path);

			var json = System.IO.File.ReadAllText(path);
			data = JsonUtility.FromJson<PlayerProgressData>(json);
		}
		else
		{
			Debug.Log("Creating new player progress");
			data = new PlayerProgressData();
		}
	}

	[ContextMenu("Save")]
	public void Save()
	{
		if (Application.isEditor && !saveInEditor)
		{
			Debug.Log("Skipping to save player progress in editor");
			return;
		}

		var json = JsonUtility.ToJson(data, usePrettyPrintJson);
		var path = System.IO.Path.Combine(Application.persistentDataPath, PlayerProgressFilename);

		Debug.Log("Saving player progress to: " + path);
		
		System.IO.File.WriteAllText(path, json);
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
		Debug.Log("Clearing player progress");

		var path = System.IO.Path.Combine(Application.persistentDataPath, PlayerProgressFilename);
		if (File.Exists(path))
		{
			File.Delete(path);
		}

		data = new PlayerProgressData();
	}
}
