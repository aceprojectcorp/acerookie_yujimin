using UnityEngine;
using System.Collections;

public class MissileObj : MonoBehaviour {

	public int m_msiPower = 0 ;

	Vector3 msiMovePos = Vector3.zero;
	float msiDestoryPosY		= 0; 

	// Use this for initialization
	void Start () 
	{
		m_msiPower = GameData.Instance.infoMsiPowerPerMeter[ GameData.Instance.idxMsiPower, 1];
		msiMovePos = gameObject.transform.localPosition; 
		msiDestoryPosY = ( gameObject.GetComponent<UIWidget>().localSize.y + GameData.Instance.screenHeight ) /2 ; 
	}
	
	// Update is called once per frame 
	void Update () 
	{
		// move ( 1frame, 64px move ) 
		msiMovePos.y += ( GameData.Instance.msiMovePixelPerFrame * GameData.Instance.framePerSec ) * Time.deltaTime ; 
		gameObject.transform.localPosition = msiMovePos; 

		// gameobject destory
		if (gameObject.transform.localPosition.y > msiDestoryPosY)
			Destroy (gameObject); 
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Monster")
		{
			Destroy(gameObject);
		}
	}

}
