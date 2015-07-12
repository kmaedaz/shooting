using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {
	//
	enum eState
	{
		Init,
		Main,
		GameClear,
		GameOver,
	}
	
	eState _state = eState.Init;
	
	// ゲーム開始
	void Start ()
	{
		//ショットオブジェクトを32個確保しておく
		Shot.parent = new TokenMgr<Shot>("Shot",32);
		//パーティクルオブジェクトを256個確保しておく
		Particle.parent = new TokenMgr<Particle>("Particle",256);
		// 敵弾
		Bullet.parent = new TokenMgr<Bullet>("Bullet",256);
		/// 敵オブジェクトを６４個確保.
		Enemy.parent = new TokenMgr<Enemy>("Enemy",64);
		
		
		// プレイヤーの参照を敵に登録する
		Enemy.target =GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(_state)
		{
		case eState.Init:
			//
			Sound.PlayBgm("bgm");
			_state = eState.Main;
			break;
			
		case eState.Main:
			//
			if(Boss.bDestroyed)
			{
				//
				Sound.StopBgm();
				_state = eState.GameClear;
				
			}
			//
			else if(Enemy.target.Exists == false)
			{
				//
				_state = eState.GameOver;
				
			}
			break;
			
		case eState.GameClear:
			//
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Application.LoadLevel("Title");
			}

			break;
			
		case eState.GameOver:
			//
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Application.LoadLevel("Main");
			}

			break;
			
			
		}
		
	}
	
	void DreawLabelCenter(string message )
	{
		Util.SetFontSize(32);
		//
		Util.SetFontAlignment(TextAnchor.MiddleCenter);
		//
		float w =128 ;
		float h =32;
		float px = Screen.width /2 -w /2;
		float py = Screen.height /2 -h/2;
		Util.GUILabel(px,py,w,h,message);        
	}
	
	void OnGUI()
	{
		switch(_state)
		{
		case eState.GameClear:
			DreawLabelCenter("GAME CLEAR !");  
			break;
			
		case eState.GameOver:
			DreawLabelCenter("GAME OVER");  
			break;
			
			
		}
		
	}

	void OnDestroy()
	{
		Shot.parent = null;
		Enemy.parent = null;
		Bullet.parent = null;
		Particle.parent = null;
		Enemy.target = null;
	}

	
}
