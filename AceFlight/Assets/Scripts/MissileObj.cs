using UnityEngine;
using System.Collections;

public class MissileObj : MonoBehaviour {

	public int m_msiPower = 0 ;

	Vector3 m_msiMovePos 	= Vector3.zero;
	float m_msiDestoryPosY	= 0; 
	int m_idxThisMsiPower 	= 0 ; 

	// Use this for initialization
	void Start () 
	{
		m_idxThisMsiPower = GameData.Instance.idxMsiPower;
		m_msiPower = GameData.Instance.infoMsiPowerPerMeter[ m_idxThisMsiPower, 1];
		m_msiMovePos = gameObject.transform.localPosition; 
		m_msiDestoryPosY = ( gameObject.GetComponent<UIWidget>().localSize.y + GameData.Instance.screenHeight ) /2 ; 
	}
	
	// Update is called once per frame 
	void Update () 
	{
		if (GameData.Instance.playerState == PlayerState.play) 
		{
			// move ( 1frame, 64px move ) 
			m_msiMovePos.y += (GameData.Instance.msiMovePixelPerFrame * GameData.Instance.framePerSec) * Time.deltaTime; 
			gameObject.transform.localPosition = m_msiMovePos; 
		}

		// gameobject destory
		if ( gameObject.transform.localPosition.y > m_msiDestoryPosY )
			Destroy ( gameObject ); 
	}

	void OnTriggerEnter( Collider other )
	{
		if ( other.transform.tag == "Monster")
		{
			Destroy(gameObject);
		}
	}

}
