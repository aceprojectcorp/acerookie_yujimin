using UnityEngine;
using System.Collections;

//public enum GameSceneState { title, play, result, loading }

public class GameDataTest: MonoBehaviour
{
//	public int getGoldFromPreStage = 0;
//	public int hitScoreFromPreStage = 0;
//	public int distFromPreStage = 0 ;
//	public float bgSpeed = 10.0f;
	
//	public float screenWidth = 0f;
//	public float screenHeight = 0f;
//	
//	public GameSceneState nowScene = GameSceneState.title;

	public float bgSpeed2 = 10.0f;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
	}

	void Update()
	{
	}
}
