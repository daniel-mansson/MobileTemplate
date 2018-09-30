using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdService : MonoBehaviour
{
	public static AdService Instance { get; private set; }

	[SerializeField]
	bool useDebugFakeAds = false;

	string currentDebugPlacementId = "";
	System.Action<ShowResult> currentDebugOnDoneCallback = null;

	bool isAdRunning = false;

	private void Awake()
	{
		if (Instance == null)
		{
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public bool IsReady(string placementId)
	{
		if (useDebugFakeAds)
		{
			return true;
		}
		else
		{
			return Advertisement.IsReady(placementId);
		}
	}

	public void ShowAd(string placementId, System.Action<ShowResult> onDoneCallback)
	{
		Debug.Log("Trying to show advertisement '" + placementId + "'");

		if (!isAdRunning)
		{
			isAdRunning = true;

			if (useDebugFakeAds)
			{
				currentDebugOnDoneCallback = onDoneCallback;
				currentDebugPlacementId = placementId;
			}
			else
			{
				Advertisement.Show(placementId, new ShowOptions()
				{
					resultCallback = result => HandleShowResult(placementId, result, onDoneCallback)
				});
			}
		}
		else
		{
			Debug.LogError("Trying to start ad while there already is one running.");

			if (onDoneCallback != null)
			{
				onDoneCallback(ShowResult.Failed); 
			}
		}
	}

	void HandleShowResult(string placementId, ShowResult result, System.Action<ShowResult> onDoneCallback)
	{
		Debug.Log("Advertisement '" + placementId + "' returned result: " + result.ToString());
		isAdRunning = false;

		if (onDoneCallback != null)
		{
			onDoneCallback(result);
		}
	}

	public class AdSequenceHandle
	{
		public Coroutine Coroutine;
		public ShowResult Result;
	}

	public AdSequenceHandle ShowAdSequence(string placementId)
	{
		var handle = new AdSequenceHandle()
		{
			Result = ShowResult.Failed
		};

		handle.Coroutine = StartCoroutine(ShowAdSequence(handle, placementId));

		return handle;
	}

	IEnumerator ShowAdSequence(AdSequenceHandle handle, string placementId)
	{
		bool done = false;

		ShowAd(placementId, result => 
		{
			handle.Result = result;
			done = true;
		});

		yield return new WaitUntil(() => done);
	}

	private void OnGUI()
	{
		if (useDebugFakeAds)
		{
			if (currentDebugOnDoneCallback != null)
			{
				GUI.matrix = Matrix4x4.Scale(Vector3.one * (Screen.width / 230f));
				GUI.Box(new Rect(0, 0, 600, 1200), "");

				GUILayout.Label("Showing ad with placement id: " + currentDebugPlacementId);
				GUILayout.Label("");

				foreach (ShowResult result in System.Enum.GetValues(typeof(ShowResult)))
				{
					if (GUILayout.Button("Report " + result.ToString()))
					{
						HandleShowResult(currentDebugPlacementId, result, currentDebugOnDoneCallback);

						currentDebugOnDoneCallback = null;
						currentDebugPlacementId = string.Empty;
					}
				}
			}
		}
	}
}
