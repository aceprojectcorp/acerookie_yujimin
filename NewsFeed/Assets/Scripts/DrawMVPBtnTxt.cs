using UnityEngine;
using System.Collections;

public class DrawMVPBtnTxt : MonoBehaviour 
{
	MVPData mdata ; 
	UILabel lbBtn;
	GameObject goParentObj ; 

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach ( Transform child in transforms )
		{
			switch ( child.name )
			{
			case "SelectAnswar_Label":
				lbBtn = child.GetComponent<UILabel>();
				break;
			}
		} 
	}
	void Awake() 
	{
		OnGetChildObject ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMVPData( MVPData mdata )
	{
		this.mdata = mdata;
		lbBtn.text = mdata.GetStrContent();
	}

	public void OnClickMVPBtn()
	{
		mdata.SetIsSelect ();
	}
}
