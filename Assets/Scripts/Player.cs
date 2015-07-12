using UnityEngine;
using System.Collections;

///プレイヤー
public class Player : Token {
	
	public Sprite Spr0;// 待機画像0
	public Sprite Spr1;// 待機画像1
	
	// 移動速度
	public float MoveSpeed = 5.0f;
	///アニメーションタイマー
	int _tAnim =0;
	
	// 開始
	void Start()
	{
		var w = SpriteWidth  / 2 ;
		var h = SpriteHeight / 2 ;
		SetSize(w,h);
	}
	
	//固定フレームで更新
	void FixedUpdate()
	{
		_tAnim =(_tAnim + 1) % 48;
		if(_tAnim %48 <24)
		{
			// 0-23フレームは「Spr0」
			SetSprite(Spr0);
		} else {
			SetSprite(Spr1);
		}
	}
	
	// 更新
	void Update()
	{
		// 移動処理
		Vector2 v = Util.GetInputVector();
		float speed = MoveSpeed * Time.deltaTime;
		// 移動して画面外に出ないようにする。
		ClampScreenAndMove( v * speed);
		// Spaceキーでショットを撃つ
		if(Input.GetKey(KeyCode.Space))
		{
			// X座標をランダムにずらす
			float px = X+Random.Range(0,SpriteWidth /2);
			// 発射角度
			float dir = Random.Range(-5.0f,5.0f);
			Shot.Add(px,Y,dir,10);
		}
	}
	
	//衝突判定
	void OnTriggerEnter2D(Collider2D other){
		// Layer名を取得する。
		string name = LayerMask.LayerToName(other.gameObject.layer);
		switch(name)
		{
		case "Enemy":
		case "Bullet":
			// ゲームオーバー
			Vanish();
			//パーティクル生成
			for(int i=0;i<12;i++)
			{
				Particle.Add(X,Y);
				
			}
			// 
			Sound.PlaySe("damage");
			//
			Sound.StopBgm();
			break;         
		};
		
	}
	
	
}
