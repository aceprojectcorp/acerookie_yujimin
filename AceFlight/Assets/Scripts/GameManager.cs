using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState { title, play, result, loading }

public class GameManager : MonoBehaviour {
	
	public UIRoot playUiRoot ;
//	public UIPanel playerP ;
//	public UIDragObject playerDg;

	Vector3 playerPos = new Vector3(0,0,0);

	int screenWidth = 0; 
	int screenHeight = 0;
	
	void Start () 
	{
		screenWidth = playUiRoot.manualWidth;
		screenHeight = playUiRoot.manualHeight;
	
		initPlayScene ();

	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(Input.GetMouseButton(0) == true)	// 왼쪽 마우스 눌렀을때!!!
//		{
//			Input.mousePosition	// vector3로 받아옴.  
// 스프라이트 하나하나 조각조각 이쁘게.
// transform에서 날개 안바꿔도됨 UISprite의 Flip을 Horizontally를 바꿔줌 
// uiEventTrigger 에서 해당 객체에 관련 이벤트가 있을때 처리해줌
// ngui에서 이벤트는 트윈써라!!!!!. 애니메이션도 트윈써라!!!!!
// 		핑퐁 쓰면 같은 반복 2개 돌릴 수 있음 
// 스프라이트등등 편하게 추가하려면 위젯툴 쓰셩
// press일때, 위치받고, 마우스 위치받아서 쏼라쏼라 하면 될듯 
//		}

	}

	void bgMove( UISprite bg )
	{

	}

	void initPlayScene()
	{
		/*playerPos = playerP.transform.localPosition;
		playerPos.y -= (screenHeight / 4f); // * 3f;
		Debug.Log (playerPos);
		Debug.Log ( playerP.transform.position );
		playerP.transform.localPosition = playerPos;*/
	}


}
