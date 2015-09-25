using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO : 왼쪽에 선수 이름 + 오른쪽에 내용 레이블 한줄에 가지는 애들, 오른쪽 내용 레이블 초기에 위치 재수정 해주는거 넣기 
public enum MissionType
{
	MISSION_TYPE_STRAIGHT_WIN,
	MISSION_TYPE_TOTAL_WIN,
	MISSION_TYPE_TOTAL_PLAY,
}

// 인덱스이름 바꾸기, 변수이름 좀 쉽게 바꾸기 허허..
// << 플레이정보. 승패객체, 팀별 객체 >>
public class GameData : MonoBehaviour 
{
	private static GameData m_instance;
	public static GameData Instance
	{
		get
		{
			return m_instance;
		}
	}

	//---- 내 기준 플레이 정보 ----//
	public int iPlayFightTeamCnt= 0 ;		// 현재 대결 팀과 경기 횟수
	public int iIdxFightTeam 	= 0 ;  		// 현재 대결 팀의 배열 인덱스 번호 

	public int iTotalPlayCnt 	= 0;		// 총 게임 횟수 
	public int iStraightWinCnt	= 0;		// 연승 횟수
	public int iStraightLoseCnt	= 0;		// 연패 횟수 
	//-----------------------// 

	// team 
	public string 		strNameMyTeam = "MyTeam" ;	// 우리 팀 이름 
	public string[] 	arrStrTeamName; 			// 전체 팀 이름
	public InfoOfTeam 	myTeamObj ;					// 내 팀 정보 객체 
	public InfoOfTeam 	fightTeamObj;				// 현재 대결 팀 객체 (계속 바뀜. infoAllTeam안에서) 
	public List <InfoOfTeam> listAllTeam = new List<InfoOfTeam> ();		// 전체 팀 객체

	// 미션 데이터 객체 
	public List <MissionData> listMission = new List<MissionData>();

	public string[] arrStrTextColor = new string[]
	{ "[FF6868FF]"/*red*/,	"[87C8FFFF]"/*blue*/,	"[C1C161FF]"/*bagie*/,	"[FFFFFFFF]"/*white*/ };

	// text color (type BBCode)
	public string strTextColorRed 		= "[FF6868FF] ";
	public string strTextColorBlue 		= "[87C8FFFF] ";
	public string strTextColorYellow 	= "[C1C161FF] ";
	 
	// 선수 기분 배열 
	public string[] arrStrMoodLevel = new string[]
	{ "최상", "좋음", "보통", "나쁨", "최악" };
	 
	// MVP피드 버튼 내용 
	public string[] arrStrMsgBtnMvp = new string[]
	{ 	"다른 선수들이 본받아야 하는 선수라 말한다.",
		"이름값에 걸맞는 활약을 보이려면 아직 부족하다고 말한다."
	};

	// MVP피드 버튼 결과 메세지 
	public string[] arrStrMsgResultMvp = new string[]
	{	"감독의 발언에 선수의 기분이 매우 좋아졌습니다.(기분상승)",
		"감독의 발언에 선수가 겸연쩍어 합니다."	
	};

	// 인터뷰 피드 버튼 내용 (좌-승 / 우-패)
	public string[,] arrStrMsgBtnInterview = new string[,]
	{	{ "야수진, 투수진 모드 좋은 경기력을 보여줬다",	"너무 아쉬운 승부였다. 감독의 잘못이다."			},
		{ "오늘은 운이 상당이 많이 따라준 경기였다.",		"집중력이 승패를 갈랐다. 우리가 질 경기를 했다." 	},
		{ "이제야 팀이 조금은 정상궤도에 올라선 느낌이다.",	"할 말이 없다. 다음 경기 준비 잘하겠다."			}
	}; 

	// 인터뷰 피드 버튼 결과 메세지 
	public string[] arrStrMsgResultInterview = new string[]
	{	"감독의 인터뷰에 몇몇 선수의 기분이 좋아졌습니다.",
		"감독의 인터뷰 내용은 크게 신경쓰지 않는 눈치입니다."	
	};

	// 연패 피드 버튼 내용 
	public string[] arrStrMsgBtnStraightLose = new string[]
	{	"선수들을 격려한다.",	"호통을 친다.",	"할말이 없다고 한다."	};

	// 연패 피드 결과 메세지 
	public string[] arrStrMsgResultStraightLose = new string[]
	{	"선수들이 힘을 내보고자 으쌰으쌰 한다.",
		"선수들의 기분이 나빠졌습니다."	
	};

	// UILabel에 글씨색 추가 
	public string AddColorText( string inputStr, string colorName )
	{
		if ( colorName == "red" ) 
			inputStr = arrStrTextColor[0] + inputStr + "[-]";

		else if ( colorName == "blue" ) 
			inputStr = arrStrTextColor[1] + inputStr + "[-]";

		else if ( colorName == "yellow" ) 
			inputStr = arrStrTextColor[2] + inputStr + "[-]";

		return inputStr;
	}

	void Awake()
	{		
		if( m_instance == null )
			m_instance = this;

		DontDestroyOnLoad (gameObject);

		// 전체 팀 이름 초기화  
		arrStrTeamName = new string[]
		{ "AAAA", 	"BBB", 	"CC", 	"DDDDDDD", 	"EEEEEEEEE", 	"FFF", 	"GG", 	"H", 	"IIIII", strNameMyTeam };

		// 전체 팀 객체 생성
		for(int i=0 ; i < arrStrTeamName.Length ; i++ ) 
		{
			listAllTeam.Add( new InfoOfTeam( arrStrTeamName[i], arrStrTeamName ) );	 
			if( arrStrTeamName[i] == strNameMyTeam )
				myTeamObj = listAllTeam[i];	// 내팀 객체 연결 
		}

		if ( listAllTeam [iIdxFightTeam].strMyTeamName != strNameMyTeam )
			iIdxFightTeam = 0;
		else
			iIdxFightTeam++;

		fightTeamObj = listAllTeam[iIdxFightTeam];
		fightTeamObj.FindIdxOfNextFightTeam ();
		myTeamObj.FindIdxOfNextFightTeam (fightTeamObj.strMyTeamName);

		listMission.Add ( new MissionData ( "3 연승을 하세요.", 		1,	MissionType.MISSION_TYPE_STRAIGHT_WIN  	));
		listMission.Add ( new MissionData ( "10 승을 하세요.", 		5, 	MissionType.MISSION_TYPE_TOTAL_WIN 		));
		listMission.Add ( new MissionData ( "20 경기를 진행하세요 ", 	5,	MissionType.MISSION_TYPE_TOTAL_PLAY 	)); 
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

}

