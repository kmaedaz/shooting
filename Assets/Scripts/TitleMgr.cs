using UnityEngine;
using System.Collections;

public class TitleMgr : MonoBehaviour {
	
	//
	bool _bDrawPressSatrt = false;
	
	// Use this for initialization
	IEnumerator Start () {
		while(true)
		{
			_bDrawPressSatrt = ! _bDrawPressSatrt;
			yield return new WaitForSeconds(0.6f);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Main");
		}
		
	}
	
	void OnGUI()
	{
		if( _bDrawPressSatrt )
		{
			//
			//
			float w =128;
			float h=32;
			float px=Screen.width/2 -w/2;
			float py=Screen.height/2 - h/2;
			//
			py +=65;
			Util.GUILabel(px,py,w,h,"スペースキーでゲーム開始");
			
		}
		
		
	}
	
	
}
