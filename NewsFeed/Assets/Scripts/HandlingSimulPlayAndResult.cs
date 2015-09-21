using UnityEngine;
using System.Collections;

// << 시뮬레이션 팝업 내용 출력 및 결과 데이터 저장 >>  
public class HandlingSimulPlayAndResult : MonoBehaviour   
{
	public GameObject spOkBtn ; 
	public GameObject spPGBar ; 
	public GameObject spCamera03 ; 
	public GameObject spTitleLabel ;
	public GameObject spContentLabel ;
	
	public float spPGFullTime 	= 1.0f;	// 프로그래스바 변경을 보여줄 시간(초)
	float spPGAccTime 	= 0 ; 			// 프로그래스바 현재 누적된 시간 
	bool spIsEndMessageTime = false;	// 팝업에서 결과 화면 띄울 타이밍으로 전환하는 스위치 
	string spMsgContent = null;
	string spMsgTitleInit = "시뮬레이션 실행중" ;	
	string spMsgTitleResult = " << 게 임 결 과 >>" ;	

	// 활성화 될때마다 값 초기화. 
	void OnEnable()
	{ 
//		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " Win : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myWinRec );
//		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " lose : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myLoseRec );

		// 시간, 스위치 초기화 및 총 플레이 게임수 증가. 
		spPGAccTime 		= 0 ; 			
		spIsEndMessageTime 	= false;

		// 버튼 비/활성화 
		spOkBtn.SetActive (false); 
		spPGBar.SetActive (true); 		
		
		// 연결객체 값 초기화 
		spPGBar.GetComponent<UIProgressBar> ().value = 0; 
		spTitleLabel.GetComponent<UILabel> ().text 	 = spMsgTitleInit;
		spContentLabel.GetComponent<UILabel> ().text 
			= GameData.Instance.nameOfMyTeam + " vs " + GameData.Instance.nameOfNowFightTeam;
		
		// 승패 랜덤 결과에 따른 데이터 값 변경  
		int rand1to100 = Random.Range (1, 101);
		// HeadlingWin/Lose()에서 승패에 따른 결과 메세지 변경, 
		// 각팀 객체에 승패결과 알려줌(객체내에서 상대팀과의 전적 저장), 연승연패 계산 및 누적   
		if ( rand1to100 >= 1 ) 
			HeadlingWin();
		else
			HeadlingLose();		
	}

	void HeadlingWin()
	{
		spMsgContent = "[ff0000]승   리[-]";
		GameData.Instance.infoOfNowFightTeam.PlayResult(false);
		GameData.Instance.infoOfMyTeam.PlayResult(true);

		GameData.Instance.numOfStraightLoss = 0;
//		GameData.Instance.numOfStraightWin++;
	}

	void HeadlingLose()
	{
		spMsgContent = "[0000ff]패   배[-]";
		GameData.Instance.infoOfNowFightTeam.PlayResult(true);
		GameData.Instance.infoOfMyTeam.PlayResult(false);

		GameData.Instance.numOfStraightWin = 0;
		GameData.Instance.numOfStraightLoss++;
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
			spPGBar.GetComponent<UIProgressBar>().value = spPGAccTime / spPGFullTime  ; 
			if( spPGAccTime >= spPGFullTime )
			{
				spPGAccTime = 0 ; 
				spIsEndMessageTime = true ;
				spPGBar.SetActive( false ) ; 
				spOkBtn.SetActive ( true );

				spTitleLabel.GetComponent<UILabel> ().text = spMsgTitleResult;
				spContentLabel.GetComponent<UILabel> ().text = spMsgContent;
			}
		} 
	} 

	public void OnClickOkBtnOfSmPu() 
	{
		// 다음팀으로 넘어가야 하는지 확인. 모든 팀 리스트내에서 플레이어의 팀이 아닌 다른 팀으로 변경됨 
		CheckFightTime ();

		GameData.Instance.numOfTotalPlay++;

		if ( GameData.Instance.numOfStraightLoss == 0 ) 
		{
			GameData.Instance.numOfTotalWin++;
			GameData.Instance.numOfStraightWin++;
		}

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
		Debug.Log ( GameData.Instance.nameOfNowFightTeam+ " Win : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myWinRec );
		Debug.Log ( GameData.Instance.nameOfNowFightTeam+ " lose : " + GameData.Instance.infoOfNowFightTeam.teamRecord[GameData.Instance.infoOfNowFightTeam.idxOfNowTeamListNum].myLoseRec );

		GameData.Instance.numOfNowFightTeam = 0; 

		GameData.Instance.idxFightTeam++; 
		if( GameData.Instance.infoAllTeam[ GameData.Instance.idxFightTeam ].myTeamName == GameData.Instance.nameOfMyTeam )
			GameData.Instance.idxFightTeam++; 

		if ( GameData.Instance.idxFightTeam >= GameData.Instance.teamNamesArr.Length )
			GameData.Instance.idxFightTeam = 0;
		
		GameData.Instance.nameOfNowFightTeam = GameData.Instance.teamNamesArr [ GameData.Instance.idxFightTeam ];
		GameData.Instance.infoOfMyTeam.FindIdxOfNextFightTeam ( GameData.Instance.nameOfNowFightTeam );
		GameData.Instance.infoOfNowFightTeam = GameData.Instance.infoAllTeam[ GameData.Instance.idxFightTeam ]; 
		GameData.Instance.infoOfNowFightTeam.FindIdxOfNextFightTeam ( );

	}
}
