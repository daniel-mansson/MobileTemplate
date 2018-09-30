using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerProgressData
{
	public int currentLevel;
	public int gold;
	public List<LevelScore> levelScores = new List<LevelScore>();
	//Add your own player data here
	//Unity's JsonUtility is used for serialization, take a look 
	//at the supported data types at: https://docs.unity3d.com/Manual/script-Serialization.html
	
	//Rule of thumb: If your field shows up in the Unity Editor Inspector, then it works.
}

[System.Serializable]
public class LevelScore
{
	public int levelId;
	public int score;
}