using UnityEngine;
using System.Collections;

public class Particle : Token {
	
	// パーティクル管理
	public static TokenMgr<Particle>parent = null;
	
	public static Particle Add (float x,float y)
	{
		Particle p = parent.Add(x,y);
		if(p)
		{
			p.SetVelocity(Random.Range(0,359),Random.Range(2.0f,4.0f));
			p.SetScale(0.25f,025f);
			           }
			           return p;
			           }
			           
			           // Use this for initialization
			           void Start () {
				
			}
			
			// Update is called once per frame
			void Update () {
				MulVelocity(0.95f);
				MulScale(0.97f);
				if(Scale < 0.01f)
				{
					Vanish();
				}
				
			}
			}
