using UnityEngine;
using System.Collections;

public class DrawScoreInResultScene : MonoBehaviour {

	public UILabel distScore;
	public UILabel hitScore;
	public UILabel goldScore ;
	public UILabel maxTotlaScore;
	public UILabel totalScore;

	void Start () {
	
	}
	
	void Update () 
	{
		distScore.text = GameData.Instance.scDistFromStage.ToString();
		hitScore.text = GameData.Instance.scHitMonFromStage.ToString();	
		goldScore.text = GameData.Instance.scGoldFromStage.ToString();
		maxTotlaScore.text = GameData.Instance.scBestHighTotal.ToString();	
		totalScore.text = (GameData.Instance.scDistFromStage + GameData.Instance.scHitMonFromStage).ToString();
	}
}
