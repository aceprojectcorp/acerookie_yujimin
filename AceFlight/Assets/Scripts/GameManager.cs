using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState { title, play, result, loading }

public class GameManager : MonoBehaviour {
	

//	public UIPanel playerP ;
//	public UIDragObject playerDg;
	
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
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

	}


}
