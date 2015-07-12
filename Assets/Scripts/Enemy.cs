using UnityEngine;
using System.Collections;

//敵
public class Enemy : Token
{
	// スプライト
	public Sprite Spr0;
	public Sprite Spr1;
	public Sprite Spr2;
	public Sprite Spr3;
	public Sprite Spr4;
	public Sprite Spr5;
	
	
	/// 敵管理
	public static TokenMgr<Enemy>parent = null;
	
	//HP
	int _hp= 0;
	
	
	//ＩＤからパラメータを設定
	public void SetParam(int id)
	{
		//
		if(_id !=0)
		{
			StopCoroutine("_Update"+_id);
			
		}
		if(id !=0)
		{
			StartCoroutine("_Update"+id);
			
		}
		
		// IDを設定
		_id = id;
		//HP
		int[] hps={500,30,30,30,30,30};
		// スプライトテーブル
		Sprite[] sprs ={Spr0,Spr1,Spr2,Spr3,Spr4,Spr5};
		// HPを設定
		_hp =hps[id];
		
		//
		SetSprite(sprs[id]);		
		//
		Scale = 0.5f;
	}
	
	// 敵の管理
	public static Enemy Add(int id ,float x,float y,float direction,float speed)
	{
		Enemy e =parent.Add(x,y,direction,speed);
		if(e ==null)
		{
			return null;
		}
		//
		e.SetParam(id);
		return e;
	}
	
	public static Player target=null;
	/// 狙い撃ち角度を取得する。
	public float GetAim()
	{
		float dx = target.X-X;
		float dy = target.Y-Y;
		return Mathf.Atan2(dy,dx)*Mathf.Rad2Deg;
	}
	///敵ID
	int _id=0;
	
	// 弾を発射する
	void DoBullet(float direction,float speed)
	{
		Bullet.Add(X,Y,direction, speed);
		
	}		
	
	
	// HPの取得
	public int Hp
	{
		get {return _hp; }
		
	}
	
	bool Damage(int v)
	{
		_hp -=v;
		if( _hp <=0)
		{
			//HPがなくなったので死亡
			Vanish();
			// 倒した
			for(int i=0 ;i<4 ;i++)
			{
				Particle.Add(X,Y);
			}
			// SE
			Sound.PlaySe("destroy",0);
			if( _id ==0)
			{
				Enemy.parent.ForEachExist(e =>e.Damage(9999));
				// 敵弾をすべて消す
				if(Bullet.parent != null)
				{
					Bullet.parent.Vanish();
				}
			}
			return true;
		}
		// まだ生きている
		return false;
	}
	
	// Use this for initialization
	void Start () {
		// HP
		//_hp = 30;
		
		//IDを設定
		//_id= 3;
		
		//コルーチン開始
		//StartCoroutine("_Update" + _id);		
	}
	
	
	IEnumerator _Update1 ()
	{
		while (true) {
			yield return new WaitForSeconds (2.0f);
			// 狙い撃ち角度を取得する。
			float dir = GetAim();
			Bullet.Add (X, Y, dir, 2);
		}
	}
	
	IEnumerator _Update2 ()
	{
		float dir=0;
		yield return new WaitForSeconds (2.0f);
		
		while (true) {
			Bullet.Add (X, Y, dir, 2);
			dir +=16;
			yield return new WaitForSeconds (0.1f);
		}
	}
	
	IEnumerator _Update3 ()
	{
		
		while (true) {
			yield return new WaitForSeconds (2.0f);
			DoBullet(180-3,3);
			DoBullet(180,3);
			DoBullet(180+3,3);
		}
	}
	
	IEnumerator _Update4 ()
	{
		
		yield return new WaitForSeconds (1.0f);
	}
	
	IEnumerator _Update5 ()
	{
		//1回の更新で旋回する角度
		const float ROT =5.0f;
		//ホーミングする。
		while(true)
		{
			yield return new WaitForSeconds (0.02f);
			// 現在の角度 
			float dir =Direction;
			// 狙い撃ち角度
			float aim=GetAim();
			//角度差
			float delta =Mathf.DeltaAngle(dir, aim);  
			if( Mathf.Abs(delta)< ROT)
			{
				
			} else if( delta >0)
			{
				dir += ROT;
			} else {
				dir -= ROT;
			}
			SetVelocity(dir,Speed);
			
			//画像も回転させる
			Angle = dir ;
			if(IsOutside())
			{
				Vanish();
			}
		}
	}
	
	
	//衝突判定
	void OnTriggerEnter2D(Collider2D other){
		// Layer名を取得する。
		string name = LayerMask.LayerToName(other.gameObject.layer);
		
		if( name =="Shot"){
			//
			Shot s = other.GetComponent<Shot>();
			s.Vanish();
			//
			Damage(1);
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if( _id ==4)
		{
			//ダイコンのみ
			Vector2 min = GetWorldMin();
			Vector2 max = GetWorldMax();
			if( Y< min.y || max.y>Y)
			{
				ClampScreen();
				//
				VY *=-1;
			}
			if( X< min.x || max.x>X)
			{
				Vanish();
				Angle = Direction;
			}
			
		}
		MulVelocity (0.93f);
	}
	void FiexdUpdate()
	{
		if(_id<3)
		{
			MulVelocity(0.93f);
		}
		
	}
	
}

