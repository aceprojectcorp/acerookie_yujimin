using UnityEngine;
using System.Collections;

public enum GameSceneState { title, play, result, loading }

public static class GameData {
	public static int getGoldFromPreStage = 0;
	public static int hitScoreFromPreStage = 0;
	public static int distFromPreStage = 0 ;
	public static float bgSpeed = 0.01f;

	public static GameSceneState nowScene = GameSceneState.title;


}
