using UnityEngine;
using System.Collections;

public class HandlingFeedTiredness : MonoBehaviour 
{
	private UISprite	sprBg ;
	private UISprite	sprPortrait ; 
	private UILabel		lbContentDown; 

	private GameObject	goBtnSelect ;

	private float		fDownScaleHeightBg = 20f;

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach ( Transform child in transforms )
		{
			switch ( child.name )
			{
			case "Portrait_Sprite" :
				sprPortrait = child.GetComponent<UISprite>();
				break;

			case "Title_ContentDown_Label" :
				lbContentDown = child.GetComponent<UILabel>();
				break;

			case "RecoverDirect_Btn" :
				goBtnSelect = child.gameObject;
				break;		

			}
		} 
	}

	void Awake()
	{
		OnGetChildObject ();
		sprBg = gameObject.GetComponent<UISprite> ();
	}

	// Use this for initialization
	void Start () 
	{
		lbContentDown.text = GameData.Instance.AddColorText( "즉시 회복 시 1200AP가 소모됩니다.", "yellow" ) ;
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	// 버튼 누를 경우 결과 처리 
	public void SetResultPressBtn()
	{
		lbContentDown.text = GameData.Instance.AddColorText( "선수의 피로가 회복되었습니다.", "red" ) ;
		Destroy ( goBtnSelect ); 
		UIFeedManager.Instance.DownSizeHeihgtBg ( sprBg, sprPortrait, fDownScaleHeightBg );
	}

}
