using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO : 왼쪽에 선수 이름 + 오른쪽에 내용 레이블 한줄에 가지는 애들, 오른쪽 내용 레이블 초기에 위치 재수정 해주는거 넣기 
// TODO : 버튼 내용 배열들 이름들 MSG, CONTENT 섞여있음.. 정리해주기 
// TODO : 아래 클래스들... 따로 빼서 다른 파일로 만들어주기 여기에 너무 많음
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
	public int iPlayFightTeamCnt	= 0 ;	// 현재 대결 팀과 경기 횟수
	public int iIdxFightTeam 	= 0 ;  		// 현재 대결 팀의 배열 인덱스 번호 

	public int iTotalPlayCnt 	= 0;		// 총 게임 횟수 
	public int iTotalWinCnt	 	= 0;		// x
	public int iStraightWinCnt	= 0;		// 연승 횟수
	public int iStraightLoseCnt	= 0;		// 연패 횟수 
	//-----------------------// 

	// 오늘의 미션 피드 관련 변수 
	public bool isOffTodayMissionFeed 		= false;	// TodayMission feed setActive on/off 
	public bool isSuccessAllTodayMission	= false;	// Success all todaymissions ?

	// team 
	public string strNameMyTeam = "MyTeam" ;	// 우리 팀 이름 
	public string[] arrStrTeamName; 		// 전체 팀 이름
	//		= new string[]{ "A", 	"B", 	"C", 	"D", 	"E", 	"F", 	"G", 	"H", 	"I", 	"J", nameOfMyTeam };
	 
	public InfoOfTeam myTeamObj ;			// 내 팀 정보 객체 
	public InfoOfTeam fightTeamObj;			// 현재 대결 팀 객체 (계속 바뀜. infoAllTeam안에서) 
	public List <InfoOfTeam> listAllTeam = new List<InfoOfTeam> ();		// 전체 팀 객체

	// 미션 데이터 객체 
	public List <MissionData> listMission = new List<MissionData>();

	// text color
	public string[] arrStrTextColor = new string[]
	{ "[FF6868FF]"/*red*/,	"[87C8FFFF]"/*blue*/,	"[FFFFFFFF]"/*white*/, "[C1C161FF]"/*bagie*/ };
	public string strTextColorRed 		= "FF6868FF";
	public string strTextColorBlue 		= "87C8FFFF";
	public string strTextColorYellow 	= "C1C161FF";

	// 선수 기분 배열 
	public string[] arrStrMoodLevel = new string[]
	{ "최상", "좋음", "보통", "나쁨", "최악" };
	 
	// MVP피드 버튼 내용 
	public string[] arrStrMsgBtnMvp = new string[]
	{ 	"다른 선수들이 본받아야 하는 선수라 말한다.",
		"이름값에 걸맞는 활약을 보이려면 아직 부족하다고 말한다."
	};

	// MVP피드 버튼 결과 메세지 
	public string[] arrStrMsgResultBtnMvp = new string[]
	{	"감독의 발언에 선수의 기분이 매우 좋아졌습니다.(기분상승)",
		"감독의 발언에 선수가 겸연쩍어 합니다."	
	};

	// 인터뷰 피드 버튼 내용 (좌-승 / 우-패)
	public string[,] arrStrContentInterview = new string[,]
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
	public string[] arrStrContentStraightLose = new string[]
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
			inputStr = arrStrTextColor [0] + inputStr + "[-]";

		else if ( colorName == "blue" ) 
			inputStr = arrStrTextColor [1] + inputStr + "[-]";

		else if ( colorName == "white" ) 
			inputStr = arrStrTextColor [2] + inputStr + "[-]";

		return inputStr;
	}

	//개별 미션 관련 변수 카운트 및 모든 미션 성공여부 확인
	public void CheckMissionProgress()
	{
		// success all todaymission cnt! 
		int successMissionCnt = 0; 

		foreach( MissionData data in listMission ) // ( int i = 0 ; i < listMission.Count ; i++ )
		{
			switch( data.Type )
			{
			case MissionType.MISSION_TYPE_STRAIGHT_WIN : 
				if( iStraightWinCnt >= 3 )
					data.nowSuccVal++;
				break;			
			case MissionType.MISSION_TYPE_TOTAL_WIN : 
				if( iStraightWinCnt > 0 )
					data.nowSuccVal++;
				break;
			case MissionType.MISSION_TYPE_TOTAL_PLAY : 
				if( iTotalPlayCnt > 0 )
					data.nowSuccVal++;
				break;
			}
			if( data.nowSuccVal >= data.fullSuccVal )
				successMissionCnt++;
		}

		if ( successMissionCnt >= listMission.Count )
			isSuccessAllTodayMission = true;

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

// 상대팀과의 승패를 저장할 객체. 상대팀 이름, 승패 저장 
public class TeamRecord 
{
	public string strFightTeamName ;	// 상대팀 이름 
	public int iMyWinCnt ;			// 상대팀에게 이긴 누적 횟수 
	public int iMyLoseCnt ;			// 상대팀에게 진 누적 횟수 

	public TeamRecord( string strFightTeamName, int iMyWinCnt, int iMyLoseCnt )
	{
		this.strFightTeamName   = strFightTeamName ;
		this.iMyWinCnt  		= iMyWinCnt ;
		this.iMyLoseCnt 		= iMyLoseCnt ;  
	}
}

// 자신의팀 이름과 상대팀과의 전적을 저장함. 자신의 전적 리스트 안의 인덱스 번호를 검색하여 접근한다. 
public class InfoOfTeam
{
	public string strMyTeamName	= null;						// 객체 당사자 팀 이름 
	public int iIdxFightTeam 	= 0;						// 현재 대결 팀의 리스트 인덱스 번호 
	public List <TeamRecord> listTeamRecord = new List<TeamRecord>();	// 자신을 제외한 상대 팀들과의 전적 

	public InfoOfTeam( string strMyTeamName, string[] otherTeamNames  )
	{
		// 상대팀 전적 리스트 초기화, 내 팀 이름 저장 
		this.strMyTeamName = strMyTeamName; 
		for( int i=0 ; i < otherTeamNames.Length ; i++ )
		{
			if( otherTeamNames[i] != strMyTeamName )
				listTeamRecord.Add( new TeamRecord ( otherTeamNames[i], 0, 0 ) );
		}
	}

	// 상대 팀 이름 찾아서 인덱스 저장.
	public void FindIdxOfNextFightTeam()
	{
		for( int i=0 ; i < listTeamRecord.Count ; i++ )
		{
			if( listTeamRecord[i].strFightTeamName == strMyTeamName )
			{
				iIdxFightTeam = i;
				break;
			}

		}
	}

	public void FindIdxOfNextFightTeam( string nameOfNextFightTeam )
	{
		for( int i=0 ; i < listTeamRecord.Count ; i++ )
		{
			if( listTeamRecord[i].strFightTeamName == nameOfNextFightTeam )
			{
				iIdxFightTeam = i;
				break;
			}			
		}
	}

	// 게임 결과 처리, 상대 팀들과 전적에 매개변수에 따라 승패 저장.  
	public void PlayResult( bool WinOfMyTeam )
	{
		if ( WinOfMyTeam )
			Win ();
		else
			Lose ();
	}
	
	void Win()
	{
		listTeamRecord [iIdxFightTeam].iMyWinCnt++;
	}
	
	void Lose()
	{
		listTeamRecord [iIdxFightTeam].iMyLoseCnt++;
	}
}

// 오늘의 미션 피드에서 각각의 미션창이 받을 미션 내용 객체의 클래스
public class MissionData
{
	public MissionType Type;
	public string missionContent = null ; 
	public int fullSuccVal 		 = 0 ;
	public int nowSuccVal  		 = 0 ; 

	public MissionData( string missionContent, int fullSuccVal, MissionType Type )
	{
		this.missionContent = missionContent; 
		this.fullSuccVal = fullSuccVal; 
		this.Type = Type;
	}
}

// 전력분석 랜덤값 만들기 위한 클래스 
public class PowerRandData
{
	private float	fRandomValue;
	private float	fRate;
	private float 	fMinValue;
	private float 	fMaxValue;

	public PowerRandData( float fMinValue, float fMaxValue, int iRound )
	{
		this.fMinValue = fMinValue;
		this.fMaxValue = fMaxValue;

		fRandomValue =	Random.Range( fMinValue, fMaxValue );

		// 소수점 iRound자리 까지 반올림함.
		float fRoundTmp = 1f; 
		if (iRound == 2)
			fRoundTmp = 100f;
		else if (iRound == 3)
			fRoundTmp = 1000f;

		fRandomValue = ( Mathf.Round( fRandomValue	* fRoundTmp ) )/ fRoundTmp;

		CreatePersentageVal ();
	}

	public PowerRandData( int iMinValue, int iMaxValue )
	{
		this.fMinValue = (float)iMinValue;
		this.fMaxValue = (float)iMaxValue;
		
		fRandomValue = (float)(	Random.Range( iMinValue, iMaxValue+1 ) ); 
		CreatePersentageVal ();
	}

	void CreatePersentageVal()
	{
		if ( fRandomValue - fMinValue == 0 )
			fRate = 0 ;
		else
			fRate = ( fRandomValue - fMinValue )/( fMaxValue - fMinValue ) ; 
	}

	public float GetRate()
	{
		return fRate;
	}
	
	public float GetRandomValue()
	{
		return fRandomValue;
	}
} 

// MVP 피드 내용 저장 
public class MVPData
{
	private string strContent = null ;
	private bool isSelect = false ;

	public MVPData( string strContent )
	{
		this.strContent = strContent; 
	}

	public string GetStrContent()
	{
		return strContent;
	}

	public void SetIsSelect()
	{
		isSelect = true; 
	}

	public bool GetIsSelect()
	{
		return isSelect;
	}

}


