using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO : 리스트 이름 다 바꿔주기[여기가 기준]!!! 변수이름, 클래스 이름도 다시 짧고 예쁘게 다듬기!!
// << 전력분석 테이블의 랜덤 데이터 리스트 생성/관리/연결, 게임실행 버튼 연동, 상대팀 이름 라벨에 출력 및 이동 >>
public class HandlingFeedPowerAnalysis : MonoBehaviour 
{
	private UILabel lbFightTeamName ;
	private UILabel lbContentUp;
	
	private List<DrawTableAnalysis> Analysis_ScriptList = new List<DrawTableAnalysis>();

	// 전력 데이터 객체 생성 
	private List<PowerRandData> PowerDataList_MyTeam 	= new List<PowerRandData>();
	private List<PowerRandData> PowerDataList_FightTeam	= new List<PowerRandData>();

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch ( child.name )
			{
			case "Title_FightTeam_Label" :
				lbFightTeamName = child.GetComponent<UILabel>();
				break;
				
			case "Title_ContentUp_Label" :
				lbContentUp = child.GetComponent<UILabel>();
				break;

			case "MyTeam":
				Analysis_ScriptList.Add( child.GetComponent<DrawTableAnalysis>() );
				Analysis_ScriptList [ Analysis_ScriptList.Count-1 ].SetData ( PowerDataList_MyTeam, GameData.Instance.myTeamObj.strMyTeamName );
				break;
				
			case "FightTeam":
				Analysis_ScriptList.Add( child.GetComponent<DrawTableAnalysis>() );
				Analysis_ScriptList [ Analysis_ScriptList.Count-1 ].SetData ( PowerDataList_FightTeam, GameData.Instance.fightTeamObj.strMyTeamName );
				break;
			}			
		}
	}

	void Start () 
	{
		SetPdata ( PowerDataList_MyTeam 	);
		SetPdata ( PowerDataList_FightTeam	);

		OnGetChildObject ();
		SetLabelTeamData ();	// UILabel 객체에 text값 변경 및 위치 이동 ( 상대팀 이름 글자수에 따라 뒤에 라벨(lbContentUp)이 뒤로 밀림 )
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void SetPdata( List <PowerRandData> list )
	{
		list.Add ( new PowerRandData ( 0.1f,	0.6f,	3	) );	// [0] 타율
		list.Add ( new PowerRandData ( 1.5f,	7.5f,	2	) );	// [1] 방어율 
		list.Add ( new PowerRandData ( 0, 		10			) );	// [2] 볼넷 
		list.Add ( new PowerRandData ( 0,		10			) );	// [3] 도루 
		list.Add ( new PowerRandData ( 0,		5			) );	// [4] 홈런
	}

	// 게임실행 버튼 눌렀을때, 시뮬레이션 팝업 실행
	public void OnClickPlayGame()
	{
		UIFeedManager.Instance.OnClickSimulBtn ();		
	}
	
 	// UILabel객체에 text값 변경(상대팀 이름 주기) 및 위치 이동(상대팀 이름 가로 크기에 따라서 뒤에 라벨 뒤로 밀림)  
	void SetLabelTeamData()
	{ 
		lbFightTeamName.text 	= GameData.Instance.fightTeamObj.strMyTeamName;
		
		// 팀 이름 가로크기에 따라 lbContentUp 위치 이동 
		Vector3 lbContentPosTmp = lbContentUp.transform.localPosition; 
		lbContentPosTmp.x = lbFightTeamName.transform.localPosition.x +
							lbFightTeamName.width + 1;
		lbContentUp.transform.localPosition = lbContentPosTmp;
	}

}


