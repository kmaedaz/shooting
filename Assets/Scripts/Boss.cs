using UnityEngine;
using System.Collections;

public class Boss : Enemy {
	
	// コルーチンを開始したか
	bool _bStart = false;
	// 
	public static bool bDestroyed ;

	void Start()
	{
		SetParam(0);
		// 
		bDestroyed = false ;
	}
	public override void Vanish()
	{
		bDestroyed = true ;
		base.Vanish();
	}
	
	
	void OnGUI()
	{
		Util.SetFontColor(Color.black);
		
		Util.SetFontSize(24);
		Util.SetFontAlignment(TextAnchor.MiddleCenter);	 
		// 
		string text = string.Format("{0,3}",Hp);
		Util.GUILabel(380,200,120,30,text);	 
	}
	
	
	
	void Update()
	{
		if(_bStart == false)
		{
			StartCoroutine("_GenerateEnemey");
			_bStart =true;
		}
		
	}
	
	// にんじん発射	
	void BulletCarrot()
	{
		AddEnemy(5,45,3);
		AddEnemy(5,-45,3);
		
	}
	
	// ダイコンを３方向に発射	
	void BulletRadish()
	{
		float aim =GetAim();
		AddEnemy(4,aim,3);
		AddEnemy(4,aim-30,3);
		AddEnemy(4,aim+30,3);
		
	}
	
	Enemy AddEnemy(int id ,float direction,float speed)
	{
		return Enemy.Add(id ,X,Y,direction,speed);
	}
	//
	//敵生成
	IEnumerator _GenerateEnemey()
	{
		while (true) {
			AddEnemy (1, 135, 5);
			AddEnemy (1, 255, 5);
			yield return new WaitForSeconds (3);
			BulletRadish();
			yield return new WaitForSeconds (2);
			AddEnemy (2, 90, 5);
			AddEnemy (2, 270, 5);
			BulletCarrot();
			yield return new WaitForSeconds (3);
			AddEnemy (3, 45, 5);
			AddEnemy (3, -45, 5);
			yield return new WaitForSeconds (3);
			BulletRadish();
			yield return new WaitForSeconds (2);
			BulletCarrot();
			
		}
	}
	
}
