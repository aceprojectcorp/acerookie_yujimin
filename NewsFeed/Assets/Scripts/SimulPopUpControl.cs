using UnityEngine;
using System.Collections;

public class SimulPopUpControl : MonoBehaviour   
{
	public GameObject spOkBtn ; 
	public GameObject spPGBar ; 
	public GameObject spCamera03 ; 
	public GameObject spMessageLabel ;
	
	float spPGFullTime 	= 1.0f;			// 프로그래스바 줄어들 시간(초)
	float spPGAccTime 	= 0 ; 			// 프로그래스바 현재 누적된 시간 
	bool spIsEndMessageTime = false;	// 팝업에서 결과 화면 띄울 타이밍으로 전환하는 스위치 
	bool spIsDrawTimeOfBtn = false ; 	// 팝업에서 결과 화면 전환시, 버튼 활성화. 딱 한번만 처리하기 위한 스위치. 
	string spMsgResult = null ;	
	string spMsgInit = "시뮬레이션 실행중" ;	

	void OnEnable()
	{ 
//		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " Win : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myWinRec );
//		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " lose : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myLoseRec );

		spPGAccTime 	= 0 ; 			
		spIsEndMessageTime = false;
		spIsDrawTimeOfBtn = false ;  
		spOkBtn.SetActive (false); 
		spPGBar.SetActive (true); 
		spPGBar.GetComponent<UIProgressBar> ().value = 1; 
		spMessageLabel.GetComponent<UILabel> ().text 
			= GameData.Instance.nameOfMyTeam + " vs " + GameData.Instance.nameOfNextFightTeam;

		//--
		int rand1to2 = Random.Range (1, 3);
		if (rand1to2 == 1) 
		{
			GameData.Instance.infoOfNowFightTeam.PlayResult(false);
			GameData.Instance.infoOfMyTeam.PlayResult(true);
			spMsgResult = "[ff0000]승   리[-]";
		}
		else
		{
			GameData.Instance.infoOfNowFightTeam.PlayResult(true);
			GameData.Instance.infoOfMyTeam.PlayResult(false);
			spMsgResult = "[0000ff]패   배[-]";
		}
		//--
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// 프로그래스바 누적 시간이 3초(spPGFullTime)미만이면 프로그래스바 값을 줄여주고,
		// 3초 이상이면 프로그래스바 비활성화하며 ok버튼 활성화시킴 
		if ( spIsEndMessageTime == false ) 
		{
			spPGAccTime += Time.deltaTime; 
			spPGBar.GetComponent<UIProgressBar>().value = 1.0f - spPGAccTime / spPGFullTime  ;  //reverse!!!!
			if( spPGAccTime >= spPGFullTime )
			{
				spPGAccTime = 0 ; 
				spIsEndMessageTime = true ;
				spPGBar.SetActive( false ) ; 
			}
		} 
		else 
		{
			if( spIsDrawTimeOfBtn == false )
			{
				spIsDrawTimeOfBtn = true ; 
				spOkBtn.SetActive ( true );
			}
			spMessageLabel.GetComponent<UILabel> ().text = spMsgResult;

		}
	} 

	public void OnClickOkBtnOfSmPu() 
	{
		// nextTeam Setup!
		CheckFightTime ();
		spCamera03.SetActive ( false );
	}

	void CheckFightTime()
	{
		GameData.Instance.numOfNowFightTeam++;
		if ( GameData.Instance.numOfNowFightTeam % 3 == 0 )
			ChangeFightTeam ();
	}

	void ChangeFightTeam()
	{
		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " Win : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myWinRec );
		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " lose : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myLoseRec );

		GameData.Instance.idxFightTeam++; 
		if ( GameData.Instance.idxFightTeam == GameData.Instance.teamNamesArr.Length-1 )
			GameData.Instance.idxFightTeam = 0;
		
		GameData.Instance.nameOfNextFightTeam = GameData.Instance.teamNamesArr [ GameData.Instance.idxFightTeam ];
		GameData.Instance.infoOfMyTeam.FindIdxOfNextFightTeam ( GameData.Instance.nameOfNextFightTeam );
		GameData.Instance.infoOfNowFightTeam = GameData.Instance.infoOtherTeam[ GameData.Instance.idxFightTeam ]; 
	}
}
