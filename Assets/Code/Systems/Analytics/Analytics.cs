using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class Analytics : MonoBehaviour
{
	public static Analytics Instance { get; private set; }

	[SerializeField]
	bool debugLogAnalyticsEvents = false;

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

	void SendCustomEvent(string eventName, Dictionary<string, object> eventData)
	{
		if (debugLogAnalyticsEvents)
		{
			Debug.Log("Sending analytics event '" + eventName + "': " + string.Join(", ", eventData.Select(kvp => kvp.Key + "=" + kvp.Value.ToString()).ToArray()));
		}

		var result = AnalyticsEvent.Custom(eventName, eventData);
		if (result != AnalyticsResult.Ok)
		{
			Debug.LogWarning("Unexpected analytics result for event '" + eventName + "': " + result.ToString());
		}
	}

	void SendStandardEvent(System.Func<AnalyticsResult> action, string eventName = "not set")
	{
		if (debugLogAnalyticsEvents)
		{
			Debug.Log("Sending analytics event '" + eventName + "'");
		}

		var result = action();
		if (result != AnalyticsResult.Ok)
		{
			Debug.LogWarning("Unexpected analytics result for event '" + eventName + "': " + result.ToString());
		}
	}

	public static void LevelStart(string name)
	{
		Instance.SendStandardEvent(() => AnalyticsEvent.LevelStart(name), "LevelStart");
	}

	public static void LevelComplete(string name)
	{
		Instance.SendStandardEvent(() => AnalyticsEvent.LevelComplete(name), "LevelComplete");
	}

	public static void LevelFail(string name)
	{
		Instance.SendStandardEvent(() => AnalyticsEvent.LevelFail(name), "LevelFail");
	}

	public static void LevelSkip(string name)
	{
		Instance.SendStandardEvent(() => AnalyticsEvent.LevelSkip(name), "LevelSkip");
	}

	public static void LevelQuit(string name)
	{
		Instance.SendStandardEvent(() => AnalyticsEvent.LevelQuit(name), "LevelQuit");
	}

	public static void ScreenVisit(string name)
	{
		Instance.SendStandardEvent(() => AnalyticsEvent.ScreenVisit(name), "ScreenVisit");
	}

	//... Expose more events here
}
