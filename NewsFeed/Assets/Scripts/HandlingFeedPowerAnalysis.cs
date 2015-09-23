using UnityEngine;
using System.Collections;

// label 추가!!!!, 가로data들 리스트로 저장해서 관리.. list add.. 
// list??
public class HandlingFeedPowerAnalysis : MonoBehaviour 
{
	// 화면 출력용 UI 객체들 
	private UILabel lbTitleFightTeamName ;
	private UILabel lbContentUp;
	private UILabel lbTeamNameMine;
	private UILabel lbTeamNameFightTeam;
	private UILabel lbRecordMine;
	private UILabel lbRecordFightTeam;
	private UILabel lbNumDataMine;
	private UILabel lbNumDataFightTeam;

	private UIProgressBar pgBattingAverMine;
	private UIProgressBar pgBattingAverFightTeam;
	private UIProgressBar pgERAMine;
	private UIProgressBar pgERAFightTeam;
	private UIProgressBar pg4BallsMine;
	private UIProgressBar pg4BallsFightTeam;
	private UIProgressBar pgStealBaseMine;
	private UIProgressBar pgStealBaseFightTeam;
	private UIProgressBar pgHomeRunMine;
	private UIProgressBar pgHomeRunFightTeam;

	// 랜덤값 변수들 최대, 최소 범위 
	float 	fMinValBattingAver 	= 0.1f;
	float 	fMaxValBattingAver 	= 0.6f;
	float 	fMinValERA 			= 1.5f;
	float 	fMaxValERA 			= 7.5f;
	int  	iMinVal4Balls 		= 0;
	int 	iMaxVal4Balls 		= 10;
	int 	iMinValStealBase 	= 0;
	int		iMaxValStealBase 	= 10;
	int 	iMinValHomeRun 		= 0;
	int 	iMaxValHomeRun 		= 5;

	// 랜덤값을 저장할 변수들 
	float fValBattingAverMine;
	float fValBattingAverFightTeam;
	float fValERAMine;
	float fValERAFightTeam;
	int	iVal4BallsMine;
	int iVal4BallsFightTeam;
	int iValStealBaseMine;
	int iValStealBaseFightTeam;
	int iValHomeRunMine;
	int iValHomeRunFightTeam;

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch (child.name)
			{
			//UILabel
			case "Title_FightTeam_Label" :
				lbTitleFightTeamName = child.GetComponent<UILabel>();
				break;
				
			case "Title_ContentUp_Label" :
				lbContentUp = child.GetComponent<UILabel>();
				break;
				
			case "TeamName_Mine_Label" :
				lbTeamNameMine = child.GetComponent<UILabel>();
				break;
				
			case "TeamName_FightTeam_Label" :
				lbTeamNameFightTeam = child.GetComponent<UILabel>();
				break;
				
			case "Record_Mine_Label" :
				lbRecordMine = child.GetComponent<UILabel>();
				break;
				
			case "Record_FightTeam_Label" :
				lbRecordFightTeam = child.GetComponent<UILabel>();
				break;

			case "NumOfData_MyTeam_Label" :
				lbNumDataMine = child.GetComponent<UILabel>();
				break;

			case "NumOfData_FightTeam_Label" :
				lbNumDataFightTeam = child.GetComponent<UILabel>();
				break;

			//UIProgressBar
			case "BattingAver_Mine_Pg" :
				pgBattingAverMine = child.GetComponent<UIProgressBar>();
				break;
				
			case "BattingAver_FightTeam_Pg" :
				pgBattingAverFightTeam = child.GetComponent<UIProgressBar>();
				break;
				
			case "ERA_Mine_Pg" :
				pgERAMine = child.GetComponent<UIProgressBar>();
				break;
				
			case "ERA_FightTeam_Pg" :
				pgERAFightTeam = child.GetComponent<UIProgressBar>();
				break;
				
			case "4Balls_Mine_Pg" :
				pg4BallsMine = child.GetComponent<UIProgressBar>();
				break;
				
			case "4Balls_FightTeam_Pg" :
				pg4BallsFightTeam = child.GetComponent<UIProgressBar>();
				break;
				
			case "StealBase_Mine_Pg" :
				pgStealBaseMine = child.GetComponent<UIProgressBar>();
				break;
				 
			case "StealBase_FightTeam_Pg" :
				pgStealBaseFightTeam = child.GetComponent<UIProgressBar>();
				break;
				
			case "HomeRun_Mine_Pg" :
				pgHomeRunMine = child.GetComponent<UIProgressBar>();
				break;
				
			case "HomeRun_FightTeam_Pg" :
				pgHomeRunFightTeam = child.GetComponent<UIProgressBar>();
				break;
			}
		}
	}

	void Awake()
	{
		OnGetChildObject ();
	}

	void Start () 
	{
		SetRendVal ();		// 전력 관련 변수들에 최소,최대값 범위 안에서 랜덤값 생성하여 저장. 
		SetPgTeamData ();	// UIProgressBar 객체에 값 변경 
		SetLabelTeamData ();// UILabel 객체에 text값 변경 및 위치 이동 
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// 게임실행 버튼 눌렀을때, 시뮬레이션 팝업 실행
	public void OnClickPlayGame()
	{
		UIFeedManager.Instance.OnClickSimulBtn ();		
	}

	// 전력 관련 변수들에 최소,최대값 범위 안에서 랜덤값 생성하여 저장. 
	void SetRendVal()
	{
		fValBattingAverMine 	 = Random.Range( fMinValBattingAver, 	fMaxValBattingAver 	);
		fValBattingAverFightTeam = Random.Range( fMinValBattingAver, 	fMaxValBattingAver 	);
		fValERAMine				 = Random.Range( fMinValERA, 			fMaxValERA 			);
		fValERAFightTeam		 = Random.Range( fMinValERA, 			fMaxValERA 			);
		iVal4BallsMine 			 = Random.Range( iMinVal4Balls, 		iMaxVal4Balls+1		);
		iVal4BallsFightTeam		 = Random.Range( iMinVal4Balls, 		iMaxVal4Balls+1		);
		iValStealBaseMine		 = Random.Range( iMinValStealBase, 		iMaxValStealBase+1 	);
		iValStealBaseFightTeam	 = Random.Range( iMinValStealBase, 		iMaxValStealBase+1 	);
		iValHomeRunMine	 		 = Random.Range( iMinValHomeRun, 		iMaxValHomeRun+1	);
		iValHomeRunFightTeam	 = Random.Range( iMinValHomeRun, 		iMaxValHomeRun+1	);

		// 소수점 2~3자리까지만 값을 가지게 변경 
		fValBattingAverMine 	 = ( Mathf.Round( fValBattingAverMine 		* 1000f ) )/ 1000f;
		fValBattingAverFightTeam = ( Mathf.Round( fValBattingAverFightTeam 	* 1000f ) )/ 1000f; 
		fValERAMine				 = ( Mathf.Round( fValERAMine 				* 100f 	) )/ 100f; 
		fValERAFightTeam		 = ( Mathf.Round( fValERAFightTeam 			* 100f 	) )/ 100f;
	}

 	// UILabel객체에 text값 변경 및 위치 이동 
	void SetLabelTeamData()
	{ 
		lbTitleFightTeamName.text 	= GameData.Instance.fightTeamObj.strMyTeamName;
		lbTeamNameMine.text 	 	= GameData.Instance.myTeamObj.strMyTeamName;
		lbTeamNameFightTeam.text 	= GameData.Instance.fightTeamObj.strMyTeamName;
		
		lbRecordMine.text		 	
		= 	GameData.Instance.myTeamObj.listTeamRecord[ GameData.Instance.myTeamObj.iIdxFightTeam ].iMyWinCnt + "승 0무 " +
			GameData.Instance.myTeamObj.listTeamRecord[ GameData.Instance.myTeamObj.iIdxFightTeam ].iMyLoseCnt + "패";
		lbRecordFightTeam.text 	 	
		=	GameData.Instance.fightTeamObj.listTeamRecord[ GameData.Instance.fightTeamObj.iIdxFightTeam ].iMyWinCnt + "승 0무 " +
			GameData.Instance.fightTeamObj.listTeamRecord[ GameData.Instance.fightTeamObj.iIdxFightTeam ].iMyLoseCnt + "패";
			
		
		lbNumDataMine.text		 	= 	fValBattingAverMine		+"\n"+
										fValERAMine				+"\n"+
										iVal4BallsMine			+"\n"+
										iValStealBaseMine		+"\n"+
										iValHomeRunMine;
		lbNumDataFightTeam.text	 	= 	fValBattingAverFightTeam+"\n"+
										fValERAFightTeam		+"\n"+
										iVal4BallsFightTeam		+"\n"+
										iValStealBaseFightTeam	+"\n"+
										iValHomeRunFightTeam;
		
		// 팀 이름 가로크기에 따라 lbContentUp 위치 이동 
		Vector3 lbContentPosTmp = lbContentUp.transform.localPosition; 
		lbContentPosTmp.x = lbTitleFightTeamName.transform.localPosition.x +
							lbTitleFightTeamName.width + 1;
		lbContentUp.transform.localPosition = lbContentPosTmp;


	}
	
	// UIProgressBar 객체에 값 변경 
	void SetPgTeamData()
	{
		pgBattingAverMine.value		 = GetPersentageVal ( fMinValBattingAver,	fMaxValBattingAver,	fValBattingAverMine 		); 
		pgBattingAverFightTeam.value = GetPersentageVal ( fMinValBattingAver,	fMaxValBattingAver,	fValBattingAverFightTeam	); 
		pgERAMine.value 			 = GetPersentageVal ( fMinValERA, 			fMaxValERA,			fValERAMine					); 
		pgERAFightTeam.value 		 = GetPersentageVal ( fMinValERA,			fMaxValERA,			fValERAFightTeam			); 
		pg4BallsMine.value 			 = GetPersentageVal ( iMinVal4Balls,		iMaxVal4Balls,		iVal4BallsMine				); 
		pg4BallsFightTeam.value 	 = GetPersentageVal ( iMinVal4Balls,		iMaxVal4Balls,		iVal4BallsFightTeam 		); 
		pgStealBaseMine.value  		 = GetPersentageVal ( iMinValStealBase,		iMaxValStealBase,	iValStealBaseMine			); 
		pgStealBaseFightTeam.value	 = GetPersentageVal ( iMinValStealBase,		iMaxValStealBase,	iValStealBaseFightTeam		); 
		pgHomeRunMine.value 		 = GetPersentageVal ( iMinValHomeRun,		iMaxValHomeRun,		iValHomeRunMine				); 
		pgHomeRunFightTeam.value 	 = GetPersentageVal ( iMinValHomeRun,		iMaxValHomeRun,		iValHomeRunFightTeam		); 
	}

	// 전적관련 변수를 백분율로 변환하여 반환
	float GetPersentageVal( float minVal, float maxVal, float randVal )
	{
		if ( randVal - minVal == 0 )
			return 0;
		else
			return ( randVal - minVal )/( maxVal-minVal ) ; 
	}

	float GetPersentageVal( int minVal, int maxVal, int randVal ) 
	{
		if ( randVal - minVal == 0 )
			return 0;
		else
			return (float)( randVal - minVal )/(float)( maxVal-minVal ) ; 
	}

	

}
