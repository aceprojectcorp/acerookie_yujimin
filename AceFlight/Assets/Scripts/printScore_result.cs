using UnityEngine;
using System.Collections;

public class printScore_result : MonoBehaviour {

	public UILabel totalScore;
	public UILabel hitScore;
	public UILabel goldScore ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		staticGameData.distFromPreStage++;
		totalScore.text = (GameData.distFromPreStage + GameData.hitScoreFromPreStage).ToString();
		hitScore.text = GameData.hitScoreFromPreStage.ToString();
		goldScore.text = GameData.getGoldFromPreStage.ToString();	
	}
}
