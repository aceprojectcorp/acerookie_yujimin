using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public int numOfNowFightTeam = 0 ; 			// 현재 대결 팀과 경기 횟수
	public int idxFightTeam = 0 ;  				// 현재 대결 팀의 배열 인덱스 번호 

	public int numOfTotalPlay 		= 0;		// 총 게임 횟수 
	public int numOfTotalWin 		= 0;		// 
	public int numOfStraightWin 	= 0;		// 연승 횟수
	public int numOfStraightLoss 	= 0;		// 연패 횟수 
	//-----------------------// 
	public bool isSucssesTodayMs 	= false;

	public string nameOfMyTeam = "MyTeam" ;		// 우리 팀 이름 
	public string[] teamNamesArr ; 				// 전체 팀 이름 
	//	= new string[]{ "A", 	"B", 	"C", 	"D", 	"E", 	"F", 	"G", 	"H", 	"I", 	"J", nameOfMyTeam };
	public string nameOfNowFightTeam = null ;	// 현재 대결 팀의 이름 
	 
	public InfoOfTeam infoOfMyTeam ;			// 내 팀 정보 객체 
	public InfoOfTeam infoOfNowFightTeam;		// 현재 대결 팀 객체 (계속 바뀜. infoAllTeam안에서) 
	public List <InfoOfTeam> infoAllTeam = new List<InfoOfTeam> ();		// 전체 팀 객체
	public List <missionData> missionList = new List<missionData>();



	void Awake()
	{		
		if( m_instance == null )
			m_instance = this;

		DontDestroyOnLoad (gameObject);

		// 전체 팀 이름 초기화  
		teamNamesArr = new string[]
		{ "A", 	"B", 	"C", 	"D", 	"E", 	"F", 	"G", 	"H", 	"I", 	"J", nameOfMyTeam };

		// 전체 팀 객체 생성
		for(int i=0 ; i < teamNamesArr.Length ; i++ ) 
		{
			infoAllTeam.Add( new InfoOfTeam( teamNamesArr[i], teamNamesArr ) );	 
			if( teamNamesArr[i] == nameOfMyTeam )
				infoOfMyTeam = infoAllTeam[i];	// 내팀 객체 연결 
		}

		infoOfNowFightTeam = infoAllTeam[idxFightTeam];
		nameOfNowFightTeam = teamNamesArr [idxFightTeam];

		missionList.Add ( new missionData ( "3 연승을 하세요.", 		1  ));
		missionList.Add ( new missionData ( "10 승을 하세요.", 		5 ));
		missionList.Add ( new missionData ( "20 경기를 진행하세요 ", 	5 )); 
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
	public string OppoTeamName ;	// 상대팀 이름 
	public int myWinRec ;			// 상대팀에게 이긴 누적 횟수 
	public int myLoseRec ;			// 상대팀에게 진 누적 횟수 

	public TeamRecord( string OppoTeamName, int myWinRec, int myLoseRec )
	{
		this.OppoTeamName   = OppoTeamName ;
		this.myWinRec  		= myWinRec ;
		this.myLoseRec 		= myLoseRec ;  
	}
}

// 자신의팀 이름과 상대팀과의 전적을 저장함. 자신의 전적 리스트 안의 인덱스 번호를 검색하여 접근한다. 
public class InfoOfTeam
{
	public string myTeamName 			= null;						// 객체 당사자 팀 이름 
	public List <TeamRecord> teamRecord = new List<TeamRecord>();	// 자신을 제외한 상대 팀들과의 전적 
	public int idxOfNowTeamListNum 		= 0;						// 현재 대결 팀의 리스트 인덱스 번호 

	public InfoOfTeam( string myTeamName, string[] otherTeamNames  )
	{
		// 상대팀 전적 리스트 초기화, 내 팀 이름 저장 
		this.myTeamName = myTeamName; 
		for( int i=0 ; i < otherTeamNames.Length ; i++ )
		{
			if( otherTeamNames[i] != myTeamName )
				teamRecord.Add( new TeamRecord ( otherTeamNames[i], 0, 0 ) );
		}
	}

	// 상대 팀 이름 찾아서 인덱스 저장.
	public void FindIdxOfNextFightTeam()
	{
		for( int i=0 ; i < teamRecord.Count ; i++ )
		{
			if( teamRecord[i].OppoTeamName == myTeamName )
			{
				idxOfNowTeamListNum = i;
				break;
			}

		}
	}

	public void FindIdxOfNextFightTeam( string nameOfNextFightTeam )
	{
		for( int i=0 ; i < teamRecord.Count ; i++ )
		{
			if( teamRecord[i].OppoTeamName == nameOfNextFightTeam )
			{
				idxOfNowTeamListNum = i;
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
		teamRecord [idxOfNowTeamListNum].myWinRec++;
	}
	
	void Lose()
	{
		teamRecord [idxOfNowTeamListNum].myLoseRec++;
	}
}

public class missionData
{
	public string missionContent = null ; 
	public int fullSuccVal 		 = 0 ;
	public int nowSuccVal  		 = 0 ; 

	public missionData( string missionContent, int fullSuccVal )
	{
		this.missionContent = missionContent; 
		this.fullSuccVal = fullSuccVal; 
	}

}
