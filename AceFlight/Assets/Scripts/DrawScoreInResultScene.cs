using UnityEngine;
using System.Collections;

// << 결과씬에서 점수들 그려줌 >>
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
		distScore.text = GameData.Instance.g_scDistFromStage.ToString();
		hitScore.text = GameData.Instance.g_scHitMonFromStage.ToString();	
		goldScore.text = GameData.Instance.g_scGoldFromStage.ToString();
		maxTotlaScore.text = GameData.Instance.g_scBestHighTotal.ToString();	
		totalScore.text = (GameData.Instance.g_scDistFromStage + GameData.Instance.g_scHitMonFromStage).ToString();
	}
}
