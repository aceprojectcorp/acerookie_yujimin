using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// << HandlingFeedPowerAnalysis로부터 Powerdata를 받아서(SetData()로 받음), 자신의 전력분석 테이블 출력 >>
public class DrawTableAnalysis : MonoBehaviour {

//	private PowerData pData;
	private List <PowerRandData> PowerDataList ;

	private UILabel lbTeamName;
	private UILabel lbRecord;

	private UILabel lbBattingAver;
	private UILabel lbERA;
	private UILabel lb4Balls;
	private UILabel lbStealBase;
	private UILabel lbHomeRun;

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


			case "BattingAver_Label" :
				lbBattingAver = child.GetComponent<UILabel>();
				break;
				
			case "ERA_Label" :
				lbERA = child.GetComponent<UILabel>();
				break;
				
			case "4Balls_Label" :
				lb4Balls = child.GetComponent<UILabel>();
				break;
				
			case "StealBase_Label" :
				lbStealBase = child.GetComponent<UILabel>();
				break;
				
			case "HomeRun_Label" :
				lbHomeRun = child.GetComponent<UILabel>();
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
		PowerDataList = list;
		
		lbTeamName.text = strNameTeam ;
		if( strNameTeam == GameData.Instance.infoMyTeamObj.strMyTeamName ) 
		{
			lbRecord.text 	= GameData.Instance.infoMyTeamObj.TeamRecordList[ GameData.Instance.infoMyTeamObj.iIdxFightTeam ].iMyWinCnt + "승 0무 " +
							  GameData.Instance.infoMyTeamObj.TeamRecordList[ GameData.Instance.infoMyTeamObj.iIdxFightTeam ].iMyLoseCnt + "패";
		}
		else
		{
			lbRecord.text 	= GameData.Instance.infoFightTeamObj.TeamRecordList[ GameData.Instance.infoFightTeamObj.iIdxFightTeam ].iMyWinCnt + "승 0무 " +
							  GameData.Instance.infoFightTeamObj.TeamRecordList[ GameData.Instance.infoFightTeamObj.iIdxFightTeam ].iMyLoseCnt + "패";	
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

	void SetLabel()
	{ 
		lbBattingAver.text 	= ( PowerDataList[0].GetRandomValue() ).ToString() ;
		lbERA.text 			= ( PowerDataList[1].GetRandomValue() ).ToString() ;
		lb4Balls.text 		= ( PowerDataList[2].GetRandomValue() ).ToString() ;
		lbStealBase.text 	= ( PowerDataList[3].GetRandomValue() ).ToString() ;
		lbHomeRun.text 		= ( PowerDataList[4].GetRandomValue() ).ToString() ;
	}
	
	void SetProgressBar()
	{
		pgBattingAver.value = PowerDataList [0].GetRate ();
		pgERA.value 		= PowerDataList [1].GetRate ();
		pg4Balls.value 		= PowerDataList [2].GetRate ();
		pgStealBase.value  	= PowerDataList [3].GetRate ();
		pgHomeRun.value 	= PowerDataList [4].GetRate ();
	}	
}
