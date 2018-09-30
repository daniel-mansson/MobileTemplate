using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
	[SerializeField]
	List<GameObject> systemPrefabs;

	static bool isInitialized = false;

	void Start ()
	{
		if(!isInitialized)
		{
			isInitialized = true;
			foreach (var prefab in systemPrefabs)
			{
				Instantiate(prefab);
			}
		}

		Destroy(gameObject);
	}
}
