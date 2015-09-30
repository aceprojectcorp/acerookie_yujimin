using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassFile{
	
}

// 상대팀과의 승패를 저장할 객체. 상대팀 이름, 승패 저장 
public class TeamRecord 
{
	public string	strFightTeamName ;		// 상대팀 이름 
	public int		iMyWinCnt ;				// 상대팀에게 이긴 누적 횟수 
	public int		iMyLoseCnt ;			// 상대팀에게 진 누적 횟수 
	
	public TeamRecord( string strFightTeamName, int iMyWinCnt, int iMyLoseCnt )
	{
		this.strFightTeamName   = strFightTeamName ;
		this.iMyWinCnt  		= iMyWinCnt ;
		this.iMyLoseCnt 		= iMyLoseCnt ;  
	}
}


// 자신의팀 이름과 상대팀과의 전적을 저장함. 자신의 전적 리스트 안의 인덱스 번호를 검색하여 접근한다. 
public class InformationTeam
{
	public string 	strMyTeamName	= null;		// 객체 당사자 팀 이름 
	public int 		iIdxFightTeam 	= 0;		// 현재 대결 팀의 리스트 인덱스 번호 

	public List <TeamRecord> TeamRecordList = new List<TeamRecord>();	// 자신을 제외한 상대 팀들과의 전적 

	// 상대팀 전적 리스트 초기화, 내 팀 이름 저장 
	public InformationTeam( string strMyTeamName, string[] arrStrNamesOtherTeam  )
	{
		this.strMyTeamName = strMyTeamName; 
		for( int i=0 ; i < arrStrNamesOtherTeam.Length ; i++ )
		{
			if( arrStrNamesOtherTeam[i] != strMyTeamName )
				TeamRecordList.Add( new TeamRecord ( arrStrNamesOtherTeam[i], 0, 0 ) );
		}
	}
	
	// 상대 팀 이름 찾아서 인덱스 저장.	
	public void FindIdxNextTeam( string strNameNextTeam )
	{
		for( int i=0 ; i < TeamRecordList.Count ; i++ )
		{
			if( TeamRecordList[i].strFightTeamName == strNameNextTeam )
			{
				iIdxFightTeam = i;
				break;
			}			
		}
	}
	
	// 게임 결과 처리, 상대 팀들과 전적에 매개변수에 따라 승패 저장.  
	public void PlayResult( bool isWinMyTeam )
	{
		if ( isWinMyTeam )
			Win ();
		else
			Lose ();
	}
	
	void Win()
	{
		TeamRecordList [iIdxFightTeam].iMyWinCnt++;
	}
	
	void Lose()
	{
		TeamRecordList [iIdxFightTeam].iMyLoseCnt++;
	}
}

// 오늘의 미션 피드에서 각각의 미션창이 받을 미션 내용 객체의 클래스
public class MissionData
{
	public MissionType 	mType;
	public string 		strMissionContent 	 = null ; 
	public int 			iFullSuccVal 		 = 0 ;
	public int 			iNowSuccVal  		 = 0 ; 
	
	public MissionData( string strMissionContent, int iFullSuccVal, MissionType mType )
	{
		this.strMissionContent = strMissionContent; 
		this.iFullSuccVal = iFullSuccVal; 
		this.mType = mType;
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
		if ( iRound == 2 )
			fRoundTmp = 100f;
		else if ( iRound == 3 )
			fRoundTmp = 1000f;
		
		fRandomValue = ( Mathf.Round( fRandomValue * fRoundTmp ) )/ fRoundTmp;
		
		CreateRateVal ();
	}
	
	public PowerRandData( int iMinValue, int iMaxValue )
	{
		this.fMinValue = (float)iMinValue;
		this.fMaxValue = (float)iMaxValue;
		
		fRandomValue = (float)(	Random.Range( iMinValue, iMaxValue+1 ) ); 
		CreateRateVal ();
	}
	
	void CreateRateVal()
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
	private string	strContent	= null ;
	private bool	isSelect	= false ;
	
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

