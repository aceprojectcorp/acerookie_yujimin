using UnityEngine;
using System.Collections;

// << 시뮬레이션 팝업 내용 출력 및 결과 데이터 저장(활성화 되는 순간 저장) >>  
public class HandlingSimulPlayAndResult : MonoBehaviour   
{
	public GameObject Camera03 ; 

	private GameObject GoOkBtn ; 
	private UIProgressBar pgSimulPlayTime ; 	
	private UILabel lbTitle ;
	private UILabel lbContent ;
	
	public float pgFullTime	 = 1.0f;	// 프로그래스바 변경을 보여줄 시간(초)	// 테스트 끝나면 public 없얘기 
	float pgAccTime 		 = 0 ; 		// 프로그래스바 현재 누적된 시간 
	bool isEndMessageTime 	 = false;	// 팝업에서 결과 화면 띄울 타이밍으로 전환하는 스위치 
	string strMsgContent 	 = null;
	string strMsgTitleInit 	 = "시뮬레이션 실행중" ;	
	string strMsgTitleResult = " << 게 임 결 과 >>" ;	

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch (child.name)
			{
			case "Ok_Btn":
				GoOkBtn = child.gameObject;
				break;
				
			case "Progress Bar" :
				pgSimulPlayTime = child.GetComponent<UIProgressBar>();
				break;
				
			case "Title_Label" :
				lbTitle = child.GetComponent<UILabel>();
				break;
				
			case "Content_Label" :
				lbContent = child.GetComponent<UILabel>();
				break;				
			}
		}
	}
	void Awake()
	{
		OnGetChildObject ();
	}


	// 활성화 될때마다 값 초기화. 
	void OnEnable() 
	{ 
//		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " Win : " + GameData.Instance.infoOfNowFightTeam.listTeamRecord[GameData.Instance.infoOfNowFightTeam.iIdxFightTeam].iMyWinCnt );
//		Debug.Log ( GameData.Instance.nameOfNextFightTeam+ " lose : " + GameData.Instance.infoOfNowFightTeam.listTeamRecord[GameData.Instance.infoOfNowFightTeam.iIdxFightTeam].iMyLoseCnt );

		// 시간, 스위치 초기화 및 총 플레이 게임수 증가. 
		pgAccTime 		= 0 ; 			
		isEndMessageTime 	= false;

		// 버튼 비/활성화 
		GoOkBtn.SetActive (false); 
		pgSimulPlayTime.gameObject.SetActive (true); 		
		
		// 연결객체 값 초기화 
		pgSimulPlayTime.value = 0; 
		lbTitle.text = strMsgTitleInit;
		lbContent.text 
			= GameData.Instance.strNameMyTeam + " vs " + GameData.Instance.fightTeamObj.strMyTeamName;
		
		// 승패 랜덤 결과에 따른 데이터 값 변경  
		int rand1to100 = Random.Range (1, 101);
		// HeadlingWin/Lose()에서 승패에 따른 결과 메세지 변경, 
		// 각팀 객체에 승패결과 알려줌(객체내에서 상대팀과의 전적 저장), 연승연패 계산 및 누적   
		if ( rand1to100 >= 1 ) 
			HeadlingWin();
		else
			HeadlingLose();	

		// 다음팀으로 넘어가야 하는지 확인. 모든 팀 리스트내에서 플레이어의 팀이 아닌 다른 팀으로 변경됨 
		CheckFightTime ();

		GameData.Instance.iTotalPlayCnt++;
	}


	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
		// 프로그래스바 누적 시간이 3초(pgFullTime)미만이면 프로그래스바 값을 줄여주고,
		// 3초 이상이면 프로그래스바 비활성화하며 ok버튼 활성화시킴 
		if ( isEndMessageTime == false ) 
		{
			pgAccTime += Time.deltaTime; 
			pgSimulPlayTime.value = pgAccTime / pgFullTime  ; 
			if( pgAccTime >= pgFullTime )
			{
				pgAccTime = 0 ; 
				isEndMessageTime = true ;
				pgSimulPlayTime.gameObject.SetActive( false ) ; 
				GoOkBtn.SetActive ( true );

				lbTitle.text = strMsgTitleResult;
				lbContent.text = strMsgContent;
			}
		} 
	} 

	public void OnClickOkBtn() 
	{
		GameData.Instance.CheckMissionProgress ();
		Camera03.SetActive ( false ); 
	}

	void HeadlingWin()
	{
		strMsgContent = "[ff0000]승   리[-]";
		GameData.Instance.fightTeamObj.PlayResult(false);
		GameData.Instance.myTeamObj.PlayResult(true);
		
		GameData.Instance.iStraightLoseCnt = 0;
		GameData.Instance.iStraightWinCnt++;
	}
	
	void HeadlingLose()
	{
		strMsgContent = "[0000ff]패   배[-]";
		GameData.Instance.fightTeamObj.PlayResult(true);
		GameData.Instance.myTeamObj.PlayResult(false);
		
		GameData.Instance.iStraightWinCnt = 0;
		GameData.Instance.iStraightLoseCnt++;
	}
	
	void CheckFightTime()
	{
		GameData.Instance.iPlayFightTeamCnt++;
		if ( GameData.Instance.iPlayFightTeamCnt % 3 == 0 )
			ChangeFightTeam ();
	}
	
	void ChangeFightTeam()
	{
		//		Debug.Log ( GameData.Instance.nameOfNowFightTeam+ " Win : " + GameData.Instance.fightTeamObj.listTeamRecord[GameData.Instance.fightTeamObj.iIdxFightTeam].iMyWinCnt );
		//		Debug.Log ( GameData.Instance.nameOfNowFightTeam+ " lose : " + GameData.Instance.fightTeamObj.listTeamRecord[GameData.Instance.fightTeamObj.iIdxFightTeam].iMyLoseCnt );
		
		GameData.Instance.iPlayFightTeamCnt = 0; 
		
		GameData.Instance.iIdxFightTeam++; 
		if( GameData.Instance.listAllTeam[ GameData.Instance.iIdxFightTeam ].strMyTeamName == GameData.Instance.strNameMyTeam )
			GameData.Instance.iIdxFightTeam++; 
		
		if ( GameData.Instance.iIdxFightTeam >= GameData.Instance.arrStrTeamName.Length )
			GameData.Instance.iIdxFightTeam = 0;
		
		GameData.Instance.myTeamObj.FindIdxOfNextFightTeam ( GameData.Instance.fightTeamObj.strMyTeamName );
		GameData.Instance.fightTeamObj = GameData.Instance.listAllTeam[ GameData.Instance.iIdxFightTeam ]; 
		GameData.Instance.fightTeamObj.FindIdxOfNextFightTeam ( );
		GameData.Instance.myTeamObj.FindIdxOfNextFightTeam ( GameData.Instance.fightTeamObj.strMyTeamName );
	}

}
