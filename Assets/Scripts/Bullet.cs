using UnityEngine;
using System.Collections;

public class Bullet : Token {
	
	//敵弾管理
	public static TokenMgr<Bullet>parent = null;
	//インスタンスの取得
	public static Bullet Add(float x,float y,float direction,float speed)
	{
		return parent.Add(x,y,direction,speed);
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		if(IsOutside()){
			// 画面外に出たので消す
			//DestroyObj();
			Vanish();
			
		}
	}
}
