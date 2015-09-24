using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// << HandlingFeedPowerAnalysis로부터 Powerdata를 받아서(SetData()로 받음), 자신의 전력분석 테이블 출력 >>
public class DrawTableAnalysis : MonoBehaviour {

//	private PowerData pData;
	private List <PowerRandData> pdList ;

	private UILabel lbTeamName;
	private UILabel lbRecord;
	private UILabel lbNumData;

	private UIProgressBar pgBattingAver;
	private UIProgressBar pgERA;
	private UIProgressBar pg4Balls;
	private UIProgressBar pgStealBase;
	private UIProgressBar pgHomeRun;


	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach ( Transform child in transforms )
		{
			switch ( child.name )
			{

			case "TeamName_Label" :
				lbTeamName = child.GetComponent<UILabel>();
				break;			
				
			case "Record_Label" :
				lbRecord = child.GetComponent<UILabel>();
				break;

			case "NumOfData_Label" :
				lbNumData = child.GetComponent<UILabel>();
				break;

			case "BattingAver_Pg" :
				pgBattingAver = child.GetComponent<UIProgressBar>();
				break;
				
			case "ERA_Pg" :
				pgERA = child.GetComponent<UIProgressBar>();
				break;
				
			case "4Balls_Pg" :
				pg4Balls = child.GetComponent<UIProgressBar>();
				break;
				
			case "StealBase_Pg" :
				pgStealBase = child.GetComponent<UIProgressBar>();
				break;
				
			case "HomeRun_Pg" :
				pgHomeRun = child.GetComponent<UIProgressBar>();
				break;
			}
		}
	}

	// 전력분석 데이터를 받고 데이터를 출력.
	public void SetData( List <PowerRandData> list, string strNameTeam ) 
	{
		pdList = list;
		
		lbTeamName.text = strNameTeam ;
		if( strNameTeam == GameData.Instance.myTeamObj.strMyTeamName ) 
		{
			lbRecord.text 	= GameData.Instance.myTeamObj.listTeamRecord[ GameData.Instance.myTeamObj.iIdxFightTeam ].iMyWinCnt + "승 0무 " +
							  GameData.Instance.myTeamObj.listTeamRecord[ GameData.Instance.myTeamObj.iIdxFightTeam ].iMyLoseCnt + "패";
		}
		else
		{
			lbRecord.text 	= GameData.Instance.fightTeamObj.listTeamRecord[ GameData.Instance.fightTeamObj.iIdxFightTeam ].iMyWinCnt + "승 0무 " +
							  GameData.Instance.fightTeamObj.listTeamRecord[ GameData.Instance.fightTeamObj.iIdxFightTeam ].iMyLoseCnt + "패";	
		}
		
		SetLabel();
		SetProgressBar();
	}
	
	void Awake()
	{
		OnGetChildObject ();
	}
	
	// Use this for initialization
	void Start () 
	{		
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	// TODO: 나중에 라벨 추가해서 따로 따로 연결해주기.. 
	void SetLabel()
	{ 
		string strTmp = null;
		for( int i =0 ; i < pdList.Count ; i++ )
		{
			strTmp += pdList[i].GetRandomValue();
			if( i != pdList.Count-1 )
				strTmp += "\n" ;
		}

		lbNumData.text = strTmp;
	}
	
	void SetProgressBar()
	{
		pgBattingAver.value = pdList [0].GetRate ();
		pgERA.value 		= pdList [1].GetRate ();
		pg4Balls.value 		= pdList [2].GetRate ();
		pgStealBase.value  	= pdList [3].GetRate ();
		pgHomeRun.value 	= pdList [4].GetRate ();
	}	
}
