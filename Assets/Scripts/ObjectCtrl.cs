//#define MUTEKI
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCtrl : MonoBehaviour
{

	[Space]
	[SerializeField]
	SpriteRenderer MainPic; // メイン画像.
	[SerializeField]
	Transform MainPos;      // 座標・回転関連.
	[SerializeField]
	BoxCollider2D MainHit;  // 当たり判定.

	[Space]

	public int LIFE = 0;        // 耐久力.
	public bool NOHIT = false;  // 当たり判定の有無.
	[Space]
	public float speed = 0.0f;  // 移動速度.
	public int angle = 0;       // 移動角度(360度を256段階で指定).
	public int type = 0;		// キャラクタタイプ(同じキャラクタだけど動きが違うなどの振り分け).
	public int mode = 0;        // 動作モード(キャラクタによって意味が違う).
	public int power = 0;       // 相手に与えるダメージ量.
	public int count = 0;       // 動作カウンタ.
	public Vector3 vect = Vector3.zero;


	public int ship_energy_charge = 0;



	[Space]
	public Sprite[] SPR_SHIP; // 暫定；キャラクタ画像置き場・将来的にはキャラクタ設定時にリストに登録するようにする.
	public Sprite[] SPR_MYSHOT;
//	public Sprite[] SPR_CHARGE;
//	public Sprite[] SPR_MYPOWERSHOT;
	public Sprite[] SPR_ENEMY;
	public Sprite[] SPR_BULLET;
	public Sprite[] SPR_CRUSH;
	public Sprite[] SPR_GENERATE;

	readonly Color COLOR_NORMAL = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	readonly Color COLOR_DAMAGE = new Color(1.0f, 0.0f, 0.0f, 1.0f);

	readonly int[] SCORE_TABLE = new int[8] { 470, 260, 280, 580, 930, 670, 490, 530 };

	const float SHIP_MOVE_MIN_X = -2.4f;
	const float SHIP_MOVE_MAX_X = 2.4f;
	const float SHIP_MOVE_MIN_Y = -4.7f;
	const float SHIP_MOVE_MAX_Y = 4.5f;

	const float MYSHOT_DISP_MAX_Y = 5.5f;

	const float ENEMY_OFFSCREEN_MIN_X = -3.0f;
	const float ENEMY_OFFSCREEN_MAX_X = 3.0f;
	const float ENEMY_OFFSCREEN_MIN_Y = -5.5f;
	const float ENEMY_OFFSCREEN_MAX_Y = 5.5f;

	const float BULLET_OFFSCREEN_MIN_X = -3.0f;
	const float BULLET_OFFSCREEN_MAX_X = 3.0f;
	const float BULLET_OFFSCREEN_MIN_Y = -5.5f;
	const float BULLET_OFFSCREEN_MAX_Y = 5.5f;

	readonly Vector2 HITSIZE_MYSHIP = new Vector2(0.08f, 0.08f);
	readonly Vector2 HITSIZE_MYSHOT = new Vector2(0.48f, 0.84f);
	readonly Vector2 HITSIZE_ENEMY = new Vector2(0.24f, 0.24f);
	readonly Vector2 HITSIZE_ENEMYBULLET = new Vector2(0.08f, 0.08f);
	readonly Vector2 HITSIZE_ENEMYBIGBULLET = new Vector2(0.16f, 0.16f);

	public Color color = new Color();                       // キャラクタ表示色(ホワイトアウトシェーダ採用のため0.5fが基準).
	public List<Sprite> PictureList = new List<Sprite>();   // キャラクタのスプライトリスト.

	public ObjectManager.MODE obj_mode = ObjectManager.MODE.NOUSE;  // キャラクタの管理状態.
	public ObjectManager.TYPE obj_type = ObjectManager.TYPE.NOUSE;  // キャラクタの分類(当たり判定時に必要).

	public bool destroy = false;	// 全滅時true(タイムアップで敵全滅)
	public bool stop = false;		// 停止時true(自機やられで敵移動停止)

	//	TouchPanelCtrl touch;
	ObjectManager manage;
	PadControlManager pad;


	void Awake()
	{
//		touch = GameObject.Find("TouchPanelCtrl").GetComponent<TouchPanelCtrl>();
		manage = GameObject.Find("gameroot").GetComponent<ObjectManager>();
		pad = GameObject.Find("gameroot").GetComponent<PadControlManager>();
		MainHit.enabled = false;
	}


	// Use this for initialization
	void Start()
	{

	}
	// Update is called once per frame
	void Update()
	{
		Vector3 pos = Vector3.zero;
/*
		if (game.mode==MainGameLoop.MODE.WAIT) {
			return;
		}
*/
		if (obj_mode == ObjectManager.MODE.NOUSE)
		{
			return;
		}
		switch (obj_mode)
		{
			case ObjectManager.MODE.NOUSE:
				break;
			case ObjectManager.MODE.INIT:
				MainPic.enabled = true;
				if (obj_type == ObjectManager.TYPE.NOHIT_EFFECT)
				{
					obj_mode = ObjectManager.MODE.NOHIT;
					MainHit.enabled = false;
				}
				else {
					obj_mode = ObjectManager.MODE.HIT;
					MainHit.enabled = true;
				}
				count = 0;
				break;
			case ObjectManager.MODE.HIT:
				MainHit.enabled = true;
				break;
			case ObjectManager.MODE.NOHIT:
				MainHit.enabled = false;
				break;
			case ObjectManager.MODE.FINISH:
//				game.ReturnObject(this);
				break;
		}
		switch (obj_type)
		{
			case ObjectManager.TYPE.MYSHIP:
				MainHit.enabled = true;
				MainHit.size = HITSIZE_MYSHIP;
				MainPic.color = COLOR_NORMAL;
				MainPic.sortingOrder = 0;
				Vector3 pd = new Vector3(0, 0, 0);
				pd = pad.pad.pad;


				if (manage.stop == false)
				{
					pd.x = pd.x / 15;
					pd.y = pd.y / 15;
					pos = this.transform.localPosition + pd;
					if (pos.x > SHIP_MOVE_MAX_X)
					{
						pos.x = SHIP_MOVE_MAX_X;
					}
					else if (pos.x < SHIP_MOVE_MIN_X)
					{
						pos.x = SHIP_MOVE_MIN_X;
					}
					if (pos.y > SHIP_MOVE_MAX_Y)
					{
						pos.y = SHIP_MOVE_MAX_Y;
					}
					else if (pos.y < SHIP_MOVE_MIN_Y)
					{
						pos.y = SHIP_MOVE_MIN_Y;
					}
					this.transform.localPosition = pos;
					manage.SHIP_POS = pos;
					if (pd.x != 0.0f)
					{
						mode++;
					}
					else
					{
						mode = 0;
						MainPic.sprite = SPR_SHIP[1];
					}
					if (mode >= 5)
					{
						if (pd.x < 0.0f)
						{
							MainPic.sprite = SPR_SHIP[0];
						}
						else if (0.0f < pd.x)
						{
							MainPic.sprite = SPR_SHIP[2];
						}
					}
					// ショット射出
					if (
						(pad.pad.button[0] == true)
					|| (pad.pad.button[1] == true)
					|| (pad.pad.button[2] == true)
					|| (pad.pad.button[3] == true)
					)
					{
						if (
							(pad.pad.button_oneshot[0] == true)
						|| (pad.pad.button_oneshot[1] == true)
						|| (pad.pad.button_oneshot[2] == true)
						|| (pad.pad.button_oneshot[3] == true)
						)
						{
							count = 0;
						}
						if ((count & 1) == 0)
						{
							manage.Set(ObjectManager.TYPE.MYSHOT, 0, pos, 0, 1);
							if ((count & 3) == 0)
							{
								SoundManager.Instance.PlaySE((int)SoundHeader.SE.SHIP_SHOT);
							}
						}
					}
				}
				else
				{
					MainPic.sortingOrder = 50;
					if (mode == 100)
					{
						MainPic.color = COLOR_NORMAL;
						MainPic.sprite = SPR_CRUSH[4];
					}
					switch (mode)
					{
						case 104:
							MainPic.sprite = SPR_CRUSH[5];
							break;
						case 108:
							MainPic.sprite = SPR_CRUSH[6];
							break;
						case 112:
							MainPic.sprite = SPR_CRUSH[7];
							break;
						case 116:
							MainPic.sprite = SPR_CRUSH[6];
							break;
						case 120:
							MainPic.sprite = SPR_CRUSH[7];
							break;
						case 124:
							MainPic.sprite = SPR_CRUSH[6];
							break;
						case 128:
							MainPic.sprite = SPR_CRUSH[7];
							break;
						case 132:
							MainPic.sprite = SPR_CRUSH[6];
							break;
						case 136:
							MainPic.sprite = SPR_CRUSH[7];
							break;
						case 140:
							MainPic.sprite = SPR_CRUSH[6];
							break;
					}
					mode++;
				}

				break;
			case ObjectManager.TYPE.MYSHOT:
				if (count == 0) {
					MainHit.enabled = true;
					MainHit.size = HITSIZE_MYSHOT;
					NOHIT = false;
					LIFE = 1;
					power = 2;
					vect.x = 0;
					vect.y = speed * 0.7f ;
					MainPic.color = COLOR_NORMAL;
				}

				if (manage.stop == false)
				{
					this.transform.localPosition += vect;
				}
				MainPic.sprite = SPR_MYSHOT[0];
				if (this.transform.localPosition.y >= MYSHOT_DISP_MAX_Y)
				{
					manage.Return(this);
				}
				break;
/*
			case ObjectManager.TYPE.MYPOWERSHOT:
				if (count == 0)
				{
					power = 16;
					vect = SetVector(game.target, speed, 0, true);
				}
				this.transform.localPosition += vect;
				MainPic.sprite = SPR_MYPOWERSHOT[(count & 1)];
				if (Mathf.Abs(this.transform.localPosition.x) > ENEMY_OFFSCREEN_MAX_X) {
					game.ReturnObject(this);
				}
				else if (Mathf.Abs(this.transform.localPosition.y) > ENEMY_OFFSCREEN_MAX_Y) {
					game.ReturnObject(this);
				}
				break;
*/
			case ObjectManager.TYPE.ENEMY1:
				if (manage.destroy == true)
				{
					Dead();
				}
				else
				{

					if (count == 0)
					{
						MainHit.enabled = true;
						NOHIT = false;
						LIFE = 2;
						power = 1;
						MainHit.size = HITSIZE_ENEMY;
						angle = 0;
						MainPic.color = COLOR_NORMAL;
					}
					MainPic.sprite = SPR_ENEMY[0 + ((count >> 2) % 4)];


					if (manage.stop == false)
					{
						Vector3 mov = new Vector3(0, 0, 0);
						mov.x = 0.0f;
						mov.y = 0 - ((float)speed * 0.01f);
						this.transform.localPosition += mov;

						if ((count % 16) == 0)
						{
							if (Random.Range((count / 512), 256) >= 254)
							{
								manage.Set(ObjectManager.TYPE.ENEMYBULLET, 0, this.transform.localPosition, 0, (Random.Range(2, 11) + 1));
								//game.SetObject(ObjectManager.TYPE.ENEMYBULLET, 0, this.transform.localPosition, 0, 7);
							}
						}
					}



					if (manage.stop == false)
					{
						if ((count % 4) == 0)
						{
							MainPic.color = COLOR_NORMAL;
						}
					}
					if (Mathf.Abs(this.transform.localPosition.x) > ENEMY_OFFSCREEN_MAX_X)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
					else if (Mathf.Abs(this.transform.localPosition.y) > ENEMY_OFFSCREEN_MAX_Y)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
				}
				break;

			case ObjectManager.TYPE.ENEMY2:
				if (manage.destroy == true)
				{
					//FileOutput.Log("enemy2:pos=" + this.transform.localPosition);
					Dead();
				}
				else
				{

					if (count == 0)
					{
						MainHit.enabled = true;
						NOHIT = false;
						LIFE = 6;
						power = 1;
						MainHit.size = HITSIZE_ENEMY;
						angle = 0;
						MainPic.color = COLOR_NORMAL;



						Vector3 orig = this.transform.localPosition;
						if (orig.x < 0)
						{
							orig.x = -2.4f + Random.Range(0.0f, 0.1f);
							angle = 0 - Random.Range(0x08, 0x20);
						}
						else
						{
							orig.x = 2.4f - Random.Range(0.0f, 0.1f);
							angle = 0 + Random.Range(0x08, 0x20);
						}
						this.transform.localPosition = orig;
						mode = 0;
					}
					MainPic.sprite = SPR_ENEMY[4 + ((count >> 2) % 4)];
					int bullet_check = 254;

					switch (mode)
					{
						case 0: // 最初～自機にXが近づくまで(等速)
							vect = AngleToVector3(angle, 5);
							vect = vect / 100.0f;
							//FileOutput.Log("pos("+this.transform.localPosition+") vect(" + vect + ")");

							break;
						case 1: // 減速
							vect.y += 0.01f;
							if (vect.x < 0)
							{
								vect.x += 0.01f;
								if (vect.x > 0)
								{
									mode = 2;
								}
							}
							else
							{
								vect.x -= 0.01f;
								if (vect.x < 0)
								{
									mode = 3;
								}
							}
							break;
						case 2: // 反転(プラス方向に加速)
							if (vect.x < 0.03f)
							{
								vect.x += 0.005f;
							}
							if (vect.y < 0.02f)
							{
								vect.y += 0.01f;
							}
							bullet_check = 100;
							break;
						case 3: // 反転(マイナス方向に加速)
							if (-0.03f < vect.x)
							{
								vect.x -= 0.005f;
							}
							if (vect.y < 0.02f)
							{
								vect.y += 0.01f;
							}
							bullet_check = 100;
							break;
					}


					Vector3 check = new Vector3(0, 0, 0);
					check = this.transform.localPosition;
					if (manage.stop == false)
					{
						this.transform.localPosition += vect;
						if (check.x < manage.SHIP_POS.x)
						{
							check.x = check.x - manage.SHIP_POS.x;
						}
						else
						{
							check.x = manage.SHIP_POS.x - check.x;
						}
					}
					//FileOutput.Log("local.x="+this.transform.localPosition.x+" / ship.x="+manage.SHIP_POS.x+" / check.x=" + check.x);
					if (mode == 0)
					{
						if (Mathf.Abs(check.x) < 0.6f)
						{
							mode = 1;
							count = 0;
						}
					}
					if (manage.stop == false)
					{
						if ((count % 16) == 0)
						{
							if (Random.Range((count / 512), 256) >= bullet_check)
							{
								int maxspd = 9;
								if (bullet_check <= 100)
								{
									maxspd = 13;
								}
								manage.Set(ObjectManager.TYPE.ENEMYBULLET, 0, this.transform.localPosition, 0, Random.Range(5, maxspd));
								//game.SetObject(ObjectManager.TYPE.ENEMYBULLET, 0, this.transform.localPosition, 0, 7);
							}
						}
					}



					if (manage.stop == false)
					{
						if ((count % 4) == 0)
						{
							MainPic.color = COLOR_NORMAL;
						}
					}
					if (Mathf.Abs(this.transform.localPosition.x) > ENEMY_OFFSCREEN_MAX_X)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
					else if (Mathf.Abs(this.transform.localPosition.y) > ENEMY_OFFSCREEN_MAX_Y)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
				}
				break;

			case ObjectManager.TYPE.ENEMY3:
				if (manage.destroy == true)
				{
					Dead();
				}
				else
				{
					if (count == 0)
					{
						MainHit.enabled = true;
						NOHIT = false;
						LIFE = 4;
						power = 1;
						MainHit.size = HITSIZE_ENEMY;
						vect = SetVector(manage.SHIP_POS, speed + 1, Random.Range(-0.03f, 0.03f), false);
						count = 1;
						MainPic.color = COLOR_NORMAL;
						MainPic.sortingOrder = -2;
						//angle = 0;
					}
					MainPic.sprite = SPR_ENEMY[8 + ((count >> 2) % 4)];
					if (manage.stop == false)
					{
						this.transform.localPosition += vect;
						if ((count % 16) == 0)
						{
							if (Random.Range((count / 512), 256) >= 254)
							{
								int rnd = Random.Range(2, 5);
								manage.Set(ObjectManager.TYPE.ENEMYBULLET, 0, this.transform.localPosition, 0, rnd);
								manage.Set(ObjectManager.TYPE.ENEMYBIGBULLET, 0, this.transform.localPosition, 0, rnd + 2);
								//game.SetObject(ObjectManager.TYPE.ENEMYBULLET, 0, this.transform.localPosition, 0, 7);
							}
						}
					}



					if (manage.stop == false)
					{
						if ((count % 4) == 0)
						{
							MainPic.color = COLOR_NORMAL;
						}
					}
					if (Mathf.Abs(this.transform.localPosition.x) > ENEMY_OFFSCREEN_MAX_X)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
					else if (Mathf.Abs(this.transform.localPosition.y) > ENEMY_OFFSCREEN_MAX_Y)
					{
						manage.Return(this);
						//game.ReturnObject(this);

					}
				}
				break;



			case ObjectManager.TYPE.ENEMYBULLET:
				if (manage.destroy == true)
				{
					Dead();
				}
				else
				{

					if (count == 0)
					{
						if (Mathf.Abs(manage.SHIP_POS.y - this.transform.localPosition.y) < 2.45f)
						{
							//FileOutput.Log("bullet erase:manage.SHIP_POS.y-this.transform.localPosition.y=" + Mathf.Abs(manage.SHIP_POS.y - this.transform.localPosition.y));
							manage.Return(this);
						}

						MainHit.enabled = true;
						MainHit.size = HITSIZE_ENEMYBULLET;
						MainPic.color = COLOR_NORMAL;
						MainPic.sortingOrder = -1;
						power = 1;
						LIFE = 10;
						vect = SetVector(manage.SHIP_POS, speed, Random.Range(-0.03f, 0.03f), false);
						//					MainHit.enabled = true;
						//					obj_mode = ObjectManager.MODE.HIT;
						//vect.x = vect.x;
						//vect.y = vect.y;
					}
					if (manage.stop == false)
					{
						this.transform.localPosition += vect;
					}
					MainPic.sprite = SPR_BULLET[((count % 16) >> 3) + 2];
					if (Mathf.Abs(this.transform.localPosition.x) > BULLET_OFFSCREEN_MAX_X)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
					else if (Mathf.Abs(this.transform.localPosition.y) > BULLET_OFFSCREEN_MAX_Y)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
				}
				break;



			case ObjectManager.TYPE.ENEMYBIGBULLET:
				if (manage.destroy == true)
				{
					Dead();
				}
				else
				{
					if (count == 0)
					{
						if (Mathf.Abs(manage.SHIP_POS.y - this.transform.localPosition.y) < 2.45f)
						{
							//FileOutput.Log("bullet erase:manage.SHIP_POS.y-this.transform.localPosition.y=" + Mathf.Abs(manage.SHIP_POS.y - this.transform.localPosition.y));
							manage.Return(this);
						}

						MainHit.enabled = true;
						MainHit.size = HITSIZE_ENEMYBIGBULLET;
						MainPic.color = COLOR_NORMAL;
						MainPic.sortingOrder = -1;
						power = 1;
						LIFE = 10;
						vect = SetVector(manage.SHIP_POS, speed, Random.Range(-0.03f, 0.03f), false);
						//					MainHit.enabled = true;
						//					obj_mode = ObjectManager.MODE.HIT;
						//vect.x = vect.x;
						//vect.y = vect.y;
					}
					if (manage.stop == false)
					{
						this.transform.localPosition += vect;
					}
					MainPic.sprite = SPR_BULLET[((count % 16) >> 3) + 0];
					if (Mathf.Abs(this.transform.localPosition.x) > BULLET_OFFSCREEN_MAX_X)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
					else if (Mathf.Abs(this.transform.localPosition.y) > BULLET_OFFSCREEN_MAX_Y)
					{
						manage.Return(this);
						//game.ReturnObject(this);
					}
				}
				break;







			case ObjectManager.TYPE.NOHIT_EFFECT:
				MainPic.color = COLOR_NORMAL;
				MainHit.enabled = false;
				if (manage.stop == false)
				{
					switch (mode)
					{
						case 0:
							MainPic.sprite = SPR_CRUSH[(count % 16) >> 2];
							if (count == 16)
							{
								manage.Return(this);
								//game.ReturnObject(this);
							}
							break;
						case 1:
							MainPic.sprite = SPR_CRUSH[(count % 16) >> 2];
							vect = SetVector(new Vector3(0, 0, 0), speed, angle, false);
							this.transform.localPosition += vect;
							if (count == 16)
							{
								manage.Return(this);
								//game.ReturnObject(this);
							}
							break;
						case 2:
							MainPic.sprite = SPR_CRUSH[(count % 16) >> 2];
							vect.x = (float)Random.Range(-(count), (count)) / 10.0f;
							vect.y = (float)Random.Range(-(count), (count)) / 10.0f;
							this.transform.localPosition += vect;
							manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, 1, this.transform.localPosition, Random.Range(0, 360), Random.Range(1, 5));
							//game.SetObject(ObjectManager.TYPE.NOHIT_EFFECT, 0, this.transform.localPosition + vect, 0, 0);
							if (count == 16)
							{
								manage.Return(this);
								//game.ReturnObject(this);
							}
							break;
					}
				}
				break;
		}



		if (LIFE <= 0)
		{
			Dead();
		}



		count++;
	}
	//-------------------------------------------------------------------
	//	public void SetVector(Vector3 pos, float spd, float rad_offset)
	//		敵弾に位置を基準とした移動量を設定
	//	Vector3 pos=基準座標
	//	float spd=移動速度
	//	float rad_offset=追加角度(扇状弾などに使う)
	//-------------------------------------------------------------------
	Vector3 SetVector(Vector3 pos, float spd, float rad_offset,bool rotation)
	{
		Vector3 vec = Vector3.zero;
		if (spd == 0.0f)    //必ず少しは動くようにする.
		{
			spd = 1f;
		}
		spd = spd / 100.0f;
		float rad = Mathf.Atan2(
								pos.y - this.transform.position.y,
								pos.x - this.transform.position.x
								);              //座標から角度を求める.
		rad += rad_offset;                      //角度にオフセット値追加.
		vec.x = spd * Mathf.Cos(rad);           //角度からベクトル求める.
		vec.y = spd * Mathf.Sin(rad);
		if (rotation == true)
		{
			this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90.0f + RadToRotation(rad)));
//			Debug.Log("rad=" + rad + " / angle=" + this.transform.localRotation.z);
		}
		return vec;
	}
	Vector3 AngleToVector3(int ang,float spd) {
		Vector3 vec = Vector3.zero;
		float rot = AngleToRotation(ang);
		float rad = ((rot - 90.0f) * Mathf.PI) / 180.0f;
		vec.x = spd * Mathf.Cos(rad);           //角度からベクトル求める.
		vec.y = spd * Mathf.Sin(rad);
//		FileOutput.Log("ang=" + ang + " / rot=" + rot + " / rad=" + rad);
		return vec;
	}



	float GetRad(Vector3 pos) {
		return Mathf.Atan2(	pos.y - this.transform.localPosition.y,
							pos.x - this.transform.localPosition.x
						);
	}
	float RadToRotation(float rad) {
		return (float)(((rad * 180.0f) / Mathf.PI) % 360.0f);
	}

	float AngleToRotation(int ang) {
		return (float)(360.0f - (360.0f * ((float)(ang % 256) / 256.0f)));
	}
	int RotationToAngle(float rot)
	{
		return ((int)(256.0f * (360.0f - ((rot % 360.0f) / 360.0f)))) % 256;
	}

#if false
	public void SetObject(MainGameLoop mgame)
	{
		game = mgame;
	}
#endif

	public void Generate(ObjectManager.TYPE type, Vector3 pos, int ang, int spd)
	{
		obj_mode = ObjectManager.MODE.INIT;
		obj_type = type;
		this.transform.localPosition = pos;
		this.transform.localRotation = Quaternion.identity;
		angle = ang;
		speed = spd;
		LIFE = 1;
		vect = Vector3.zero;
	}


	void OnTriggerStay2D(Collider2D collider)
	{
		if (obj_mode == ObjectManager.MODE.NOHIT)
		{
			return;
		}
		ObjectCtrl other = collider.gameObject.GetComponent<ObjectCtrl>();
		if (other.obj_mode == ObjectManager.MODE.NOHIT)
		{
			return;
		}
		if (NOHIT == true)
		{
			return;
		}
		if (other.NOHIT == true)
		{
			return;
		}
		switch (other.obj_type)
		{
			case ObjectManager.TYPE.ENEMYBULLET:    // 敵弾・自分がMYSHIPの場合に処理.
			case ObjectManager.TYPE.ENEMYBIGBULLET: // 敵弾・自分がMYSHIPの場合に処理.
				if (this.obj_type == ObjectManager.TYPE.MYSHIP)
				{
					int rnd = Random.Range(0, 2);
					if (Random.Range(0, 256) > 220)
					{
						rnd = 2;
					}
					manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, rnd, other.transform.localPosition, 0, 0);
					Damage(other.power);
					other.Damage(power);
				}
				break;
			case ObjectManager.TYPE.ENEMY1:          // 敵・自分がMYSHIPかMYSHOTの場合に処理.
				if (this.obj_type == ObjectManager.TYPE.MYSHOT)
				{
					int rnd = Random.Range(0, 2);
					if (Random.Range(0, 256) > 220)
					{
						rnd = 2;
					}
					manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, rnd, other.transform.localPosition, 0, 0);
					Damage(other.power);
					other.Damage(power);
				}
				else if (this.obj_type == ObjectManager.TYPE.MYSHIP)
				{
					int rnd = Random.Range(0, 2);
					if (Random.Range(0, 256) > 220)
					{
						rnd = 2;
					}
					manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, rnd, other.transform.localPosition, 0, 0);
					Damage(other.power);
					other.Damage(power);
				}
				break;
			case ObjectManager.TYPE.ENEMY2:          // 敵・自分がMYSHIPかMYSHOTの場合に処理.
				if (this.obj_type == ObjectManager.TYPE.MYSHOT)
				{
					int rnd = Random.Range(0, 2);
					if (Random.Range(0, 256) > 220)
					{
						rnd = 2;
					}
					manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, rnd, other.transform.localPosition, 0, 0);
					Damage(other.power);
					other.Damage(power);
				}
				else if (this.obj_type == ObjectManager.TYPE.MYSHIP)
				{
					int rnd = Random.Range(0, 2);
					if (Random.Range(0, 256) > 220)
					{
						rnd = 2;
					}
					manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, rnd, other.transform.localPosition, 0, 0);
					Damage(other.power);
					other.Damage(power);
				}
				break;
			case ObjectManager.TYPE.ENEMY3:          // 敵・自分がMYSHIPかMYSHOTの場合に処理.
				if (this.obj_type == ObjectManager.TYPE.MYSHOT)
				{
					int rnd = Random.Range(0, 2);
					if (Random.Range(0,256)>220)
					{
						rnd = 2;
					}
					manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, rnd, other.transform.localPosition, 0, 0);
					Damage(other.power);
					other.Damage(power);
				}
				else if (this.obj_type == ObjectManager.TYPE.MYSHIP)
				{
					int rnd = Random.Range(0, 2);
					if (Random.Range(0, 256) > 220)
					{
						rnd = 2;
					}
					manage.Set(ObjectManager.TYPE.NOHIT_EFFECT, rnd, other.transform.localPosition, 0, 0);
					Damage(other.power);
					other.Damage(power);
				}
				break;
/*
			case ObjectManager.TYPE.MYSHIP:         // 自機・自分がENEMYの場合に処理.
				if (
					(this.obj_type == ObjectManager.TYPE.ENEMY1)
				||	(this.obj_type == ObjectManager.TYPE.ENEMY2)
				||	(this.obj_type == ObjectManager.TYPE.ENEMY3)
				)
				{
					MainPic.color = COLOR_DAMAGE;
					//Damage(other.power);
					other.Damage(power);
				}
				else if (
					(this.obj_type == ObjectManager.TYPE.ENEMYBULLET)
				||	(this.obj_type == ObjectManager.TYPE.ENEMYBIGBULLET)
				)
				{
					MainPic.color = COLOR_DAMAGE;
					//Damage(other.power);
					other.Damage(power);
				}
				break;
*/
			case ObjectManager.TYPE.MYSHOT:         // 自機弾・自分がENEMYの場合自分のLIFEを減らす処理.
				if (
					(this.obj_type == ObjectManager.TYPE.ENEMY1)
				||	(this.obj_type == ObjectManager.TYPE.ENEMY2)
				||	(this.obj_type == ObjectManager.TYPE.ENEMY3)
				)
				{
					Damage(other.power);
					other.Damage(power);
				}
				break;
		}
	}



	public void Damage(int damage)
	{
		LIFE -= damage;
		if (LIFE <= 0)
		{
			Dead();
		}
		MainPic.color = COLOR_DAMAGE;
	}


	public void Dead()
	{
		obj_mode = ObjectManager.MODE.NOHIT;
		switch (obj_type)
		{
			case ObjectManager.TYPE.ENEMYBULLET:
			case ObjectManager.TYPE.ENEMYBIGBULLET:
				if (manage.destroy == true)
				{
					obj_type = ObjectManager.TYPE.NOHIT_EFFECT;
					LIFE = 1;
					mode = 0;
					count = 0;
					MainHit.enabled = false;
					MainPic.sprite = SPR_CRUSH[0];
					MainPic.color = COLOR_NORMAL;
				}
				else
				{
					manage.Return(this);
				}
				//game.ReturnObject(this);
				break;
			case ObjectManager.TYPE.MYSHOT:
				manage.Return(this);
				break;
			case ObjectManager.TYPE.ENEMY1:
			case ObjectManager.TYPE.ENEMY2:
			case ObjectManager.TYPE.ENEMY3:
				//game.score += SCORE_TABLE[mode];
				if (manage.destroy == false)
				{
					switch (obj_type)
					{
						case ObjectManager.TYPE.ENEMY1:
							manage.score += MainGameLoop.SCORETABLE[0];
							break;
						case ObjectManager.TYPE.ENEMY2:
							manage.score += MainGameLoop.SCORETABLE[1];
							break;
						case ObjectManager.TYPE.ENEMY3:
							manage.score += MainGameLoop.SCORETABLE[2];
							break;
					}
				}
				//FileOutput.Log("dead:pos=" + this.transform.localPosition);
				obj_type = ObjectManager.TYPE.NOHIT_EFFECT;
				LIFE = 1;
				mode = 0;
				count = 0;
				MainHit.enabled = false;
				MainPic.sprite = SPR_CRUSH[0];
				MainPic.color = COLOR_NORMAL;
				SoundManager.Instance.PlaySE((int)SoundHeader.SE.ENEMY_DEAD);
				if (Random.Range(0, 32) == 0)
				{
					mode = 2;
				}
				if (manage.expert == true)
				{
					int spd = Random.Range(2, 7);
					manage.Set(ObjectManager.TYPE.ENEMYBIGBULLET, 0, this.transform.localPosition, 0, spd);
					manage.Set(ObjectManager.TYPE.ENEMYBULLET, 0, this.transform.localPosition, 0, spd + Random.Range(1, 3));
				}
				//manage.Return(this);
				break;
			case ObjectManager.TYPE.MYSHIP:
				//obj_type = ObjectManager.TYPE.NOHIT_EFFECT;
				LIFE = 1;
				//mode = 2;
				//count = 0;
				//MainHit.enabled = false;
				//game.mode = MainGameLoop.MODE.GAMEOVER;
				//game.count = -1;
#if MUTEKI
				// 死亡処理スキップ
#else
				SoundManager.Instance.PlaySE((int)SoundHeader.SE.SHIP_MISS);
				SoundManager.Instance.StopBGM();
				manage.stop = true;
				mode = 100;
#endif
				break;
		}
		MainPic.color = COLOR_NORMAL;
		count = 0;
	}


	public void DisplayOff() {
		MainPic.enabled = false;
		MainHit.enabled = false;
	}



	public void Erase()
	{
		MainPic.enabled = false;
		MainHit.enabled = false;
		MainPic.color = COLOR_NORMAL;
		MainPic.sprite = SPR_CRUSH[7];
	}
}
