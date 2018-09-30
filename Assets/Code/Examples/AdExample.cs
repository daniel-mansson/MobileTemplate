using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdExample : MonoBehaviour
{
	[SerializeField]
	Button callbackButton;
	[SerializeField]
	Button coroutineButton;
	[SerializeField]
	Text progressText;

	private void Start()
	{
		callbackButton.onClick.AddListener(OnCallbackButtonClicked);
		coroutineButton.onClick.AddListener(OnCoroutineButtonClicked);
	}

	private void OnCallbackButtonClicked()
	{
		progressText.text = "Clicked callback button";

		AdService.Instance.ShowAd("video", HandleShowAdDone);
	}

	private void HandleShowAdDone(ShowResult result)
	{
		progressText.text = "Callback result: " + result.ToString();
	}

	private void OnCoroutineButtonClicked()
	{
		StartCoroutine(ShowAdSequence());
	}

	IEnumerator ShowAdSequence()
	{
		progressText.text = "Clicked coroutine button";

		var handle = AdService.Instance.ShowAdSequence("video");
		yield return handle.Coroutine;

		progressText.text = "Coroutine result: " + handle.Result.ToString();
	}
}
