using UnityEngine;
using System.Collections;

public class DrawScoreInPlayScene : MonoBehaviour {
	
	public UILabel totalScore;
	public UILabel hitScore;
	public UILabel goldScore ;

	void Start () {
		
	}
	
	void Update () 
	{
		totalScore.text = GameData.Instance.scDistFromStage.ToString();
		hitScore.text 	= GameData.Instance.scHitMonFromStage.ToString();
		goldScore.text 	= GameData.Instance.scGoldFromStage.ToString();	
	}
}
