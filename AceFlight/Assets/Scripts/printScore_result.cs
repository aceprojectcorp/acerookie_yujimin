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
		totalScore.text = (staticGameData.distFromPreStage + staticGameData.hitScoreFromPreStage).ToString();
		hitScore.text = staticGameData.hitScoreFromPreStage.ToString();
		goldScore.text = staticGameData.getGoldFromPreStage.ToString();	
	}
}
