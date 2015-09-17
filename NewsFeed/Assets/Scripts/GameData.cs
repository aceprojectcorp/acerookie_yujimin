using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public int numOfNowFightTeam = 0 ; 		// 현재 대결 팀과 경기 횟수
	public int idxFightTeam = 0 ;  			// 현재 대결 팀의 배열 인덱스 번호 

	public string nameOfMyTeam = "MyTeam" ;	// 우리 팀 이름 
	public string[] teamNamesArr ; 			// 전체 팀 이름 
	//	= new string[]{ "A", 	"B", 	"C", 	"D", 	"E", 	"F", 	"G", 	"H", 	"I", 	"J", nameOfMyTeam };
	public string nameOfNextFightTeam = null ;// 현재 대결 팀의 이름 


	public List <InfoOfTeam> infoOtherTeam = new List<InfoOfTeam> ();		// 전체 팀 객체
	public InfoOfMyTeam infoOfMyTeam ;		// 내 팀 정보 객체 
	public InfoOfTeam infoOfNowFightTeam;	// 현재 대결 팀 객체 (계속 바뀜. infoOtherTeam안에서) 

	void Awake()
	{		
		if( m_instance == null )
			m_instance = this;

		DontDestroyOnLoad (gameObject);

		// 전체 팀 이름 초기화  // 마지막엔 무조건 자신의 팀 이름이 들어감 
		teamNamesArr = new string[]
		{ "A", 	"B", 	"C", 	"D", 	"E", 	"F", 	"G", 	"H", 	"I", 	"J", nameOfMyTeam };

		// 전체 팀 객체 생성( 내꺼 빼고 )
		for(int i=0 ; i < teamNamesArr.Length-1 ; i++ ) 
		{
			infoOtherTeam.Add( new InfoOfTeam( teamNamesArr[i], teamNamesArr ) );	 
		}

		// 내 팀 정보 객체 생성 
		infoOfMyTeam = new InfoOfMyTeam( nameOfMyTeam, teamNamesArr );

		nameOfNextFightTeam = teamNamesArr [idxFightTeam];
		infoOfNowFightTeam = infoOtherTeam[idxFightTeam];
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
		this.OppoTeamName  = OppoTeamName ;
		this.myWinRec  = myWinRec ;
		this.myLoseRec = myLoseRec ;
	}
}


public class InfoOfTeam
{
	public string myTeamName 		= null;							// 객체 당사자 팀 이름 
	public List <TeamRecord> teamRecord = new List<TeamRecord>();	// 자신을 제외한 상대 팀들과의 전적 
	public int idxOfNowTeamListNum = 0;								// 현재 대결 팀의 리스트 인덱스 번호 

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

	// 상대 팀 이름 찾아서 인덱스 저장. 현재는 우리팀 이름 인덱스만 찾아서 저장
	void FindFightTeam()
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

	// 게임 결과 처리, 상대 팀들과 전적에 매개변수에 따라 승패 저장.  
	public void PlayResult( bool WinOfMyTeam )
	{
		FindFightTeam ();

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


// 내부적으로 데이터만 저장/처리하자 ( 승/패/연승/연패/경기횟수 )
// 내 정보용 객체.. 
public class InfoOfMyTeam
{
	string myTeamName 		= null;		// 객체 당사자 팀 이름 
	string nowFightTeamName = null; 	// 현재 상대팀 이름
	public int idxOfNowTeamListNum = 0;	// 현재 상대팀 리스트 인덱스 저장 
	int numOfTotalPlay 		= 0;		// 총 게임 횟수 
	int numOfStraightWin 	= 0;		// 연승 횟수
	int numOfStraightLoss 	= 0;		// 연패 횟수 
	bool isPreGameWin 		= false;	// 이전게임 승/패 여부 연승, 연패를 따지기 위해. 
	
	public List <TeamRecord> teamRecord = new List<TeamRecord>(); // 자신을 제외한 상대 팀들과의 전적 

	public InfoOfMyTeam( string myTeamName, string[] otherTeamNames )
	{
		this.myTeamName = myTeamName; 
		for( int i=0 ; i < otherTeamNames.Length ; i++ )
		{
			if( otherTeamNames[i] != myTeamName )
				teamRecord.Add( new TeamRecord ( otherTeamNames[i], 0, 0 ) );
		}
		idxOfNowTeamListNum = 0; 
		nowFightTeamName = teamRecord [ idxOfNowTeamListNum ].OppoTeamName;
	}

// win,lose에서 연승, 연패, 상대팀과의 전적값 처리 
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
		numOfStraightLoss = 0;
		++numOfStraightWin;
		++numOfTotalPlay;
	} 

	void Lose()
	{
		teamRecord [idxOfNowTeamListNum].myLoseRec++;
		numOfStraightWin = 0;
		++numOfStraightLoss;
		++numOfTotalPlay;
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
}


