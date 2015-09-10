using UnityEngine;
using System.Collections;

// << 이동 거리에 따라 몬스터 객체 생성 >> 
public class MonsterEnter : MonoBehaviour {

	public GameObject MonsterObj ;

	Vector3 monFirstPos 		= Vector3.zero ;
	float[] monStartPosX 		= { -255, -128, 0, 128, 255 };
	float 	monStartPosY 		= 0; 	
	bool 	monEnterSwc 		= false; 	// 몬스터 등장 스위치 
	int 	scDistFromStagePre  = 0 ;		// 거리값이 변경 됐을때만, 몬스터 생성여부 판단하기 위해 이전 거리값 저장  
	
	void Start ()  
	{
		monStartPosY = 544; 	// == gameObject.GetComponent<Collider> ().transform.localScale.y + GameData.Instance.screenHeight )/2 ;  
		scDistFromStagePre = GameData.Instance.scDistFromStage ;
		createMonsters();
	}

	void Update () 
	{
		if( scDistFromStagePre != GameData.Instance.scDistFromStage )
		{
			monEnterSwc = true;
			scDistFromStagePre = GameData.Instance.scDistFromStage ;
		}

		// 몬스터 등장
		if( GameData.Instance.scDistFromStage % GameData.Instance.createMonMeter == 0 && monEnterSwc == true )
		{
			monEnterSwc = false;
			createMonsters();
		}
	}

	void createMonsters()
	{
		for( int i=0 ; i < 5 ; i++ )
		{			
			monFirstPos.x = monStartPosX[i];
			monFirstPos.y = monStartPosY;
			GameObject monInst = Instantiate(MonsterObj) as GameObject;
			monInst.transform.parent = GameObject.Find("Monsters").transform;
			monInst.transform.localScale = Vector3.one;
			monInst.transform.localPosition = monFirstPos;
			
			//GameObject monInst = (GameObject) Instantiate( MonsterObj, monFirstPos, mlFirstAngle );	
			// 위와 같이 사용시, 객체가 잠깐 중앙(0,0,0)에 위치 했다가 자신의 위치로 이동함. 
		}
	}


}
