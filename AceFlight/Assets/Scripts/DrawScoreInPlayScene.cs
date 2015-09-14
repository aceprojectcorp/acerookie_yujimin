using UnityEngine;
using System.Collections;

// << play씬에서 점수들 그려줌 >>
public class DrawScoreInPlayScene : MonoBehaviour {
	
	public UILabel distScore;
	public UILabel hitScore;
	public UILabel goldScore ;

	void Start () {
		
	}
	
	void Update () 
	{
		distScore.text = GameData.Instance.g_scDistFromStage.ToString();
		hitScore.text 	= GameData.Instance.g_scHitMonFromStage.ToString();
		goldScore.text 	= GameData.Instance.g_scGoldFromStage.ToString();	
	}
}
