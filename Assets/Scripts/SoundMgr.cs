using UnityEngine;
using System.Collections;

public class SoundMgr : MonoBehaviour {
	
	// Use this for initialization
	void Start ()
	{
		//
		Sound.LoadBgm("bgm","bgm01");    
		//
		Sound.LoadBgm("damage","damage");    
		
		Sound.LoadBgm("destroy","destroy");    
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


