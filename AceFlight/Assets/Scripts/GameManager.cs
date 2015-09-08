using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	Vector3 mlFirstPos 		= new Vector3( 0, 0, 0 );
	Quaternion mlFirstAngle = Quaternion.Euler(0, 0, 0);

	int frameCnt = 0; 			// 프레임 카운트 
	bool monEnterSwc = false; 	// monster enter switch 

	public GameObject MonsterLine ; 

	void Awake()
	{
		Application.targetFrameRate = GameData.Instance.framePerSec; //60;	// 초당 프레임수 고정 
	}
	void Start () 
	{
		initPlayScene ();
		MonsterLine.transform.localPosition = mlFirstPos ;
		MonsterLine.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (frameCnt == 0)
//			monEnterSwc = true;
//		else
			monEnterSwc = false;

		frameCnt++;


		if( frameCnt % ( GameData.Instance.pixelPerMeter / GameData.Instance.pixelPerFrame ) == 0 )
		{
			monEnterSwc = true;
			GameData.Instance.scDistFromStage++;
//			Debug.Log("frameCnt : "+frameCnt+", scDistFromStage : " + GameData.Instance.scDistFromStage );
		}

		// enter monster 
		if( GameData.Instance.scDistFromStage % 30 == 0 && monEnterSwc == true )
		{
			GameObject MonLineInstance = (GameObject) Instantiate( MonsterLine , mlFirstPos, mlFirstAngle ) ; 
			MonLineInstance.SetActive(true);
//			Debug.Log(GameData.Instance.scDistFromStage);
//			Debug.Log(" enter monster ");
		}




//		if(Input.GetMouseButton(0) == true)	// 왼쪽 마우스 눌렀을때!!!
//		{
//			Input.mousePosition	// vector3로 받아옴.  
// 광재님이 주신 팁!!!!!! 유니티 NGUI 개발 팁!!!!
// 스프라이트 하나하나 조각조각 이쁘게.
// transform에서 날개 안바꿔도됨 UISprite의 Flip을 Horizontally를 바꿔줌 
// uiEventTrigger 에서 해당 객체에 관련 이벤트가 있을때 처리해줌
// ngui에서 이벤트는 트윈써라!!!!!. 애니메이션도 트윈써라!!!!!
// 		핑퐁 쓰면 같은 반복 2개 돌릴 수 있음 
// 스프라이트등등 편하게 추가하려면 위젯툴 쓰셩
// press일때, 위치받고, 마우스 위치받아서 쏼라쏼라 하면 될듯 
//		}

	}

	// 게임결과창 에서 다시 게임 할때, 처음 게임할때 초기화해야할 변수들 생각해서 만들기 ㄴ
	void initPlayScene()
	{
		GameData.Instance.scGoldFromStage = 0;
		GameData.Instance.scHitMonFromStage = 0;
		GameData.Instance.scDistFromStage = 0 ;

		GameData.Instance.pixelPerFrame = 2;
	}



}
