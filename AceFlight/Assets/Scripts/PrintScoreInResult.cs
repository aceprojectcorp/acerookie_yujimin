﻿using UnityEngine;
using System.Collections;

// TODO : 스코어 출력 자체 바꿔주기. 씬 확인 후, 텍스트 위치 수정 등등. 두개가 유사하니까. 결과에 종합점수만 추가로 띄워주기 
public class PrintScoreInResult : MonoBehaviour {

	public UILabel totalScore;
	public UILabel hitScore;
	public UILabel goldScore ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		staticGameData.distFromPreStage++;	// printScore_play랑 합치기. 나중에.. 
		totalScore.text = (GameData.Instance.scDistFromStage + GameData.Instance.scHitMonFromStage).ToString();
		hitScore.text = GameData.Instance.scHitMonFromStage.ToString();
		goldScore.text = GameData.Instance.scGoldFromStage.ToString();	

	}
}
